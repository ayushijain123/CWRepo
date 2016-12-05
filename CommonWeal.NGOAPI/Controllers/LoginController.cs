using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonWeal.Data;

namespace CommonWeal.NGOAPI.Controllers
{
    public class LoginController : ApiController
    {
        
        [HttpGet]
        public HttpResponseMessage loginget()
        {
            CommonWealEntities db = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, db.Users);
            return response;
        }

        public class UserDetails
        {
            public int LoginID { get; set; }
            public int LoginUserType { get; set; }
            public bool IsActive { get; set; }
            public bool IsBlock { get; set; }
            public string ProfileName { get; set; }
            public string EmailID { get; set; }
            public string UserPassword { get; set; }
        }

        [AllowAnonymous]
        //[Route("api/Login/userlogin")]
        [HttpPost]
        public HttpResponseMessage userlogin(UserDetails objUserDetail)
        {
            CommonWealEntities db = new CommonWealEntities();
            var response = db.Users.Where(a => a.LoginEmailID == objUserDetail.EmailID && a.LoginPassword==objUserDetail.UserPassword).FirstOrDefault();
            if (response == null)
            {
                HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.NotFound,"Invalid UserID");
                return resp;
            }
            objUserDetail.LoginID = response.LoginID;
            objUserDetail.LoginUserType = response.LoginUserType;
            objUserDetail.IsActive = response.IsActive;
            objUserDetail.IsBlock = response.IsBlock;
            if (objUserDetail.LoginUserType == 1)
            {
                objUserDetail.ProfileName = response.NGOUsers.Where(a => a.LoginID == response.LoginID).FirstOrDefault().NGOName;
            }
            else if (objUserDetail.LoginUserType == 3)
            {
                objUserDetail.ProfileName = response.RegisteredUsers.Where(a => a.LoginID == response.LoginID).FirstOrDefault().FirstName;
            }
            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, objUserDetail);
            return res;
        }

        [HttpPost]
        public HttpResponseMessage loginpost(User usr)
        {
            CommonWealEntities db = new CommonWealEntities();
            if (ModelState.IsValid)
            {
                db.Users.Add(usr);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, usr);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
