using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    public class AreaOfInterestController : Controller
    {
        //
        // GET: /AreaOfInterest/

        public ActionResult Index()
        {

            CommonWealEntities context = new CommonWealEntities();
            var item = context.AreaOfInterests.ToList();
            return View(item);
        }

    }
}
