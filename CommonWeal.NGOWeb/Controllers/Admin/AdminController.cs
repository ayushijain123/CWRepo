﻿using CommonWeal.Data;
using System;
using System.Linq;
using System.Web.Mvc;


namespace CommonWeal.NGOWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminController : BaseController
    {
        dbOperations obj = new dbOperations();
        public ActionResult Index()
        {
            CommonWealEntities obj = new CommonWealEntities();
            var users = obj.NGOUsers;
            var CountOfRequests = users.Where(w => w.IsActive == false && w.IsBlock == false).Count();
            ViewBag.COR = CountOfRequests;
            var CountOfActiveUsers = users.Where(w => w.IsActive == true && w.IsBlock == false).Count();
            ViewBag.COAU = CountOfActiveUsers;
            var CountOfBlockedUsers = users.Where(w => w.IsBlock == true).Count();
            ViewBag.COBU = CountOfBlockedUsers;
            return View();

        }

        public ActionResult Active_Users()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserAccepted();
            return View(request);
        }

        public ActionResult Blocked_Users()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserBlocked();
            return View(request);
        }

        public ActionResult Requests()
        {
            CommonWealEntities context = new CommonWealEntities();
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

            try
            {
                CommonWealEntities context = new CommonWealEntities();
                //User UL = new User ();
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsActive = true;

                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();
                return RedirectToAction("Requests", "Admin");

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
        public ActionResult Block(int id)
        {

            try
            {
                CommonWealEntities context = new CommonWealEntities();
                User UL = new User();
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = false;
                ob.IsBlock = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsActive = false;
                ob1.IsBlock = true;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                return RedirectToAction("Active_Users", "Admin");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
        public ActionResult Unblock(int id)
        {


            CommonWealEntities context = new CommonWealEntities();
            User UL = new User();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsActive = true;
            ob.IsBlock = false;
            var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
            ob1.IsActive = true;
            ob1.IsBlock = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
            return RedirectToAction("Blocked_Users", "Admin");
        }


    }
}
