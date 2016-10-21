using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin_NGO.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Active_Users()
        {
            return View();
        }

        public ActionResult Blocked_Users()
        {
            return View();
        }

        public ActionResult Requests()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}
