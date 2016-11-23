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
                /*using keyword will automatically dispose objects which are not referenced or in use*/
                using (CommonWealEntities context = new CommonWealEntities())
                {
                    /*user can enter email in any case(upper/lower )*/
                    user.LoginEmailID = user.LoginEmailID.ToLower();

                    //authentication using API 
                  //  APIHelper helper = new APIHelper();

                   // helper.Login(user.LoginEmailID.ToLower(), user.LoginPassword, "password");

                    // need to modify for exception and false handling cases
                    /*check for email and password validity from User table*/
                    var result = context.Users.Where(usr =>usr.LoginEmailID.Equals(user.LoginEmailID)&& usr.LoginPassword.Equals(user.LoginPassword)).FirstOrDefault();
                    if (result == null)
                    {
                        ModelState.AddModelError("", "Invalid Email or Password");
                    }
                        /*if email is valid and user is blocked or not active*/
                    else if (!result.IsActive || result.IsBlock)
                    {
                        ModelState.AddModelError("", "Your request status is pending or blocked");
                    }
                        /*email and password are valid and user is no active and not blocked*/
                    else if (result.LoginEmailID.ToLower().Equals(user.LoginEmailID) && result.LoginPassword.Equals(user.LoginPassword))
                    {
                        /*controller name will set dynamically based on usertype  */
                        string controllerName = "";
                        /*typecasting of loginUserType to enum helper usertype*/
                        EnumHelper.UserType usertype = (EnumHelper.UserType)result.LoginUserType;
                        string roles = usertype.ToString();
                        /*enum helper is defined in base controller*/
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

                        /*authentication ticket will be generated on browser which contain LoginID and UserRole  */
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, result.LoginID.ToString(), //user id

                            DateTime.Now, DateTime.Now.AddMinutes(20),  // expiry
                            false,  //do not remember
                            roles, //role in userdata
                            "/");
                        /*to add ticket to cookie in browser*/
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                    FormsAuthentication.Encrypt(authTicket));
                        Response.Cookies.Add(cookie);
                        /*the page will redirect to index metod of "controller name" which is decided dynamically*/
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
            APIHelper<User>.LogOut();
            return RedirectToAction("Index", "Home");

        }

    }
}
