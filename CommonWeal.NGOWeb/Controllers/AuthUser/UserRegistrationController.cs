using System;
using System.Linq;
using System.Web.Mvc;
using CommonWeal.Data;


namespace CommonWeal.NGOWeb.Controllers.AuthUser
{
    [AllowAnonymous]
    public class UserRegistrationController : BaseController
    {
        //
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
                    CommonWealEntities context = new CommonWealEntities();
                    User obj = new User();
                    obj.LoginPassword = ru.UserPassword;
                    obj.LoginEmailID = ru.UserEmail;
                    var roleobj = context.RoleTypes.Where(w => w.RoleName == "USER").FirstOrDefault();
                    obj.LoginUserType = roleobj.RoleID;
                    obj.IsActive = true;
                    obj.IsBlock = false;
                    obj.ModifiedOn = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    context.Users.Add(obj);
                    context.SaveChanges();
                    ru.LoginID = obj.LoginID;
                    ru.LoginUserType = 3; // Added by Rishiraj  on 24/10/2016
                    context.RegisteredUsers.Add(ru);
                    context.SaveChanges();
                    ViewData["msg"] = "<script>alert('Registered succesfully');</script>";
                    return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
                    //return RedirectToAction("Index", new { controller = "Home", area = string.Empty });
                    //return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            // return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });
            return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");

        }

        public JsonResult checkEmail(string UserEmail)
        {
            CommonWealEntities context = new CommonWealEntities();
            return Json(!context.Users.Any(x => x.LoginEmailID == UserEmail), JsonRequestBehavior.AllowGet);
        }
    }

}

