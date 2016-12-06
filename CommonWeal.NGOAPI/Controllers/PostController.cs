using CommonWeal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class PostController : BaseController
    {
        public class Comment
        {

            public string Username { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public string commentContent { get; set; }
            public int commentLike { get; set; }
            public string commentUserImage { get; set; }
            public int commentId { get; set; }

        }
        public class PostLikeModel
        {
            public string userName { get; set; }
            public string userImageUrl { get; set; }
            public int UserID { get; set; }
            public int LoginID{get;set;}
            public int PostID{get;set;}
            public bool like { get;set;}
        }
        public class Post
        {
            public int userId { get; set; }
            public int postId { get; set; }
            public string userName { get; set; }
            public string userImage { get; set; }
            public DateTime postCreateTime { get; set; }
            public string postImageUrl { get; set; }
            public int likeCount { get; set; }
            public int commentCount { get; set; }
            public string postcontent { get; set; }
            public List<PostLikeModel> postlike { get; set; }
            public List<Comment> PostComments { get; set; }
        }
        CommonWealEntities context = new CommonWealEntities();
        //public class data 
        //{ 
        //   public List<Post> value=new List<Post>(); 
        //}
        [HttpGet]
        public HttpResponseMessage GetAllPost()
        {
            /*Make List of custom post model type */
            List<Post> ob = new List<Post>();
            /*Getting list from all table */
            var NGOPostlist = context.NGOPosts.OrderByDescending(x => x.CreatedOn).ToList();
            var RegUserlist = context.RegisteredUsers.ToList();
            var Commentlist = context.PostComments.ToList();
            var NGOUserlist = context.NGOUsers.ToList();
            var LoginUserlist = context.Users.ToList();
            var PostLikeListmain = context.PostLikes.ToList();
            if (NGOPostlist != null)
            {


                foreach (var item in NGOPostlist)
                {

                    // var RegUser =RegUserlist.Where(regusr=>regusr.UserEmail==item.EmailID);
                    var Comment = Commentlist.Where(comment => comment.PostID == item.PostID);


                    Post pm = new Post();
                    pm.userId = item.LoginID.Value;

                    int UserRole = LoginUserlist.Where(user => user.LoginID == item.LoginID).Select(x => x.LoginUserType).FirstOrDefault();

                    switch (UserRole)
                    {
                        case 1: string NGOUser = NGOUserlist.Where(ngusr => ngusr.LoginID == item.LoginID).FirstOrDefault().NGOName.ToString();
                            pm.userName = NGOUser;
                            break;
                        case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.LoginID == item.LoginID).FirstOrDefault();
                            pm.userName = RegUser.FirstName + " " + RegUser.LastName;
                            break;

                    }
                    pm.postcontent = item.PostContent;
                    pm.postImageUrl = item.PostUrl;
                    pm.postCreateTime = item.CreatedOn.Value;
                    pm.likeCount = item.PostLikeCount.Value;
                    pm.commentCount = item.PostCommentCount.Value;
                    pm.postId = item.PostID;
                    /*getting list of like user of curent post*/
                    var postcomment = Commentlist.Where(Cmntlst => Cmntlst.PostID == item.PostID).ToList();

                    //start like list
                    List<PostLikeModel> imageLikeList = new List<PostLikeModel>();
                    var PostLikeList = PostLikeListmain.Where(pstlike => pstlike.PostID == item.PostID).ToList();

                    int likecount = 0;
                    foreach (var like in PostLikeList)
                    {

                        PostLikeModel pl = new PostLikeModel();
                        int userType = LoginUserlist.Where(user => user.LoginID == like.LoginID).FirstOrDefault().LoginUserType;

                        switch (userType)
                        {
                            case 1: string NGOUser = NGOUserlist.Where(ngusr => ngusr.LoginID == like.LoginID).FirstOrDefault().NGOName.ToString();
                                pl.userName = NGOUser;
                                break;
                            case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.LoginID == like.LoginID).FirstOrDefault();
                                pl.userName = RegUser.FirstName + " " + RegUser.LastName;
                                break;

                        }
                        pl.userImageUrl = "";
                        pl.UserID = like.LoginID;

                        imageLikeList.Add(pl);
                        likecount++;

                    }
                    pm.likeCount = item.PostLikeCount.Value;
                    pm.postlike = imageLikeList;
                    //end like list

                    // start all comment of  particular post
                    List<Comment> imagecommentlist = new List<Comment>();
                    pm.commentCount = postcomment.Count();
                    int commentcount = 0;
                    foreach (var a in postcomment)
                    {
                        Comment cmnt = new Comment();
                        cmnt.commentId = a.CommentID;
                        cmnt.commentContent = a.CommentText;
                        cmnt.commentLike = 0;
                        cmnt.commentUserImage = "";
                        cmnt.CreatedDateTime = a.CreatedOn.Value;
                        int userType = LoginUserlist.Where(user => user.LoginID == a.LoginID).FirstOrDefault().LoginUserType;

                        switch (userType)
                        {
                            case 1: string NGOUser = NGOUserlist.Where(ngusr => ngusr.LoginID == a.LoginID).FirstOrDefault().NGOName.ToString();
                                cmnt.Username = NGOUser;
                                break;
                            case 3: var RegUser = RegUserlist.Where(lgnuser => lgnuser.LoginID == a.LoginID).FirstOrDefault();
                                cmnt.Username = RegUser.FirstName + " " + RegUser.LastName;
                                break;

                        }

                        imagecommentlist.Add(cmnt);
                        commentcount++;
                    }
                    pm.commentCount = item.PostCommentCount.Value;
                    //end all comment of  particular post
                    pm.PostComments = imagecommentlist;
                    ob.Add(pm);

                }
            }
            //List<data> imageList = new List<data>();
            
            //imageList.Add(new data { value = ob });
            var response = Request.CreateResponse(HttpStatusCode.OK, ob);
            return response;
            //return (ob);
        }


       
        //public class UserNamesResponse
        //{
        //     "postID": 2,
        //"loginID": 7175,
        //"postDateTime": "2016-11-23T15:09:32.373",
        //"postType": "Image",
        //"postContent": "test2",
        //"postUrl": "/Images/Post/Chrysanthemum.jpg",
        //"postLikeCount": 0,
        //"postCommentCount": 0,
        //"createdOn": "2016-11-23T15:09:32.373",
        //"modifiedOn": "2016-11-23T15:09:32.373",
        //"createdBy": null
        //    public int postID  { get; set; }
        //    public int loginID { get; set; }
        //    public DateTime postDateTime { get; set; }
        //    public string postType { get; set; }
        //    public string postContent { get; set; }
        //    public int MyProperty { get; set; }
        //    public List<string> Names { get; set; }
        //    public IQueryable<NGOPost> Profiles { get; set; }
        //}
        //// GET api/<controller>
        //[HttpGet]
        //public HttpResponseMessage PostAll()
        //{

        //    CommonWealEntities context = new CommonWealEntities();
        //    List<UserNamesResponse> imageList = new List<UserNamesResponse>();
        //    NGOPost objngouser = new NGOPost();
        //    var balance = (from a in context.NGOUsers
        //                   join c in context.NGOPosts on a.LoginID equals c.LoginID
        //                   where c.LoginID == a.LoginID
        //                   select a.NGOName)
        //       .ToList();
        //    imageList.Add(new UserNamesResponse { Names = balance });
        //    imageList.Add(new UserNamesResponse { Profiles=context.NGOPosts });
        //    var response = Request.CreateResponse(HttpStatusCode.OK, imageList);
        //    return response;

        //}
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
        public HttpResponseMessage SubmitLike(PostLikeModel objpostlike)
        {
           // bool like;
           int PostID = -1;
            PostID = objpostlike.PostID;
           
            if (PostID != -1)
            {
                CommonWealEntities db = new CommonWealEntities();

                /*login user property defined in base controller*/
                /*checking is current login user already liked the image or not */
                var currentLikeUser = db.PostLikes.Where(pstlike => pstlike.PostID == PostID & pstlike.LoginID == objpostlike.LoginID).FirstOrDefault();

                if (currentLikeUser == null)
                {
                    /*if not like than add row in post  */
                    PostLike pl = new PostLike();
                    pl.CreatedOn = DateTime.Now;
                    pl.ModifiedOn = DateTime.Now;
                    pl.IsLike = true;
                    /*login user property defined in base controller*/
                    pl.LoginID = objpostlike.LoginID;
                    pl.PostID = PostID;
                    db.PostLikes.Add(pl);
                    db.SaveChanges();
                    /*update like count */
                    var post = db.NGOPosts.Where(ngpost => ngpost.PostID == PostID).FirstOrDefault();
                    post.PostLikeCount++;
                    db.SaveChanges();
                }
                else
                {/*if already liked by user than remove like row of user for unlike */
                    var removeLike = db.PostLikes.Where(pstlike => pstlike.PostID == PostID & pstlike.LoginID == objpostlike.LoginID).FirstOrDefault();
                    db.PostLikes.Remove(removeLike);                 
                    var post = db.NGOPosts.Where(ngpost => ngpost.PostID == PostID).FirstOrDefault();
                    post.PostLikeCount--;                     
                    db.SaveChanges();
                }
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, context.PostLikes);
            return response;
        }


        //[HttpPost]
        //public HttpResponseMessage LikePOST(PostLike objPostLike)
        //{
        //    CommonWealEntities context = new CommonWealEntities();
        //    var ob =context.NGOPosts.Include(x => x.PostLikes).Where(w => w.PostLikes.Where(m => m.PostID == objPostLike.PostID)).ToList();
        //    var ob = context.PostLikes.Where(x => x.PostID == objPostLike.PostID).FirstOrDefault();
        //    if (objPostLike.IsLike != null)
        //    {
        //        if (ob.IsLike == true)
        //        {
        //            ob.IsLike = false;
        //        }
        //        else
        //        {
        //            ob.IsLike = true;
        //        }
        //    }
        //    else
        //    { ob.IsLike = true; }
        //    context.Configuration.ValidateOnSaveEnabled = false;
        //    context.SaveChanges();
        //    var response = Request.CreateResponse(HttpStatusCode.OK, context.PostLikes);
        //    return response;
        //}
        //public HttpResponseMessage PostData()
        //{

        //}
        ////[HttpGet]
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
