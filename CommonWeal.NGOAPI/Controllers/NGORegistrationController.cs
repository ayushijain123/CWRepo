using CommonWeal.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class NGORegistrationController : BaseController
    {

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "ASJ", "AJ" };
        //}
        public class checkEmail
        { 
          public string  EmailId {get;set;}
        }
        [HttpPost]
        public bool CheckEmail(checkEmail email)
        {
            var EmailId = email.EmailId;
            CommonWealEntities context = new CommonWealEntities();

            var user = context.Users.Where(w => w.LoginEmailID == EmailId).FirstOrDefault();
            if(user == null)
            {return true;}
            else
                return false;
          
          }
       
        [HttpPost]
        public async Task<HttpResponseMessage> PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                           // var filePath = HttpContext.Current.Server.MapPath("~/UploadFiles/" + postedFile.FileName); //+ extension

                            //postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
        public class ImageListResponse
        {
            //public Bitmap MyImage { get; set; }
            public string strImage { get; set; }
            public string ImageId { get; set; }
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var content = new MultipartContent();
            CommonWealEntities context = new CommonWealEntities();
            var data = context.ImageHandlers.ToList();
            List<ImageListResponse> imageList = new List<ImageListResponse>();
            foreach (var item in data)
            {
                string imageBase64Data = Convert.ToBase64String(item.Image);

                imageList.Add(new ImageListResponse() { strImage = imageBase64Data });
            }
            result = Request.CreateResponse(HttpStatusCode.OK, imageList);
            return result;
        }


        [HttpPost]
        public bool CreateNGO(NGOUser objngo)
        {
            var request = HttpContext.Current.Request;
            try
            {
                using (CommonWealEntities context = new CommonWealEntities())
                {
                    if (objngo.DateOfRegistration == null)
                    {
                        objngo.DateOfRegistration = DateTime.Now;
                    }
                    /*first we update user table than NGOUser table to maintain referential integrity */
                    User obj = new User();
                    obj.LoginPassword = objngo.NGOPassword;
                    obj.LoginEmailID = objngo.NGOEmailID.ToLower();
                    var roleobj = context.RoleTypes.Where(w => w.RoleName == "NGO").FirstOrDefault();
                    obj.LoginUserType = roleobj.RoleID;
                    obj.IsActive = false;/*NGO is not active bydefault it will become active after admin verification  */
                    obj.IsBlock = false;/*default ngo user is not blocked */
                    obj.ModifiedOn = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    context.Users.Add(obj);                    
                    /*to append data in ngoUser object objngo which are not submitted through form */
                    ImageHandler imgobj = new ImageHandler();
                    if (objngo.ChairmanID != null && objngo.RegistrationProof != null)
                    {
                        byte[] data = Convert.FromBase64String(objngo.ChairmanID);
                        //byte[] data = Encoding.ASCII.GetBytes(objngo.ChairmanID);
                        imgobj.Image = data;
                        context.ImageHandlers.Add(imgobj);
                    }
                    objngo.LoginID = obj.LoginID;/*reference key from user table */
                    objngo.NGOEmailID = objngo.NGOEmailID.ToLower();
                    objngo.IsActive = false;                   
                    objngo.IsBlock = false;
                    objngo.ChairmanID = null;
                    objngo.RegistrationProof = null;
                    context.NGOUsers.Add(objngo);
                    context.SaveChanges();
                    /*it will redirect ngo user to welcome page after registration*/
                    return true;
                }

            }

            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
    }
}
