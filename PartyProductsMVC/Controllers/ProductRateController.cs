using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PartyProductsMVC.DAL;
using PartyProductsMVC.Models;

namespace PartyProductsMVC.Controllers
{
    public class ProductRateController : Controller
    {
        private PartyProductContext db = new PartyProductContext();

        // GET: ProductRate
        public ActionResult Index()
        {
            var productRates = db.ProductRates.Include(p => p.Product);
            return View(productRates.ToList());
        }

        // GET: ProductRate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRate productRate = db.ProductRates.Find(id);
            if (productRate == null)
            {
                return HttpNotFound();
            }
            return View(productRate);
        }

        // GET: ProductRate/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName");
            return View();
        }

        // POST: ProductRate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductID,Rate,RowVersion")] ProductRate productRate)
        {
            if (ModelState.IsValid)
            {
                db.ProductRates.Add(productRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", productRate.ProductID);
            return View(productRate);
        }

        // GET: ProductRate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRate productRate = db.ProductRates.Find(id);
            if (productRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", productRate.ProductID);
            return View(productRate);
        }

        // POST: ProductRate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductID,Rate,RowVersion")] ProductRate productRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", productRate.ProductID);
            return View(productRate);
        }

        // GET: ProductRate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRate productRate = db.ProductRates.Find(id);
            if (productRate == null)
            {
                return HttpNotFound();
            }
            return View(productRate);
        }

        // POST: ProductRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductRate productRate = db.ProductRates.Find(id);
            db.ProductRates.Remove(productRate);
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
