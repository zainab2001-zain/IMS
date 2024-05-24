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
    public class PRODUCT_CATEGORYController : Controller
    {
        private Inventory_Management_SystemEntities db = new Inventory_Management_SystemEntities();

        // GET: PRODUCT_CATEGORY
        public ActionResult Index()
        {
            var pRODUCT_CATEGORY = db.PRODUCT_CATEGORY.Include(p => p.CATEGORY).Include(p => p.PRODUCT).Include(p => p.PRODUCT_DETAILS);
            return View(pRODUCT_CATEGORY.ToList());
        }

        // GET: PRODUCT_CATEGORY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CATEGORY pRODUCT_CATEGORY = db.PRODUCT_CATEGORY.Find(id);
            if (pRODUCT_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_CATEGORY);
        }

        // GET: PRODUCT_CATEGORY/Create
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.CATEGORies, "Category_ID", "Category_Name");
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name");
            ViewBag.Product_Detail_ID = new SelectList(db.PRODUCT_DETAILS, "Product_Detail_ID", "Product_Detail_ID");
            return View();
        }

        // POST: PRODUCT_CATEGORY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Category_ID,Product_ID,Category_ID,Product_Detail_ID")] PRODUCT_CATEGORY pRODUCT_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT_CATEGORY.Add(pRODUCT_CATEGORY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_ID = new SelectList(db.CATEGORies, "Category_ID", "Category_Name", pRODUCT_CATEGORY.Category_ID);
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name", pRODUCT_CATEGORY.Product_ID);
            ViewBag.Product_Detail_ID = new SelectList(db.PRODUCT_DETAILS, "Product_Detail_ID", "Product_Detail_ID", pRODUCT_CATEGORY.Product_Detail_ID);
            return View(pRODUCT_CATEGORY);
        }

        // GET: PRODUCT_CATEGORY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CATEGORY pRODUCT_CATEGORY = db.PRODUCT_CATEGORY.Find(id);
            if (pRODUCT_CATEGORY == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_ID = new SelectList(db.CATEGORies, "Category_ID", "Category_Name", pRODUCT_CATEGORY.Category_ID);
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name", pRODUCT_CATEGORY.Product_ID);
            ViewBag.Product_Detail_ID = new SelectList(db.PRODUCT_DETAILS, "Product_Detail_ID", "Product_Detail_ID", pRODUCT_CATEGORY.Product_Detail_ID);
            return View(pRODUCT_CATEGORY);
        }

        // POST: PRODUCT_CATEGORY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_Category_ID,Product_ID,Category_ID,Product_Detail_ID")] PRODUCT_CATEGORY pRODUCT_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT_CATEGORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(db.CATEGORies, "Category_ID", "Category_Name", pRODUCT_CATEGORY.Category_ID);
            ViewBag.Product_ID = new SelectList(db.PRODUCTs, "Product_ID", "Product_Name", pRODUCT_CATEGORY.Product_ID);
            ViewBag.Product_Detail_ID = new SelectList(db.PRODUCT_DETAILS, "Product_Detail_ID", "Product_Detail_ID", pRODUCT_CATEGORY.Product_Detail_ID);
            return View(pRODUCT_CATEGORY);
        }

        // GET: PRODUCT_CATEGORY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT_CATEGORY pRODUCT_CATEGORY = db.PRODUCT_CATEGORY.Find(id);
            if (pRODUCT_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT_CATEGORY);
        }

        // POST: PRODUCT_CATEGORY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT_CATEGORY pRODUCT_CATEGORY = db.PRODUCT_CATEGORY.Find(id);
            db.PRODUCT_CATEGORY.Remove(pRODUCT_CATEGORY);
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
