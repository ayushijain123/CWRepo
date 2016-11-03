﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonWeal.NGOWeb;
using CommonWeal.NGOWeb.ViewModel;
using CommonWeal.NGOWeb.Utility;

namespace CommonWeal.NGOWeb.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public LoggedInUser LoginUser { get; set; }
        /// <summary>
        /// Fetch logged in user detail
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                CommonWealEntities1 CWContext = new CommonWealEntities1();

                var usr = CWContext.Users.Where(user => user.LoginEmailID.ToLower() == HttpContext.User.Identity.Name.ToLower() && user.IsActive && !user.IsBlock).FirstOrDefault();


                this.LoginUser = new ViewModel.LoggedInUser()
                {
                    LoginID = usr.LoginID,
                    LoginEmailID = usr.LoginEmailID,
                    LoginUserType = usr.LoginUserType,


                };


                switch ((TypeHelper.UserType)usr.LoginUserType)
                {
                    case TypeHelper.UserType.Admin:
                        this.LoginUser.UserName = "Admin";
                        break;
                    case TypeHelper.UserType.NGOAdmin:

                        this.LoginUser.UserName = CWContext.NGOUsers.Where(user => user.LoginID == this.LoginUser.LoginID).FirstOrDefault().NGOName; ;
                        break;
                    case TypeHelper.UserType.User:
                        var reguser = CWContext.RegisteredUsers.Where(user => user.LoginID == this.LoginUser.LoginID).FirstOrDefault(); ;
                        this.LoginUser.UserName = reguser.FirstName + " " + reguser.LastName;
                        break;
                }


                ViewBag.LoginUser = LoginUser;
            }

        }
        /// <summary>
        /// Create Context and set role for authenticated user
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);


            HttpCookie authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket =
                                            FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new Char[] { ',' });
                //set roles and recreate context user
                GenericPrincipal userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);

                HttpContext.User = userPrincipal;

            }

        }


    }
}
