using System;
using System.Linq;
using System.Web.Mvc;
using CommonWeal.Data;


namespace CommonWeal.NGOWeb.Controllers.AuthUser
{
    [AllowAnonymous]
    public class UserRegistrationController : BaseController
    {
        
        // GET: /userRegistration/
        public ActionResult CreateUserPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(RegisteredUser ru)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*using keyword will automatically dispose objects which are not referenced or in use*/
                    using(CommonWealEntities context = new CommonWealEntities())
                    {
                    /*first we update user table than registered user table to maintain referential integrity */
                    User obj = new User();
                    obj.LoginPassword = ru.UserPassword;
                    obj.LoginEmailID = ru.UserEmail.ToLower();
                    var roleobj = context.RoleTypes.Where(w => w.RoleName == "USER").FirstOrDefault();
                    obj.LoginUserType = roleobj.RoleID;
                    obj.IsActive = true;/*user is by default active*/ 
                    obj.IsBlock = false;/*to block user.presently user is not blocked*/
                    obj.ModifiedOn = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    context.Users.Add(obj); /*add user to users table*/
                    context.SaveChanges();
                    /*to add user to RegisteredUser table that contain refrfence key*/
                    ru.LoginID = obj.LoginID;
                    ru.UserEmail = ru.UserEmail.ToLower();
                    ru.LoginUserType = roleobj.RoleID; 
                    context.RegisteredUsers.Add(ru);
                    context.SaveChanges();
                    /*the form type is ajaxform  that's why return type should be (return JavaScript) */
                    return JavaScript("window.location = '" + Url.Action("Index", "Welcome") + "'");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            /*the form type is ajaxform  that's why return type should be (return JavaScript) */
            return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
        }
         
        /*to identify email address uniquely through ajax on registration*/
        public JsonResult checkEmail(string UserEmail)
        {
            CommonWealEntities context = new CommonWealEntities();
            return Json(!context.Users.Any(x => x.LoginEmailID == UserEmail), JsonRequestBehavior.AllowGet);
        }
    }

}

