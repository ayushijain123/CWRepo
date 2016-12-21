using System;
using System.Linq;
using System.Web.Mvc;
using CommonWeal.Data;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using CommonWeal.NGOWeb.Utility;


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
        public async Task<ActionResult> CreateUser(RegisteredUser ru)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await Task.Run(() => APIHelper<string>.PostJson("UserRegistration/CreateUser", ru));
                    return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
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

