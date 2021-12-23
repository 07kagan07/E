using And.Eticaret.Core.Model1;
using And.Eticaret.Core.Model1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.Eticaret.UI.WEB.Controllers
{

    public class HomeController : AndContollerBase
    {
        AndDB db = new AndDB();
        // GET: Home
        
        public ActionResult Index()
        {
            ViewBag.Islogin = this.IsLogin;
            return View(db.Products.ToList());
        }

        public PartialViewResult GetMenu()
        {
           
            var menus = db.Categories.Where(x => x.ParentID == 0 && x.IsActive==true).ToList();
            return PartialView(menus);
        }
        [Route("UyeGiris")]
        public PartialViewResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        [Route("UyeGiris")]
        public ActionResult Login(string Email,string Password)
        {
            var isactive = db.Users.Where(x => x.IsActive==false).ToList();
            var users = db.Users.Where(x => x.Email == Email 
            && x.Password == Password
            &&x.IsActive==true
            &&x.IsAdmin==false).ToList();
            if (users.Count == 1)
            {
                //giris onay
                Session["LoginUserID"] = users.FirstOrDefault().ID;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else if(isactive.Count==1)
            {
                ViewBag.ban = "Üyeliğiniz Geçici Süreliğine Engellenmiştir";
                return View();
            }
            else {

                ViewBag.Error = "Hatalı Kullanıcı ve ya Şifre";
            return View();
            }
        }
        [Route("uyekayit")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("uyekayit")]
        public ActionResult Register(User entity)
        {
            try { 
            entity.CreateDate = DateTime.Now;
            entity.CreateUserID = 1;
            entity.IsActive = true;
            entity.IsAdmin = false;
            entity.TCKN = "0";
            db.Users.Add(entity);
            db.SaveChanges();
            return Redirect("/");
            }
            catch { 
            return View();
            }
        }
        [Route("cik")]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect("/");
        }




    }   
}