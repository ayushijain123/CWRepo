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
        public class Delete
        {
            public int PostID { get; set; }
            public int CommentID { get; set; }
        }

    [HttpPost]
        public bool DeleteCommentOnPost(Delete delete)
        {
            int id = delete.CommentID;
            bool result = false;
            CommonWealEntities context = new CommonWealEntities();
            context.Configuration.ValidateOnSaveEnabled = false;
            var res2 = context.PostComments.Where(a => a.CommentID == id).FirstOrDefault();
            if (res2 != null)
            {

                context.PostComments.Remove(res2);
                context.SaveChanges();
                result = true;
                return result;
            }

           
            return result;

        }

        [HttpPost]
        public bool deletePost(Delete delete)
        {
            int ID = delete.PostID;
            bool result = false;
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                context.Configuration.ValidateOnSaveEnabled = false;
                var res1 = context.PostLikes.Where(a => a.PostID == ID).ToList();
                if (res1 != null)
                {
                    foreach (var item in res1)
                    {
                        context.PostLikes.Remove(item);
                    }

                }
                var res2 = context.PostComments.Where(a => a.PostID == ID).ToList();
                if (res2 != null)
                {
                    foreach (var item in res2)
                    {
                        context.PostComments.Remove(item);
                    }
                }
                var res3 = context.PostCategories.Where(a => a.PostID == ID).ToList();
                if (res3 != null)
                {
                    foreach (var item in res3)
                    {
                        context.PostCategories.Remove(item);
                    }

                }
                var res = context.NGOPosts.Where(a => a.PostID == ID).FirstOrDefault();
                if (res != null)
                {
                    context.NGOPosts.Remove(res);
                }
                context.SaveChanges();
                result = true;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
         
            return result;
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
            //jfsdklfjklj
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
