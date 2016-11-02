using System;
using System.Linq;
using System.Web.Mvc;


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
                    CommonWealEntities1 context = new CommonWealEntities1();
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
                    TempData["Usermsg"] = ("<script>alert('User Registered successfully');</script>");
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
            CommonWealEntities1 context = new CommonWealEntities1();
            return Json(!context.RegisteredUsers.Any(x => x.UserEmail == UserEmail), JsonRequestBehavior.AllowGet);
        }
    }

}

