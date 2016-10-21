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
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(RegisteredUser ru, RegisteredUserMeta objm)
        {
            if (ModelState.IsValid)
            {
                CommonWealEntities context = new CommonWealEntities();
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
                context.RegisteredUsers.Add(ru);
                context.SaveChanges();
                Response.Write("<script>alert('Registration successfully')</script>");
            }
            return View();
        }
    }

}

