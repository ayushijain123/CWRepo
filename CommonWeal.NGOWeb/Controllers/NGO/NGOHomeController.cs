using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using CommonWeal.NGOWeb.ViewModel;

namespace CommonWeal.NGOWeb.Controllers.NGO
{   /*entry for authorized user and whose role is NGOAdmin*/
    [Authorize(Roles = "NGOAdmin")]
    public class NGOHomeController : BaseController
    {
        //
        // GET: /NGOHome/
        public ActionResult Index()
        {
            PostWithTopNgo pwtn = new PostWithTopNgo();
            CommonWealEntities context = new CommonWealEntities();
            ViewBag.country = new SelectList(context.CountryMasters, "ID", "Name");
            ViewBag.state = new SelectList(new List<StateMaster>(), "ID", "Name");
            ViewBag.city = new SelectList(new List<CityMaster>(), "ID", "Name");

            
            dbOperations ob = new dbOperations();
            /*get postList from GetAllPost method*/
            var postlist = ob.GetPostOnLoad();
            postlist = postlist.OrderByDescending(x => x.postCreateTime).ToList();
            var list = ob.GetAllCategory();
           // ViewBag.categoryList = new SelectList(list,"categoryId","categoryName");
            /*return To view with postList */
            pwtn.post = postlist;
            pwtn.ngouser = GetTopNgo;
            return View(pwtn);
            
        }
        //class to convert image from file to binary
        public class ImageHandler
        {
            public byte[] ImagePost(HttpPostedFileBase file)
            {
                byte[] fileData = null;

                if (file != null)
                {
                    // var docfiles = new List<string>();
                    //byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(file.ContentLength);
                    }
                }
                return fileData;
            }
        }
        //Modal for post
        public class postData
        {
            public string content { get; set; }
            public string file { get; set; }
            public int loginid { get; set; }
            public int[] cat { get; set; }
        }
        /*method for upload image*/
        [HttpPost]
        public async Task<ActionResult> PostImage(HttpPostedFileBase file, NGOPost obpost, int[] category)
        {   
            postData postValue=new postData();
            dbOperations ob = new dbOperations();
            CommonWealEntities db = new CommonWealEntities();
            /*if file is uploaded with or without content message*/
            if (file != null)
            {
                ImageHandler img = new ImageHandler();
                var res = img.ImagePost(file);
                // string result = System.Text.Encoding.UTF8.GetString(byteArray);                  
                postValue.file = Convert.ToBase64String(res);
                postValue.content = obpost.PostContent;
                postValue.loginid = LoginUser.LoginID; 
                postValue.cat = category;
                var result = await Task.Run(() => APIHelper<string>.PostJson("Post/NGOPost", postValue));
                //string ImageName = System.IO.Path.GetFileName(file.FileName);
                //string physicalPath = Server.MapPath("/Images/Post/" + ImageName);
                //// save image in folder
                //file.SaveAs(physicalPath);
                ////save new record in database
                //obpost.PostUrl = "/Images/Post/" + ImageName;
                //obpost.PostType = "Image";
                //obpost.PostDateTime = System.DateTime.Now;
                //obpost.ModifiedOn = System.DateTime.Now;
                //obpost.CreatedOn = System.DateTime.Now;
                //obpost.PostCommentCount = 0;
                //obpost.PostLikeCount = 0;
                /*login user login id is a property in base controller
                 which contain current user information from user table*/
                //obpost.LoginID = LoginUser.LoginID;
                //    //this.User.Identity.Name;
                //db.NGOPosts.Add(obpost);
                //db.SaveChanges();
                //ob.SubmitPostCategory(obpost.PostID, category);
                
            }
                /*if only content is posted without any image*/
            else if (obpost.PostContent != null)
            {
                postValue.content = obpost.PostContent;
                postValue.loginid = LoginUser.LoginID;
                postValue.cat = category;
                /*login user login id is a property in base controller
                which contain current user information from user table*/
                //obpost.LoginID=LoginUser.LoginID; 
                //    //this.User.Identity.Name;
                //obpost.PostType = "Text";
                //obpost.PostDateTime = DateTime.Now;
                //obpost.ModifiedOn = DateTime.Now;
                //obpost.CreatedOn = DateTime.Now;
                //obpost.PostCommentCount = 0;
                //obpost.PostLikeCount = 0;
                //db.NGOPosts.Add(obpost);
                //db.SaveChanges();
                //ob.SubmitPostCategory(obpost.PostID, category);
            }
           Console.Write("<script>alert('"+obpost.PostID+"')</script>");
            /*return to current view*/
            return RedirectToAction("Index", "NGOHome");
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
