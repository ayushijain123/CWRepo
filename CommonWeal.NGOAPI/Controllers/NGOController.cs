using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.NGOWeb;
 

namespace CommonWeal.NGOAPI.Controllers
{
    public class NGOController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get()
        {
            CommonWealEntities1 db = new CommonWealEntities1();
            var objTest = db.Tests.Where(x => x.ID == 1).FirstOrDefault();
            var response = Request.CreateResponse(HttpStatusCode.OK, objTest);
            return response;
        }

        [HttpPost]
        public bool PostTest(Test objTest)
        {
            CommonWealEntities1 db = new CommonWealEntities1();
            db.Tests.Add(objTest);
            db.SaveChanges();
            return true;
        }
    }
}
