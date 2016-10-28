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

       
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ConfirmOTP(string UserEmail) 
        {
            var EnteredEmail = UserEmail;
            CommonWealEntities1 context = new CommonWealEntities1();
            dbOperations obj = new dbOperations();
            var request = obj.RegisteredUserIsAccepted();
            var ob = context.RegisteredUsers.Where(w => w.UserEmail == EnteredEmail).FirstOrDefault();
            //var Email = ob.UserEmail;
            if (ob != null)
            {
                foreach (var EmailId in request)
                {
                    if
                        (EnteredEmail == EmailId.UserEmail.ToString())
                    {
                        //return( RedirectToAction("ForgotPasswordConfirm",EmailId));
                        //ViewBag.Email = UserEmail;
                        var Randomcode = obj.GenerateRandomPassword(7);
                        obj.SendActivationEmail(UserEmail, Randomcode);
                        ViewBag.code = Randomcode;
                        ForgotPassword entry = new ForgotPassword();
                        entry.EmailId = UserEmail;
                        entry.OTP = Randomcode;
                        entry.CreatedOn= System.DateTime.Now;
                        entry.ModifiedOn = System.DateTime.Now;
                        entry.CreatedBy = UserEmail;
                       // context.Users.Add(obj);
                        context.ForgotPasswords.Add(entry);
                        context.SaveChanges();
                        Session["FinalEmail"] = UserEmail;
                        Session["OTP"] = Randomcode;
                        return View();//"ConfirmPassword","Password"
                    }
                }
            }
            else
            {
                //TempData["EmailID"] = "<script>alert('EmailId doesnot exist please register');</script>";
                TempData["EmailID"] = "EmailId doesnot exist please register";
                return RedirectToAction("ForgotPassword", "Password");

            }
            return View();
        }

        //[HttpPost]
        public ActionResult ConfirmPassword(string FinalEmail,string OTP,string FinalOTP)
        {
            CommonWealEntities1 context = new CommonWealEntities1();
            dbOperations obj = new dbOperations();

            if (OTP == FinalOTP)
            {
                return View();
            }
            else
            {
                TempData["wrongOTP"] = "<script>alert('You have Entered Wrong OTP Please Enter Your Email');</script>";
                return RedirectToAction("ForgotPassword", "Password");
            }
            //var EnteredEmail = UserEmail;
            //CommonWealEntities1 context = new CommonWealEntities1();
            //dbOperations obj = new dbOperations();
            //var request = obj.RegisteredUserIsAccepted();
            //var ob = context.RegisteredUsers.Where(w => w.UserEmail == EnteredEmail).FirstOrDefault();
            ////var Email = ob.UserEmail;
            //if (ob != null)
            //    foreach (var EmailId in request)
            //    {
            //        if
            //            (EnteredEmail == EmailId.UserEmail.ToString())
            //        {
            //            //return( RedirectToAction("ForgotPasswordConfirm",EmailId));
            //            //ViewBag.Email = UserEmail;
            //            var Randomcode = obj.GenerateRandomPassword(7);
            //            obj.SendActivationEmail(UserEmail,Randomcode);
            //           ViewBag.code = Randomcode;
            //            Session["FinalEmail"] = UserEmail;
            //            Session["OTP"] = Randomcode;
            //            return View();
            //        }
            //    }
            //else
            //{
            //    //TempData["EmailID"] = "<script>alert('EmailId doesnot exist please register');</script>";
            //    TempData["EmailID"] = "EmailId doesnot exist please register";
            //    return RedirectToAction("ForgotPassword", "Password");

            //}
            return View();
        }

        public ActionResult UpdatePassword(string NewPassword, string ConfirmPassword, string FinalEmail)//,string ConfirmOTP, string OTP
        {
            //if (ConfirmOTP == OTP)
            //{
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
                    return RedirectToAction("Index", "Login");
                }
            //}


                else
                {
                    TempData["Wrong"] = "<script>alert('Please Enter Correct OTP');</script>";
                    return RedirectToAction("ForgotPassword", "Password", new { iid = FinalEmail });
                }
                return View();
           
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
