using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;using System.Text;using iTextSharp.text;using iTextSharp.text.pdf;using iTextSharp.text.html.simpleparser;
using System.Web.UI;

namespace IMS.Controllers
{
    public class ORDERsController : Controller
    {
        private Inventory_Management_SystemEntities db = new Inventory_Management_SystemEntities();

        // GET: ORDERs
        public ActionResult Index()
        {
            var oRDERs = db.ORDERs.Include(o => o.USER);
            return View(oRDERs.ToList());
        }

        // GET: ORDERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERs.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // GET: ORDERs/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.USERs, "ID", "First_Name");
            return View();
        }

        // POST: ORDERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_ID,Date_Of_Order,ID")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.ORDERs.Add(oRDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.USERs, "ID", "First_Name", oRDER.ID);
            return View(oRDER);
        }

        // GET: ORDERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERs.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.USERs, "ID", "First_Name", oRDER.ID);
            return View(oRDER);
        }

        // POST: ORDERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_ID,Date_Of_Order,ID")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.USERs, "ID", "First_Name", oRDER.ID);
            return View(oRDER);
        }

        // GET: ORDERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERs.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // POST: ORDERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDER oRDER = db.ORDERs.Find(id);
            db.ORDERs.Remove(oRDER);
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

        public string GetPrice(int productID)
        {
            var product = db.PRODUCTs.Find(productID);
            if (product != null)
            {
                var productDetails = db.PRODUCT_DETAILS.Where(pd => pd.ProductID == product.Product_ID).FirstOrDefault();
                if (productDetails != null)
                {
                    return productDetails.Product_Price.ToString();
                }
            }
            return "";
        }

        public string PlaceOrder(string accountNo, string PaymentType, int IsPaid, int[] Ids, int[] Qty)
        {
            try
            {
                if (Session["id"] != null)
                {
                    if (Ids.Length > 0)
                    {
                        var totalAmount = 0;
                        ORDER order = new ORDER();
                        order.Date_Of_Order = DateTime.Now;

                        order.ID = Convert.ToInt32(Session["id"].ToString()); // yahan pe aap ne login user ki id pass karni hain

                        db.ORDERs.Add(order);
                        db.SaveChanges();

                        for (int i = 0; i < Ids.Length; i++)
                        {
                            int id = Ids[i];
                            var product = db.PRODUCTs.Where(p => p.Product_ID == id).FirstOrDefault();
                            if (product != null)
                            {
                                var productDetails = db.PRODUCT_DETAILS.Where(pd => pd.ProductID == product.Product_ID).FirstOrDefault();
                                if (productDetails != null)
                                {
                                    totalAmount += productDetails.Product_Price * Qty[i];
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }
                        }

                        if (totalAmount > 0)
                        {
                            PAYMENT payment = new PAYMENT();
                            payment.Is_Paid = IsPaid == 1 ? true : false;
                            payment.Payment_Type = PaymentType;

                            payment.Account_Number = Convert.ToInt32(accountNo);
                            payment.Total_Amount = totalAmount;

                            db.PAYMENTs.Add(payment);
                            db.SaveChanges();

                            for (int i = 0; i < Ids.Length; i++)
                            {
                                int id = Ids[i];
                                var product = db.PRODUCTs.Where(p => p.Product_ID == id).FirstOrDefault();
                                if (product != null)
                                {
                                    var productDetails = db.PRODUCT_DETAILS.Where(pd => pd.ProductID == product.Product_ID).FirstOrDefault();
                                    if (productDetails != null)
                                    {
                                        ORDER_DETAIL orderDetail = new ORDER_DETAIL();
                                        orderDetail.Date = DateTime.Now;
                                        orderDetail.Product_ID = product.Product_ID;
                                        orderDetail.Quantity = Qty[i];
                                        orderDetail.Unit_Price = productDetails.Product_Price;
                                        orderDetail.Order_ID = order.Order_ID;
                                        orderDetail.Bill_Number = payment.Bill_Number;
                                        db.ORDER_DETAIL.Add(orderDetail);
                                        db.SaveChanges();
                                    }
                                }
                            }

                            return "success-"+ order.Order_ID;
                        }
                    }

                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public void GeneratePDF(int orderNo,string accountNo, string PaymentType, int IsPaid,string  Ids,string Qty)
        {
            var Idss = Ids.Split(',');
            var qtty = Qty.Split(',');

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { 
                        new DataColumn("Product", typeof(string)),
                        new DataColumn("Price", typeof(int)),
                        new DataColumn("Quantity",  typeof(int)),
                        new DataColumn("Total",  typeof(int))});
            
            for (int i = 0; i < Idss.Length -1 ; i++)
            {
                int id = Convert.ToInt32(Idss[i]);
                var product = db.PRODUCTs.Where(p => p.Product_ID == id).FirstOrDefault();
                if (product != null)
                {
                    var productDetails = db.PRODUCT_DETAILS.Where(pd => pd.ProductID == product.Product_ID).FirstOrDefault();
                    if (productDetails != null)
                    {
                        var total = productDetails.Product_Price * Convert.ToInt32(qtty[i]); 
                        dt.Rows.Add(product.Product_Name, productDetails.Product_Price, qtty[i], total);
                    }
                }
            }
            
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #1251cc' colspan = '2'><b>Order Information</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Order No: </b>");
                    sb.Append(orderNo);
                    sb.Append("</td><td align = 'right'><b>Date: </b>");
                    sb.Append(DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    sb.Append("<table border = '1'>");
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<th style = 'background-color: #0fe618;color:#000000'>");
                        sb.Append(column.ColumnName);
                        sb.Append("</th>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr><td align = 'right' colspan = '");
                    sb.Append(dt.Columns.Count - 1);
                    sb.Append("'>Total</td>");
                    sb.Append("<td>");
                    sb.Append(dt.Compute("sum(Total)", ""));
                    sb.Append("</td>");
                    sb.Append("</tr></table>");


                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + orderNo + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();

                }
            }

        }



    }
}
