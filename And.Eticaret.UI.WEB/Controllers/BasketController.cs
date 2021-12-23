using And.Eticaret.Core.Model1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class BasketController : AndContollerBase
    {
        // GET: Basket
        [HttpPost]
        public JsonResult AddProduct(int  productID,int quantitiy)
        {
            var db = new AndDB();
            db.Baskets.Add(new Core.Model1.Entity.Basket
            {
                CreateDate=DateTime.Now,
                CreateUserID=LoginUserID,
                ProductID=productID,
                Quantity=quantitiy,
                UserID=LoginUserID
            });
            var rt =db.SaveChanges();
            return Json(rt,JsonRequestBehavior.AllowGet);
        }

        [Route("Sepetim")]
        public  PartialViewResult Sepet()
        {
            var db = new AndDB();
            var data = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID);
            if (data.FirstOrDefault() ==null)
            {
                ViewBag.bos = "Boş";
            }
            return PartialView(data);
        }
        public ActionResult Delete(int id)
        {
            var db = new AndDB();
            var deleteitem = db.Baskets.Where(x => x.ID == id).FirstOrDefault();
            db.Baskets.Remove(deleteitem);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}