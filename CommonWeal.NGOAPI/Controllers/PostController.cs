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
    public class PostController : BaseController
    {

        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage PostAll()
        {

            CommonWealEntities db = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, db.NGOPosts);
            return response;

        }

        //[HttpGet]
        //public PostComment Post(PostComment postcmnt)
        //{

        //    CommonWealEntities db = new CommonWealEntities();
        //    var response = db.PostComments.Find(postcmnt.PostID);
        //    if (response == null)
        //    {

        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));


        //    }
        //    return response;

        //}

        [HttpPost]
        public bool PostTest(PostComment objTest)
        {
            CommonWealEntities db = new CommonWealEntities();
            db.PostComments.Add(objTest);
            db.SaveChanges();
            return true;
        }


    }
}
