using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS;

namespace IMS.Controllers
{
    public class SUPPLIERsController : Controller
    {
        private Inventory_Management_SystemEntities db = new Inventory_Management_SystemEntities();

        // GET: SUPPLIERs
        public ActionResult Index()
        {
            var sUPPLIERs = db.SUPPLIERs.Include(s => s.ORDER_DETAIL);
            return View(sUPPLIERs.ToList());
        }

        // GET: SUPPLIERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Create
        public ActionResult Create()
        {
            ViewBag.Order_Detail_ID = new SelectList(db.ORDER_DETAIL, "Order_Detail_ID", "Order_Detail_ID");
            return View();
        }

        // POST: SUPPLIERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Supplier_ID,First_Name,Last_Name,Phone,Email,Address,City,Province,Country,CreatedDate,UpdatedDate,Order_Detail_ID,Is_Supplied")] SUPPLIER sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                db.SUPPLIERs.Add(sUPPLIER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Order_Detail_ID = new SelectList(db.ORDER_DETAIL, "Order_Detail_ID", "Order_Detail_ID", sUPPLIER.Order_Detail_ID);
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_Detail_ID = new SelectList(db.ORDER_DETAIL, "Order_Detail_ID", "Order_Detail_ID", sUPPLIER.Order_Detail_ID);
            return View(sUPPLIER);
        }

        // POST: SUPPLIERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Supplier_ID,First_Name,Last_Name,Phone,Email,Address,City,Province,Country,CreatedDate,UpdatedDate,Order_Detail_ID,Is_Supplied")] SUPPLIER sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPPLIER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_Detail_ID = new SelectList(db.ORDER_DETAIL, "Order_Detail_ID", "Order_Detail_ID", sUPPLIER.Order_Detail_ID);
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // POST: SUPPLIERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            db.SUPPLIERs.Remove(sUPPLIER);
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
