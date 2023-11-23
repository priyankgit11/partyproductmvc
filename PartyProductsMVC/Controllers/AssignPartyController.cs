using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PartyProductsMVC.DAL;
using PartyProductsMVC.Models;

namespace PartyProductsMVC.Controllers
{
    public class AssignPartyController : Controller
    {
        private PartyProductContext db = new PartyProductContext();

        // GET: AssignParty
        public ActionResult Index()
        {
            var assignParties = db.AssignParties.Include(a => a.Party).Include(a => a.Product);
            return View(assignParties.ToList());
        }

        // GET: AssignParty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignParty assignParty = db.AssignParties.Find(id);
            if (assignParty == null)
            {
                return HttpNotFound();
            }
            return View(assignParty);
        }

        // GET: AssignParty/Create
        public ActionResult Create()
        {
            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName");
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName");
            return View();
        }

        // POST: AssignParty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartyID,ProductID,RowVersion")] AssignParty assignParty)
        {
            if (ModelState.IsValid)
            {
                db.AssignParties.Add(assignParty);
                db.SaveChanges();
                return RedirectToAction("Index");   
            }

            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", assignParty.PartyID);
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", assignParty.ProductID);
            return View(assignParty);
        }

        // GET: AssignParty/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignParty assignParty = db.AssignParties.Find(id);
            if (assignParty == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", assignParty.PartyID);
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", assignParty.ProductID);
            return View(assignParty);
        }

        // POST: AssignParty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "PartyID", "ProductID", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var assignPartyToUpdate = await db.AssignParties.FindAsync(id);
            if (assignPartyToUpdate == null)
            {
                AssignParty deletedAssignParty = new AssignParty();
                TryUpdateModel(deletedAssignParty, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The party was deleted by another user.");
                ViewBag.Id = new SelectList(db.AssignParties, "Id", "Id", deletedAssignParty.Id);
                ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", deletedAssignParty.PartyID);
                ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", deletedAssignParty.ProductID);
                return View(deletedAssignParty);
            }

            if (TryUpdateModel(assignPartyToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(assignPartyToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (AssignParty)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The party was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (AssignParty)databaseEntry.ToObject();

                        if (databaseValues.PartyID != clientValues.PartyID)
                            ModelState.AddModelError("PartyID", "Current value: "
                                + (db.Parties.Where(i => i.Id == databaseValues.PartyID).SingleOrDefault()).PartyName);
                        if (databaseValues.ProductID != clientValues.ProductID)
                            ModelState.AddModelError("ProductID", "Current value: "
                                + (db.Products.Where(i => i.Id == databaseValues.ProductID).SingleOrDefault()).ProductName);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        assignPartyToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.Id = new SelectList(db.AssignParties, "Id", "Id", assignPartyToUpdate.Id);
            ViewBag.PartyID = new SelectList(db.Parties, "Id", "PartyName", assignPartyToUpdate.PartyID);
            ViewBag.ProductID = new SelectList(db.Products, "Id", "ProductName", assignPartyToUpdate.ProductID);
            return View(assignPartyToUpdate);
        }

        // GET: AssignParty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignParty assignParty = db.AssignParties.Find(id);
            if (assignParty == null)
            {
                return HttpNotFound();
            }
            return View(assignParty);
        }

        // POST: AssignParty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignParty assignParty = db.AssignParties.Find(id);
            db.AssignParties.Remove(assignParty);
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
