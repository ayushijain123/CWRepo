using Comonweal.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace NGOUserPage.Controllers
{
    public class userRegistrationController : Controller
    {
        //
        // GET: /userRegistration/
        public ActionResult CreateUserPost()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(RegisteredUser ru, RegisteredUserMeta objm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CommonWealEntities1 context = new CommonWealEntities1();
                    UserLogin obj = new UserLogin();
                    obj.LoginPassword = ru.UserPassword;
                    obj.LoginEmailID = ru.UserEmail;
                    var roleobj = context.RoleTypes.Where(w => w.RoleName == "USER").FirstOrDefault();
                    obj.LoginUserType = roleobj.RoleID;
                    obj.IsActive = true;
                    obj.IsBlock = false;
                    obj.ModifiedOn = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    context.UserLogins.Add(obj);
                    context.SaveChanges();
                    ru.LoginID = obj.LoginID;
                    ru.LoginUserType = 3; // Added by Rishiraj  on 24/10/2016
                    context.RegisteredUsers.Add(ru);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
            return RedirectToAction("CreateUserPost","userRegistration");
          
        }
    }

}

