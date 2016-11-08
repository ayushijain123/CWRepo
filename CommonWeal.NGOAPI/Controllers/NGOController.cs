using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CommonWeal.Data;


namespace CommonWeal.NGOAPI.Controllers
{
 
    public class NGOController :BaseController
    {

        [Authorize(Roles= "User")]
        [HttpGet]
        public HttpResponseMessage GetPosts()
        {
            CommonWealEntities db = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, db.NGOPosts);
            return response;
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public bool PostTest(Test objTest)
        {
            CommonWealEntities db = new CommonWealEntities();
            db.Tests.Add(objTest);
            db.SaveChanges();
            return true;
        }
    }
}
