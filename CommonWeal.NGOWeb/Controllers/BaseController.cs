
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CommonWeal.NGOWeb.Controllers
{
    [Authorize]

    public class BaseController : Controller
    {
        public User LoggedInUser { get; set; }
        /// <summary>
        /// Fetch logged in user detail
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            CommonWealEntities1 CWContext = new CommonWealEntities1();
            this.LoggedInUser = CWContext.Users.Where(user => user.LoginEmailID.ToLower() == HttpContext.User.Identity.Name.ToLower() && user.IsActive && !user.IsBlock).FirstOrDefault();


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
