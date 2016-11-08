using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.Data;

namespace CommonWeal.NGOAPI.Controllers
{
    public class LoginController :BaseController
    {
        
        [HttpGet]
        public HttpResponseMessage loginget()
        {
            CommonWealEntities db = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK,db.Users);
            return response;
        }

        [HttpGet]
        public User userlogin(string userid)
        {
            CommonWealEntities db = new CommonWealEntities();
            var response = db.Users.Find( Convert.ToInt32( userid));
            if (response == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound)); ;

            }
            return response;
        }
        [HttpPost]
        public HttpResponseMessage loginpost(User usr) {
            CommonWealEntities db = new CommonWealEntities();
            if (ModelState.IsValid)
            {
                db.Users.Add(usr);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, usr);
                return response;
            }
            else {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            
            }
 
        
        }


    }
}
