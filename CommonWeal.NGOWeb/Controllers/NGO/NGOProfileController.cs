using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;


namespace CommonWeal.NGOWeb.Controllers.NGO
{
    //[Authorize]
     [Authorize(Roles = "NGOAdmin")]
    public class NGOProfileController : BaseController
    {
        public ActionResult Index()
        {
            CommonWealEntities obj=new CommonWealEntities();
            //User UL = new User();
            var CountOfRequests=obj.Users.Where(w=>w.IsActive==false&& w.IsBlock==false); 
          //  ViewBag["CountOfRequests"]=CountOfRequests;
            return View();
        }
        public ActionResult AboutUs()
        {
            string res = this.User.Identity.Name;
            dbOperations obj = new dbOperations();
           var details=obj.GetNGODetails(res);
           if (details != null)
           {
             // ViewBag.value = details;
               return View(details);
           }
           return RedirectToAction("Login", "Login");
        }
        public ActionResult Album()
        {
            return View();
        }
    }
}
