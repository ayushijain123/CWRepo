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

        //static async Task RunAsync()
        //{
        //    // New code:
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:61504/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    Task.WaitAll();

        //    Console.ReadLine();
        //}
        //[HttpPost]
        //static async Task<Uri> CreateUser(RegisteredUser ru)
        //{
        //    using (CommonWealEntities context = new CommonWealEntities())
        //    {
        //        HttpClient client = new HttpClient();
        //        HttpResponseMessage response = await client.PostAsJsonAsync("api/UserRegistration/CreateUser", ru);
        //        response.EnsureSuccessStatusCode();

        //        // Return the URI of the created resource.
        //        return response.Headers.Location;
        //    }
        //}

        [HttpPost]
        public ActionResult CreateUser(RegisteredUser ru)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var res = Task.Run(() => APIHelper<string>.PostJson("UserRegistration/CreateUser", ru));

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

