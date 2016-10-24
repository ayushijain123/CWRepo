using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.NGOWeb;

namespace CommonWeal.NGOWeb.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Active_Users()
        {
            CommonWealEntities1 context = new CommonWealEntities1();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserAccepted();
            return View(request);
        }

        public ActionResult Blocked_Users()
        {
            return View();
        }

        public ActionResult Requests()
        {
            CommonWealEntities1 context = new CommonWealEntities1();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserNotAccepted();
            return View(request);
        }

        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult Accept(int id)
        {
            CommonWealEntities1 context = new CommonWealEntities1();
            //UserLogin UL = new UserLogin ();
            var ob = context.UserLogins.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsActive = true;
            var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
            ob1.IsActive = true;
            context.SaveChanges();
            return RedirectToAction("Requests", "Admin");

        }
        public ActionResult Block(int id)
        {


            CommonWealEntities1 context = new CommonWealEntities1();
            UserLogin  UL= new UserLogin();
            var ob = context.UserLogins.Where(w => w.LoginID== id).FirstOrDefault();
            ob.IsActive = false;
            ob.IsBlock = true;
            var ob1 = context.NGOUsers.Where(w => w.LoginID== id).FirstOrDefault();
            ob.IsActive = false;
            ob.IsBlock = true;
            context.SaveChanges();
            return RedirectToAction("Requests", "Admin");
        }
       

    }
}
