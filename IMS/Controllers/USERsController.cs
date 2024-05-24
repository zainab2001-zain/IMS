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
    public class USERsController : Controller
    {
        private Inventory_Management_SystemEntities db = new Inventory_Management_SystemEntities();

        // GET: USERs
        public ActionResult Index()
        {
            var uSERs = db.USERs.Include(u => u.ROLE);
            return View(uSERs.ToList());
        }

        // GET: USERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            SelectListItem si1 = new SelectListItem();
            si1.Text = "One";
            si1.Value = "1";
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });

            ViewBag.abc = new SelectList(li, "Value", "Text");
            ViewBag.Role_ID = new SelectList(db.ROLES, "Role_ID", "Role_Name");
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,First_Name,Last_Name,User_Name,Password,Address,Gender,Phone,Email,CNIC,IsActive,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy,Role_ID")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USERs.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_ID = new SelectList(db.ROLES, "Role_ID", "Role_Name", uSER.Role_ID);
            return View(uSER);
        }

        // GET: USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_ID = new SelectList(db.ROLES, "Role_ID", "Role_Name", uSER.Role_ID);
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,First_Name,Last_Name,User_Name,Password,Address,Gender,Phone,Email,CNIC,IsActive,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy,Role_ID")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_ID = new SelectList(db.ROLES, "Role_ID", "Role_Name", uSER.Role_ID);
            return View(uSER);
        }

        // GET: USERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USERs.Find(id);
            db.USERs.Remove(uSER);
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
