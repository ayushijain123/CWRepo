using CommonWeal.NGOWeb;
using System.Linq;
using System.Web.Mvc;



namespace CommonWeal.NGOWeb.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin obj)
        {
            CommonWealEntities1 context = new CommonWealEntities1();
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
                        if (result.LoginUserType == 2 && obj.LoginEmailID=="admin@gmail.com"&&obj.LoginPassword=="admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        //Edited by abhijeet on 24/10/2016
                        if (result.LoginUserType==3)
                        { //login for ngo and user
                            return RedirectToAction("Index", "welcome");
                        }
                        if (result.LoginUserType == 1)
                        {
                            Session["UserID"] = result.LoginID;
                            return RedirectToAction("Index", "NGOProfile");
                        }
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
                    Response.Write("<script>alert('Incorrect email or password'</script>)");
                }
               
            }

            return View();
        }

    }
}
