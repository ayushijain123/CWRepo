using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    [AllowAnonymous]
    public class ngoRegistrationController : BaseController
    {
        //
        // GET: /ngoRegistration/
        public ActionResult CreateNGO()
        {
            CommonWealEntities context = new CommonWealEntities();
            ViewBag.country = new SelectList(context.CountryMasters, "ID", "Name");
            ViewBag.state = new SelectList(new List<StateMaster>(), "ID", "Name");
            ViewBag.city = new SelectList(new List<CityMaster>(), "ID", "Name");
            return View();
        }

        public IList<StateMaster> GetState(int countryid)
        {
            CommonWealEntities context = new CommonWealEntities();
            return context.StateMasters.Where(m => m.CountryID == countryid).ToList();
        }

        public JsonResult GetJsonState(int id)
        {
            CommonWealEntities context = new CommonWealEntities();

            var stateListt = this.GetState(id);
            List<StateMaster> list = new List<StateMaster>();
            list.Add(new StateMaster { Name = "--Select Country--", ID = 0 });
            foreach (var item in stateListt)
            {

                list.Add(new StateMaster { Name = item.Name, ID = item.ID });

            }
            //var statesList = stateListt.Select(m => new SelectListItem()
            //{
            //    Text = m.Name,
            //    Value = m.ID.ToString()
            //});

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public IList<CityMaster> GetCity(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            return context.CityMasters.Where(m => m.StateID == id).ToList();
        }

        public JsonResult GetJsonCity(int id)
        {
            CommonWealEntities context = new CommonWealEntities();

            var citylist = this.GetCity(id);
            List<CityMaster> list = new List<CityMaster>();
            list.Add(new CityMaster { Name = "--Select Country--", ID = 0 });
            foreach (var item in citylist)
            {

                list.Add(new CityMaster { Name = item.Name, ID = item.ID });

            }
            //var cityList = citylist.Select(m => new SelectListItem()
            //{
            //    Text = m.Name,
            //    Value = m.ID.ToString()
            //});

            return Json(list, JsonRequestBehavior.AllowGet);
        }







        [HttpPost]
        public JsonResult doesEmailExist(string UserName)
        {

            var user = Membership.GetUser(UserName);

            return Json(user == null);
        }

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


        [HttpGet]
        public async Task<ActionResult> getImage()
        {
            List<string> imageValues = new List<string>();
            var result = await APIHelper<List<ImageListResponse>>.GetJsonAsync1("NGORegistration/Get");
            foreach (var item in result)
            {
                imageValues.Add("data:image/png;base64," + item.strImage);
            }
            ViewBag.ImageList = imageValues;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateNGO(NGOUser objngo, HttpPostedFileBase chairmanID, HttpPostedFileBase RegistrationProof)
        {           
            try
            {
                if (RegistrationProof != null)
                {
                    objngo.RegistrationProof = "default";
                }   
                    if (chairmanID != null)
                    {
                        var ext = chairmanID.FileName.Substring(chairmanID.FileName.LastIndexOf('.'));
                        ImageHandler img = new ImageHandler();
                        var res = img.ImagePost(chairmanID);
                        // string result = System.Text.Encoding.UTF8.GetString(byteArray);                  
                        objngo.ChairmanID = Convert.ToBase64String(res);
                        objngo.OperationalArea = ext;

                    }
                    if (RegistrationProof != null)
                    {
                        var ext1 = RegistrationProof.FileName.Substring(RegistrationProof.FileName.LastIndexOf('.'));
                        ImageHandler img = new ImageHandler();
                        var res1 = img.ImagePost(RegistrationProof);
                        objngo.RegistrationProof = Convert.ToBase64String(res1);
                        objngo.AreaOfIntrest = ext1;
                    }
                   
                    var result = await Task.Run(() => APIHelper<string>.PostJson("NGORegistration/CreateNGO", objngo));

                    return RedirectToAction("Index", "Welcome");
                
                return RedirectToAction("Index", "Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
                throw ex;
            }

            /*the form type is ajaxform  that's why return type should be (return JavaScript) */
            //return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");

        }
        /*to identify email address uniquely through ajax on registration*/
        public JsonResult checkEmail(NGOUser email)
        {
            CommonWealEntities context = new CommonWealEntities();
            return Json(!context.Users.Any(x => x.LoginEmailID == email.NGOEmailID), JsonRequestBehavior.AllowGet);
        }


    }
    public class ImageListResponse
    {
        //public Bitmap MyImage { get; set; }
        public string strImage { get; set; }
        public string ImageId { get; set; }
    }
}
