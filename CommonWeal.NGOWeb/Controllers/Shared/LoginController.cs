using CommonWeal.Data;
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
                using (CommonWealEntities context = new CommonWealEntities())
                {
                    string userEMail = user.LoginEmailID.ToLower();
                    var result = context.Users.Where(usr => userEMail == usr.LoginEmailID.ToLower() && usr.LoginPassword == user.LoginPassword).FirstOrDefault();
                    if (result == null)
                    {
                        ModelState.AddModelError("", "Invalid Email or Password");
                    }
                    else if (!result.IsActive || result.IsBlock)
                    {
                        ModelState.AddModelError("", "Your request status is pending or blocked");
                    }
                    else if (result.LoginEmailID.ToLower() == userEMail && result.LoginPassword == user.LoginPassword)
                    {
                        string controllerName = "";
                        EnumHelper.UserType usertype = (EnumHelper.UserType)result.LoginUserType;
                        string roles = usertype.ToString();
                        switch (usertype)
                        {
                            case EnumHelper.UserType.NGOAdmin:
                                controllerName = "NGOHome";
                                break;
                            case EnumHelper.UserType.Admin:
                                controllerName = "Admin";


                                break;
                            case EnumHelper.UserType.User:
                                controllerName = "UserHome";
                                break;
                            default:
                                break;
                        }
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.LoginEmailID, //user id



                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, result.LoginEmailID, //user id

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
            }
                return View();
            
            }
            
        

        [Authorize]
        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            //Session.Clear();
            Session.Abandon();
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Home");

        }

    }
}
