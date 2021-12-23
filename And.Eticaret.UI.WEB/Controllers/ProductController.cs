using And.Eticaret.Core.Model1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.Eticaret.UI.WEB.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [Route("urun/{title}/{id}")]
        public ActionResult Detail(string title,int id)
        {
            var db = new AndDB();
            var prod = db.Products.Where(x => x.ID == id).FirstOrDefault();

           
            return View(prod);
        }
    }
}