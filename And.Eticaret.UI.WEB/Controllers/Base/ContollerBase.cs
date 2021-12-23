using And.Eticaret.Core.Model1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.Eticaret.UI.WEB
{
    public class AndContollerBase : Controller
    {
        public bool IsLogin { get; private set; }
        public int LoginUserID { get; private set; }
        public User LoginUserEntity { get; private  set; }
        protected override void Initialize(RequestContext requestContext)
        {
            //TODO:Üye Girişi işlemleri sonrası değişecek  
            if (requestContext.HttpContext.Session["LoginUserID"]!=null)
            {
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserID"];
                LoginUserEntity=(Core.Model1.Entity.User)requestContext.HttpContext.Session["LoginUser"];

            }
            base.Initialize(requestContext);    
        }
    }
}