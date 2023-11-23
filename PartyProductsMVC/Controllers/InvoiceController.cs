using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using PartyProductsMVC.DAL;
using PartyProductsMVC.Models;
using PartyProductsMVC.ViewModels;

namespace PartyProductsMVC.Controllers
{
    public class InvoiceController : Controller
    {
        private PartyProductContext db = new PartyProductContext();

        // GET: Invoice
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Party);
            return View(invoices.ToList());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            var parties = (from t1 in db.Parties
                           join t2 in db.AssignParties on t1.Id equals t2.PartyID
                           select t1).ToList();
            var products = (from t1 in db.Products
                           join t2 in db.AssignParties on t1.Id equals t2.ProductID
                           join t3 in db.ProductRates on t1.Id equals t3.ProductID
                           select t1).ToList();
            ViewBag.PartyID = new SelectList(parties, "Id", "PartyName");
            ViewBag.ProductID = new SelectList(products, "Id", "ProductName");
            var model = new InvoiceViewModel()
            {
                Rate = db.ProductRates.OrderByDescending(t => t.Id).Select(t => t.Rate).FirstOrDefault(),
                //InvoiceViewModels = new List<InvoiceViewModel>()
            };
            return View(model);
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartyID,ProductId,Rate,Quantity,InvoiceId")] InvoiceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Total = vm.Rate * vm.Quantity;
                vm.IsDisabled = true;
                var parties = (from t1 in db.Parties where t1.Id == vm.PartyID
                               select t1).ToList();
                var products = (from t1 in db.Products
                                join t2 in db.AssignParties on t1.Id equals t2.ProductID
                                join t3 in db.ProductRates on t1.Id equals t3.ProductID
                                select t1).ToList();
                ViewBag.PartyID = new SelectList(parties, "Id", "PartyName");
                ViewBag.ProductID = new SelectList(products, "Id", "ProductName");
                var invoiceDB = new Invoice()
                {
                    PartyID = vm.PartyID,
                    GrandTotal = 0,
                };
                if (vm.InvoiceID == 0)
                {
                    db.Invoices.Add(invoiceDB);
                    db.SaveChanges();
                    vm.InvoiceID = invoiceDB.Id;
                    var invoiceDetailDB = new InvoiceDetail()
                    {
                        PartyID = vm.PartyID,
                        ProductID = vm.ProductID,
                        Rate = vm.Rate,
                        Quantity = vm.Quantity,
                        Total = vm.Total,
                        InvoiceID = vm.InvoiceID
                    };
                    db.InvoiceDetails.Add(invoiceDetailDB);
                    UpdateGrandTotal(invoiceDB, vm.Total);
                    vm.InvoiceDetails = db.InvoiceDetails.Include(i=>i.Invoices).Include(i=>i.Party).Include(i=>i.Product).Where(i=>i.InvoiceID == vm.InvoiceID).ToList();
                    //foreach(var item in db.InvoiceDetails.ToList())
                    //{
                    //    var tempVM = new InvoiceViewModel();
                    //    tempVM.Id = item.Id;
                    //    tempVM.InvoiceID = item.InvoiceID;
                    //    tempVM.Rate = item.Rate;
                    //    tempVM.Quantity = item.Quantity;
                    //    tempVM.Total = item.Total;
                    //}
                }
                else
                {

                }
            }
            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", vm.PartyID);
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", vm.ProductID);
            return View(vm);
        }
        [HttpPost]
        public JsonResult GetRate(int selectedValue)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var rate = db.ProductRates.Where(i => i.ProductID == selectedValue).ToList();
            var temp = Json(rate);
            return temp;
        }
        [HttpPost]
        public JsonResult GetProduct(int selectedValue)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //var products = db.AssignParties.Where(i => i.PartyID == selectedValue).ToList();
            var products = (from t1 in db.Products
                            join t2 in db.AssignParties on t1.Id equals t2.ProductID
                            join t3 in db.ProductRates on t1.Id equals t3.ProductID
                            where t2.PartyID == selectedValue
                            select t1).ToList();
            ViewBag.ProductID = new SelectList(products, "Id", "ProductName");
            var temp = Json(products);
            return temp;
        }
        public void UpdateGrandTotal(Invoice invm , decimal total)
        {
            invm.GrandTotal += total;
            db.Invoices.AddOrUpdate(invm);
            db.SaveChanges();
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", invoice.PartyID);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartyID,GrandTotal")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", invoice.PartyID);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
