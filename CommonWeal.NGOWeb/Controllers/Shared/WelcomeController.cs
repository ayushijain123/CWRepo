using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers.Shared
{
     [AllowAnonymous]
    public class WelcomeController : BaseController
    {
        //
        // GET: /welcome/ Test This

        public ActionResult Index()
        {
            return View();
        }


        

    }
}
