using And.Eticaret.Core.Model1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class CategoryController : AndContollerBase
    {
        // GET: Category
        [Route("kategori/{isim}/{id}")]
        public ActionResult Index(string isim,int id)
        {
            var db = new AndDB();
            var data = db.Products.Where(x =>x.IsActive==true &&x.CategoryID == id).ToList();
            ViewBag.ktgr = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
    }
}