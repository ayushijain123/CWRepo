using NGOUserPage;
using System.Linq;
using System.Web.Mvc;
namespace Comonweal.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin obj)
        {
            CommonWealEntities context = new CommonWealEntities();
            var result = context.UserLogins.Where(w => w.LoginEmailID == obj.LoginEmailID).FirstOrDefault();
            if (result == null)
            {
                return View();
            }
            else if (result.LoginEmailID != null && result.LoginPassword != null)
            {
                if (result.LoginEmailID == obj.LoginEmailID && result.LoginPassword == obj.LoginPassword)
                {
                    if (result.IsActive == true && result.IsBlock == false)
                    {
                        if (result.LoginUserType == 1 || result.LoginUserType == 3)
                        { //login for ngo and user
                            return RedirectToAction("Index", "welcome");
                        }
                        else
                        {
                            //login for admin
                        }
                    }
                    else if (result.IsActive == false && result.IsBlock == false)
                    {

                        //redirect to login page
                        //request is pending for admin portal
                    }
                    else if (result.IsActive == true && result.IsBlock == true)
                    {
                        //redirect to login page
                        //all user are blocked
                    }
                }
                else
                {
                    Response.Write("<script>alert('check email and password'</script>)");
                }
            }

            return View();
        }

    }
}
