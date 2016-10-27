using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers.Shared
{
    public class PasswordController : Controller
    {
        //
        // GET: /Password/

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        //[HttpPost]
        public ActionResult ConfirmPassword(string UserEmail)
        {
            var EnteredEmail = UserEmail;
            CommonWealEntities1 context = new CommonWealEntities1();
            dbOperations obj = new dbOperations();
            var request = obj.RegisteredUserIsAccepted();
            var ob = context.RegisteredUsers.Where(w => w.UserEmail == EnteredEmail).FirstOrDefault();
            //var Email = ob.UserEmail;
            if (ob != null)
                foreach (var EmailId in request)
                {
                    if
                        (EnteredEmail == EmailId.UserEmail.ToString())
                    {
                        //return( RedirectToAction("ForgotPasswordConfirm",EmailId));
                        //ViewBag.Email = UserEmail;
                        Session["FinalEmail"] = UserEmail;
                        return View();
                    }
                }
            else
            {
                //TempData["EmailID"] = "<script>alert('EmailId doesnot exist please register');</script>";
                TempData["EmailID"] = "EmailId doesnot exist please register";
                return RedirectToAction("ForgotPassword", "Password");

            }
            return View(request);
        }

        public ActionResult UpdatePassword(string NewPassword, string ConfirmPassword, string FinalEmail)
        {
            var EnteredEmail = FinalEmail;
            CommonWealEntities1 context = new CommonWealEntities1();
            dbOperations obj = new dbOperations();
            if (NewPassword == ConfirmPassword)
            {
                var request = obj.RegisteredUserIsAccepted();
                var ob = context.RegisteredUsers.Where(w => w.UserEmail == EnteredEmail).FirstOrDefault();
                ob.UserPassword = NewPassword;
                var LoginId = ob.LoginID;
                var Pooja = context.Users.Where(w => w.LoginID == LoginId).FirstOrDefault();
                Pooja.LoginPassword = NewPassword;
                context.SaveChanges();
                //Session["FinalEmail"] = null;
                Session.Remove("FinalEmail");
                //return Content("Password Changed Successfully please Login");
                TempData["msg"] = "<script>alert('Password Changed Successfully please Login');</script>";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["Wrong"] = "<script>alert('Password and ConfirmPassword do not match');</script>";
                return RedirectToAction("ForgotPassword", "Password", new { iid = EnteredEmail });
            }
           
        }


        public ActionResult ChangPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangPassword(int x)
        {
            return View();
        }

    }
}
