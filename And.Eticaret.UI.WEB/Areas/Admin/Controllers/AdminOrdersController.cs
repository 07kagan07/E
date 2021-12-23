using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using And.Eticaret.Core.Model1;
using And.Eticaret.Core.Model1.Entity;

namespace And.Eticaret.UI.WEB.Areas.Admin.Controllers
{
    public class AdminOrdersController : Controller
    {
        private AndDB db = new AndDB();

        // GET: Admin/AdminOrders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAdress).Include(o => o.OrderProducts).ToList();
            orders.Reverse();
            return View(orders.Where(o=>o.StatusID==3 || o.StatusID==2));
        }

        public PartialViewResult onayli()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAdress).Include(o => o.OrderProducts).ToList();
            orders.Reverse();
            return PartialView(orders.Where(o => o.StatusID == 4));
        }

        public PartialViewResult yolda()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAdress).Include(o => o.OrderProducts).ToList();
            orders.Reverse();
            return PartialView(orders.Where(o => o.StatusID == 5));
        }
        public PartialViewResult tamamlanan()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAdress).Include(o => o.OrderProducts).ToList();
            orders.Reverse();
            return PartialView(orders.Where(o => o.StatusID == 6));
        }

        public PartialViewResult red()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAdress).Include(o => o.OrderProducts).ToList();
            orders.Reverse();
            return PartialView(orders.Where(o => o.StatusID == 7));
        }

        // GET: Admin/AdminOrders/Details/5
        public ActionResult Details(int? id)
        {

            var data = db.Orders.Include("OrderProducts")
                .Include("OrderProducts.Product")
                .Include("OrderPayments")
                .Include("Status")
                .Include("UserAdress").Where(x => x.ID == id).FirstOrDefault();
            return View(data);



            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(order);


        }

        // GET: Admin/AdminOrders/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            ViewBag.UserAdressID = new SelectList(db.UserAdresses, "ID", "Title");
            return View();
        }

        // POST: Admin/AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,UserAdressID,StatusID,TotalProductPrice,TotalTaxPrice,TotalDiscount,TotalPrice,CreateDate,CreateUserID,UpdateDate,UpdateUserID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            ViewBag.UserAdressID = new SelectList(db.UserAdresses, "ID", "Title", order.UserAdressID);
            return View(order);
        }

        // GET: Admin/AdminOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            ViewBag.UserAdressID = new SelectList(db.UserAdresses, "ID", "Title", order.UserAdressID);
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,UserAdressID,StatusID,TotalProductPrice,TotalTaxPrice,TotalDiscount,TotalPrice,CreateDate,CreateUserID,UpdateDate,UpdateUserID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", order.StatusID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            ViewBag.UserAdressID = new SelectList(db.UserAdresses, "ID", "Title", order.UserAdressID);
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public ActionResult onayla(int id)
        {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 4;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = order.ID });
        }

        public ActionResult reddet(int id)
        {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 7;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = order.ID });
        }
        public ActionResult tam(int id)
        {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 6;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = order.ID });
        }
        public ActionResult yol(int id)
        {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 5;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = order.ID });
        }
    }
}
