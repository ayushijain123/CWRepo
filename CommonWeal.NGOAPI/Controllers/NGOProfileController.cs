using CommonWeal.Data;
using CommonWeal.NGOWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class NGOProfileController : BaseController
    {
        public IEnumerable<string> Get()
        {
           return new string[] { "ASJ", "AJ" };
       }
        public class AboutUs
        {
            public int LoginID { get; set; }
            public string NGOEmail { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string NGOName { get; set; }
            public string ChairmanName { get; set; }
            public string AboutNGO { get; set; }

        }
        [HttpPost]
        public HttpResponseMessage NGOPost(AboutUs loginid)
        {
            dbOperations db = new dbOperations();
            var res = db.GetPostById(loginid.LoginID);
            var response = Request.CreateResponse(HttpStatusCode.OK, res);
            return response;

        }
        [HttpPost]
        public HttpResponseMessage AboutNGO(AboutUs loginid)
        {
            CommonWealEntities context = new CommonWealEntities();
            var res = context.NGOUsers.Where(w => w.LoginID==loginid.LoginID).FirstOrDefault();
            loginid.NGOEmail = res.NGOEmailID;
            loginid.Mobile = res.Mobile;
            loginid.Address = res.NGOAddress;
            loginid.NGOName = res.NGOName;
            loginid.ChairmanName = res.ChairmanName;
            loginid.AboutNGO = res.AboutUs;
            var response = Request.CreateResponse(HttpStatusCode.OK, loginid);
            return response;
        }
        [HttpPost]
        public HttpResponseMessage UpdateNGO(AboutUs loginid)
        {
            CommonWealEntities context = new CommonWealEntities();
            if ( loginid.NGOEmail!=null && loginid.LoginID > 0 )
            {
                var ngodata = context.NGOUsers.Where(w => w.LoginID == loginid.LoginID).FirstOrDefault();
                ngodata.NGOEmailID = loginid.NGOEmail;
                ngodata.Mobile = loginid.Mobile;
                ngodata.NGOAddress = loginid.Address;
                ngodata.NGOName = loginid.NGOName;
                ngodata.ChairmanName = loginid.ChairmanName;
                ngodata.AboutUs = loginid.AboutNGO;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                var res = context.NGOUsers.Where(w => w.LoginID == loginid.LoginID).FirstOrDefault();
                loginid.NGOEmail = res.NGOEmailID;
                loginid.Mobile = res.Mobile;
                loginid.Address = res.NGOAddress;
                loginid.NGOName = res.NGOName;
                loginid.ChairmanName = res.ChairmanName;
                loginid.AboutNGO = res.AboutUs;
                var response = Request.CreateResponse(HttpStatusCode.OK, loginid);
                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, loginid);
                return response;
            }
            
        }
    }
}
