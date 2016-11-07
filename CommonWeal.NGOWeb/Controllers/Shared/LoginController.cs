﻿using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace CommonWeal.NGOWeb.Controllers.Shared
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
            {
          

            if (ModelState.IsValid)
            {


                CommonWealEntities context = new CommonWealEntities();

                string userEMail = user.LoginEmailID.ToLower();

                var result = context.Users.Where(usr => userEMail == usr.LoginEmailID.ToLower() && usr.LoginPassword == user.LoginPassword).FirstOrDefault();
                if (result == null)
                {

                    ModelState.AddModelError("", "Invalid Email or Password");

                }

                else if (!result.IsActive || result.IsBlock)
                {

                    //redirect to login page
                    //request is pending for admin portal

                    ModelState.AddModelError("", "your request status is pending or blocked");
                }
                else if (result.LoginEmailID.ToLower() == userEMail && result.LoginPassword == user.LoginPassword)
                {
                    string controllerName = "";


                    TypeHelper.UserType usertype = (TypeHelper.UserType)result.LoginUserType;

                    string roles = usertype.ToString();
                    switch (usertype)
                    {
                        case TypeHelper.UserType.NGOAdmin:
                            controllerName = "NGOHome";
                            break;
                        case TypeHelper.UserType.Admin:
                            controllerName = "Admin";

                            break;
                        case TypeHelper.UserType.User:
                            controllerName = "UserHome";
                            break;
                        default:
                            break;
                    }

                    

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.LoginEmailID, //user id
                        DateTime.Now, DateTime.Now.AddMinutes(20),  // expiry
                      false,  //do not remember
                      roles, //role in userdata
                      "/");

                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                       FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);


                    return RedirectToAction("Index", controllerName);

                }


            }

            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

    }
}
