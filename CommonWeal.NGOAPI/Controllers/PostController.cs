using CommonWeal.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class PostController : BaseController
    {

        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage PostAll()
        {

            CommonWealEntities context = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, context.NGOPosts);
            return response;

        }
        [HttpGet]
        public HttpResponseMessage LikesGET()
        {

            CommonWealEntities context = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, context.PostLikes);
            return response;

        }
        [HttpGet]
        public HttpResponseMessage CommenttGET()
        {

            CommonWealEntities context = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, context.PostComments);
            return response;

        }
        [HttpPost]
        public HttpResponseMessage CommentsPOST(PostComment objPostComment)
        {
            CommonWealEntities context = new CommonWealEntities();
            context.PostComments.Add(objPostComment);
            context.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK, context.PostComments);
            return response;

        }
        [HttpPost]
        public HttpResponseMessage LikePOST(PostLike objPostLike)
        {
            CommonWealEntities context = new CommonWealEntities();
            var ob = context.PostLikes.Where(x => x.PostID == objPostLike.PostID).FirstOrDefault();
            if (objPostLike.IsLike != null)
            {
                if (ob.IsLike == true)
                {
                    ob.IsLike = false;
                }
                else
                {
                    ob.IsLike = true;
                }
            }
            else
            { ob.IsLike = true; }
            context.Configuration.ValidateOnSaveEnabled = false;

            context.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK, context.PostLikes);
            return response;

        }
        //[HttpGet]
        //public PostComment Post(PostComment postcmnt)
        //{

        //    CommonWealEntities context = new CommonWealEntities();
        //    var response = context.PostComments.Find(postcmnt.PostID);
        //    if (response == null)
        //    {

        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));


        //    }
        //    return response;

        //}

        //[HttpPost]
        //public bool PostTest(PostComment objTest)
        //{
        //    CommonWealEntities context = new CommonWealEntities();
        //    context.PostComments.Add(objTest);
        //    context.SaveChanges();
        //    return true;
        //}


    }
}
