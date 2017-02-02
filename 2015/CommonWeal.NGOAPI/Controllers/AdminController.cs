using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.Data;

namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseController
    {
        [HttpGet]
        public HttpResponseMessage Active_User()
        {
            Functions objfun = new Functions();
            var request = objfun.GetAllUserAccepted();           
            var response = Request.CreateResponse(HttpStatusCode.OK, request);
            return response;
        }
        [HttpGet]
        public HttpResponseMessage Requests()
        {
            Functions objfun = new Functions();
            var request = objfun.GetAllUserNotAccepted().OrderByDescending(x => x.CreatedOn);
            var response = Request.CreateResponse(HttpStatusCode.OK, request);
            return response;
        }
        [HttpGet]
        public HttpResponseMessage All_Users()
        {
            Functions objfun = new Functions();
            var request = objfun.AllUsers();
            var response = Request.CreateResponse(HttpStatusCode.OK, request);
            return response;
        }
        [HttpGet]
        public HttpResponseMessage Warned_NGOs()
        {
            Functions objfun = new Functions();
            var request = objfun.WarnedNGOs();
            var response = Request.CreateResponse(HttpStatusCode.OK, request);
            return response;
        }
    }
}
