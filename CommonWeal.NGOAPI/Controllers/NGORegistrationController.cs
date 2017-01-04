using CommonWeal.Data;
//using log4net;
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
        //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
    
       public HttpResponseMessage CreateNGO(NGOUser objngo)
        {
           
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
                    obj.IsSpam = false;/*default ngo user is spam*/
                    obj.IsDecline = false;
                    obj.ModifiedOn = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    context.Users.Add(obj);                    
                    /*to append data in ngoUser object objngo which are not submitted through form */
                    ImageHandler imgobj = new ImageHandler();
                    
                        if (objngo.ChairmanID != null)
                        {
                            byte[] imageBytes = Convert.FromBase64String(objngo.ChairmanID);
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                            var abc = Guid.NewGuid();
                            string path = "/Images/" + abc + ".jpg";
                            string filepath = HttpContext.Current.Server.MapPath(path);
                            image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            objngo.ChairmanID =path;
                        }
                        if (objngo.RegistrationProof != null) 
                        {
                            byte[] imageBytes = Convert.FromBase64String(objngo.RegistrationProof);
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                            var abc = Guid.NewGuid();
                            string path = "/Images/" + abc + ".jpg";
                            string filepath = HttpContext.Current.Server.MapPath(path);
                            image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            objngo.RegistrationProof = path;
                        }
                                                                                                   
                       
                    objngo.LoginID = obj.LoginID;/*reference key from user table */
                    objngo.NGOEmailID = obj.LoginEmailID;
                    objngo.IsActive = false;                   
                    objngo.IsBlock = false;
                    objngo.IsDecline = false;                 
                    context.NGOUsers.Add(objngo);
                    var year = DateTime.Now.Year;
                    var objresyear = context.RegistrationYears.Where(x => x.year == year).FirstOrDefault();
                    if (objresyear == null)
                    {
                        RegistrationYear objres = new RegistrationYear();
                        objres.year = year;
                        context.RegistrationYears.Add(objres);
                    }
                    context.SaveChanges();
                    /*it will redirect ngo user to welcome page after registration*/
                   
                }

            }             
 
            catch (Exception ex)
            {
                //Log.Error(ex);                
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
