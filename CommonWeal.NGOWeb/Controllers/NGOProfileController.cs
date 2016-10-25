using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CommonWeal.NGOWeb.Controllers
{
    public class NGOProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            int res = Convert.ToInt32(Session["UserID"]);
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
