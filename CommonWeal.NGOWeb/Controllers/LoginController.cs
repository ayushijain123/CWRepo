using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;


namespace CommonWeal.NGOWeb.Controllers
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


            CommonWealEntities1 context = new CommonWealEntities1();
            var result = context.Users.Where(w => w.LoginEmailID == user.LoginEmailID).FirstOrDefault();
            if (result == null)
            {
                return View();
            }
            else if (result.LoginEmailID != null && result.LoginPassword != null)
            {

                if (result.LoginEmailID == user.LoginEmailID && result.LoginPassword == user.LoginPassword)
                {
                    if (result.IsActive == true && result.IsBlock == false)
                    {
                        string roles = "";
                        string controllerName = "";

                        if (result.LoginUserType == (int)Helper.UserType.Admin)
                        {                            
                            roles = "Admin";
                            controllerName = "Admin";

                        }
                        //Edited by abhijeet on 24/10/2016
                        if (result.LoginUserType == (int)Helper.UserType.User)
                        { //login for ngo and user
                            roles = "User";
                            controllerName = "welcome";

                        }
                        if (result.LoginUserType == (int)Helper.UserType.NGOAdmin)
                        {
                            Session["UserID"] = result.LoginID;
                            roles = "NGOAdmin";
                            controllerName = "NGOProfile";


                        }

                        //Authenticated user

                        //FormsAuthentication.SetAuthCookie(user.LoginEmailID,true);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.LoginEmailID, //user id
                            DateTime.Now, DateTime.Now.AddMinutes(20),  // expiry
                          false,  //do not remember
                          roles, //role
                          "/");

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                           FormsAuthentication.Encrypt(authTicket));
                        Response.Cookies.Add(cookie);


                        return RedirectToAction("Index", controllerName);

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
                    //else if (result.IsActive == false && result.IsBlock == false)
                    //{

                    //    //redirect to login page
                    //    //request is pending for admin portal
                    //}
                    //else if (result.IsActive == true && result.IsBlock == true)
                    //{
                    //    //redirect to login page
                    //    //all user are blocked
                    //}



                }
                else
                {
                    TempData["msg"] = ("<script>alert('Invalid Email or Password');</script>");
                }

            }

            return View();
        }

    }
}
