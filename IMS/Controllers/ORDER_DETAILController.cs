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
    public class ORDER_DETAILController : Controller
    {
        private Inventory_Management_SystemEntities db = new Inventory_Management_SystemEntities();

        // GET: ORDER_DETAIL
        public ActionResult Index()
        {
            var oRDER_DETAIL = db.ORDER_DETAIL.Include(o => o.ORDER).Include(o => o.PAYMENT).Include(o => o.PRODUCT);
            return View(oRDER_DETAIL.ToList());
        }

        // GET: ORDER_DETAIL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_DETAIL oRDER_DETAIL = db.ORDER_DETAIL.Find(id);
            if (oRDER_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_DETAIL);
        }

        // GET: ORDER_DETAIL/Create
        public ActionResult Create()
        {
            ViewBag.Order_ID = new SelectList(db.ORDERs, "Order_ID", "Order_ID");
            ViewBag.Bill_Number = new SelectList(db.PAYMENTs, "Bill_Number", "Payment_Type");
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name");
            return View();
        }

        // POST: ORDER_DETAIL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Detail_ID,Unit_Price,Quantity,Discount,Date,UpdatedDate,Product_ID,Order_ID,Bill_Number")] ORDER_DETAIL oRDER_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.ORDER_DETAIL.Add(oRDER_DETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Order_ID = new SelectList(db.ORDERs, "Order_ID", "Order_ID", oRDER_DETAIL.Order_ID);
            ViewBag.Bill_Number = new SelectList(db.PAYMENTs, "Bill_Number", "Payment_Type", oRDER_DETAIL.Bill_Number);
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name", oRDER_DETAIL.Product_ID);
            return View(oRDER_DETAIL);
        }

        // GET: ORDER_DETAIL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_DETAIL oRDER_DETAIL = db.ORDER_DETAIL.Find(id);
            if (oRDER_DETAIL == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_ID = new SelectList(db.ORDERs, "Order_ID", "Order_ID", oRDER_DETAIL.Order_ID);
            ViewBag.Bill_Number = new SelectList(db.PAYMENTs, "Bill_Number", "Payment_Type", oRDER_DETAIL.Bill_Number);
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name", oRDER_DETAIL.Product_ID);
            return View(oRDER_DETAIL);
        }

        // POST: ORDER_DETAIL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Detail_ID,Unit_Price,Quantity,Discount,Date,UpdatedDate,Product_ID,Order_ID,Bill_Number")] ORDER_DETAIL oRDER_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER_DETAIL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_ID = new SelectList(db.ORDERs, "Order_ID", "Order_ID", oRDER_DETAIL.Order_ID);
            ViewBag.Bill_Number = new SelectList(db.PAYMENTs, "Bill_Number", "Payment_Type", oRDER_DETAIL.Bill_Number);
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name", oRDER_DETAIL.Product_ID);
            return View(oRDER_DETAIL);
        }

        // GET: ORDER_DETAIL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_DETAIL oRDER_DETAIL = db.ORDER_DETAIL.Find(id);
            if (oRDER_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_DETAIL);
        }

        // POST: ORDER_DETAIL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDER_DETAIL oRDER_DETAIL = db.ORDER_DETAIL.Find(id);
            db.ORDER_DETAIL.Remove(oRDER_DETAIL);
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
