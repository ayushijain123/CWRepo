using CommonWeal.NGOWeb.Utility;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonWeal.Data;


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
        public ActionResult Login(User user)
        {


            CommonWealEntities context = new CommonWealEntities();

            string userEMail = user.LoginEmailID.ToLower();

            var result = context.Users.Where(usr => userEMail == usr.LoginEmailID.ToLower() && usr.LoginPassword == user.LoginPassword).FirstOrDefault();
            if (result == null)
            {
                //ViewBag.result = "Invalid username or password";
                //TempData["msg"] = "<script>alert('Invalid email or password');</script>";
               // ViewBag.FirstNameError = "Please write first name";
                //ViewData["Loginalert"] = "Invalid email or password";
                TempData["Success"] = "Invalid Email or Password";

                //ViewData["Error"] = "Error message text.";

                return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
                //TempData["msg"] = ("<script>alert('Invalid Email or Password');</script>");
                //return View();
            }
            else if (result.LoginEmailID != null && result.LoginPassword != null)
            {
                if (result.LoginEmailID.ToLower() == userEMail && result.LoginPassword == user.LoginPassword)
                {
                    if (result.IsActive == true && result.IsBlock == false)
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




                        //Authenticated user

                        //FormsAuthentication.SetAuthCookie(user.LoginEmailID,true);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.LoginEmailID, //user id
                            DateTime.Now, DateTime.Now.AddMinutes(20),  // expiry
                          false,  //do not remember
                          roles, //role in userdata
                          "/");

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                           FormsAuthentication.Encrypt(authTicket));
                        Response.Cookies.Add(cookie);


                        return JavaScript("window.location = '" + Url.Action("Index", controllerName) + "'");

                        //FormsAuthenticationTicket ticket = CreateAuthenticationTicket(userName, commaSeperatedRoles,createPersistentCookie, strCookiePath);
                        ////Encrypt the authentication ticket
                        //string encrypetedTicket = FormsAuthentication.Encrypt(ticket);
                        //if (!FormsAuthentication.CookiesSupported)
                        //{
                        //    //If the authentication ticket is specified not to use cookie, set it in the URL
                        //    FormsAuthentication.SetAuthCookie(encrypetedTicket, false);
                        //}
                        //else
                        //{
                        //    //If the authentication ticket is specified to use a cookie,
                        //    //wrap it within a cookie.
                        //    //The default cookie name is .ASPXAUTH if not specified
                        //    //in the <forms> element in web.config
                        //    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                        //    encrypetedTicket);
                        //    //Set the cookie's expiration time to the tickets expiration time
                        //    authCookie.Expires = ticket.Expiration;
                        //    //Set the cookie in the Response
                        //    HttpContext.Current.Response.Cookies.Add(authCookie);
                        //}



                        //FormsAuthentication.AttachRolesToUser();

                    }
                    else if (result.IsActive == false && result.IsBlock == false)
                    {

                        //redirect to login page
                        //request is pending for admin portal

                        TempData["pendingmsg"] = ("<script>alert('your request status is pending');</script>");
                    }
                    //else if (result.IsActive == true && result.IsBlock == true)
                    //{
                    //    //redirect to login page
                    //    //all user are blocked
                    //}

                    else
                    {
                        TempData["Loginmsg"] = ("<script>alert('Invalid Email or Password');</script>");
                    }

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
