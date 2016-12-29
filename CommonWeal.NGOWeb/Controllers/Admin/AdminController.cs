using CommonWeal.Data;
using System;
using System.Linq;
using System.Web.Mvc;
using CommonWeal.NGOWeb.Utility;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using CommonWeal.Data.ModelExtension;
using System.Net;
using System.Collections;
using CommonWeal.NGOWeb.ViewModel;


namespace CommonWeal.NGOWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminController : BaseController
    {
        dbOperations obj = new dbOperations();
        public ActionResult Index()
        {

            var result = Task.Run(() => APIHelper<Count>.GetJsonAsync1("Donut/GetCount"));

            return View(result.Result);

        }

        public ActionResult Active_Users()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserAccepted();
            return View(request);
        }

        public ActionResult Blocked_Users()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserBlocked();
            return View(request);
        }

        public ActionResult Requests()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.GetAllUserNotAccepted();
            return View(request);
        }
        //public ActionResult Index()
        //{

        //    var result = Task.Run(() => APIHelper<Count>.GetJsonAsync1("Admin/GetRequest"));

        //    return View(result.Result);

        //}




        public ActionResult All_Users()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.AllUsers();
            return View(request);
        }

        public ActionResult Blocked_NormalUsers()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.BlockedNormalUsers();
            return View(request);
        }
        public ActionResult Warned_NGOs()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var User = context.Users.Where(w => w.IsWarn == true).ToList();

            var request = obj.WarnedNGOs();

            return View(request);
        }

        public ActionResult Settings()
        {
            return View();
        }


        public ActionResult WarnNGO(int id)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();

                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsWarn = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsWarn = true;

                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();
                var ob2 = context.Users.Where(w => w.LoginID == id).Select(w => w.LoginEmailID).FirstOrDefault();
                var Randomcode = "Warning for suspicious activity";
                obj.SendActivationEmail(ob2, Randomcode);
                TempData["WS"] = "<script>alert('Warning sent!');</script>";
                return RedirectToAction("Active_Users", "Admin");

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
        public ActionResult Warn(int id)
        {
            CommonWealEntities context = new CommonWealEntities();

            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsWarn = true;
            context.SaveChanges();
            var ob2 = context.Users.Where(w => w.LoginID == id).Select(w => w.LoginEmailID).FirstOrDefault();
            var Randomcode = "Warning for suspicious activity";
            obj.SendActivationEmail(ob2, Randomcode);
            if (ob.LoginUserType == 1)
            {
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsWarn = true;

                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                TempData["WS"] = "<script>alert('Warning sent!');</script>";
                return RedirectToAction("Active_Users", "Admin");
            }
            else if (ob.LoginUserType == 3)
            {
                TempData["WS"] = "<script>alert('Warning sent!');</script>";
                return RedirectToAction("All_Users", "Admin");
            }
            context.Configuration.ValidateOnSaveEnabled = false;
            TempData["WS"] = "<script>alert('Warning sent!');</script>";
            return View();


        }
        public ActionResult UnWarn(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsWarn = false;
            if (ob.LoginUserType == 1)
            {
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsWarn = false;
            }
            context.Configuration.ValidateOnSaveEnabled = false;

            context.SaveChanges();
            return RedirectToAction("Warned_NGOs", "Admin");
        }
        public ActionResult UnWarnNGO(int id)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();

                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsWarn = false;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsWarn = false;

                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();

                return RedirectToAction("Warned_NGOs", "Admin");

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
        public ActionResult Accept(int id)
            {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                //User UL = new User ();
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsActive = true;

                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();
                return RedirectToAction("Requests", "Admin");

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

       /*method for reject request from view details page*/
        public ActionResult Decline(int id)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                context.Configuration.ValidateOnSaveEnabled = false;
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = false;
                ob.IsDecline = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsActive = false;
                ob1.IsDecline = true;                
                context.SaveChanges();
                return RedirectToAction("Requests", "Admin");
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

        public ActionResult Block(int id)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                User UL = new User();
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = false;
                ob.IsBlock = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsActive = false;
                ob1.IsBlock = true;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                return RedirectToAction("Active_Users", "Admin");
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
        public ActionResult BlockWarned(int id)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                User UL = new User();
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = false;
                ob.IsBlock = true;
                context.SaveChanges();
                var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
                ob1.IsActive = false;
                ob1.IsBlock = true;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                return RedirectToAction("Warned_NGOs", "Admin");
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
        /*method for ngo details*/
        public ActionResult ViewDetails(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            var viewdetails = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
            TempData["ViewDetails"] = viewdetails;
            return RedirectToAction("NGODetails", "Admin");
        }
        public ActionResult NGODetails()
        {        
           return View(TempData["ViewDetails"]);
        }

        /*method for More Info*/
        public ActionResult MoreInfo(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            var viewdetails = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            return View(viewdetails);
        }
        public class MoreInformation
        {
            public string NGOEmailID { get; set; }
            public string Message { get; set; }
        }
        /*method for SendMailFromAdmin*/
        public ActionResult SendMailFromAdmin(MoreInformation objMoreInfo)
        {
            dbOperations objdb = new dbOperations();
            var res = objdb.SendActivationEmail(objMoreInfo.NGOEmailID, objMoreInfo.Message);
            ViewData["MoreInfo"] = "Mail Sent";
            return RedirectToAction("Index", "Admin");
        }

        /*method for spam users*/
        public ActionResult SpamUser()
        {
            CommonWealEntities context = new CommonWealEntities();
            //var ob = context.SpamUsers.ToList();
            //return View(ob);
            User UL = new User();
            var ob = context.Users.Where(w => (w.IsSpam == true || w.IsBlock==true)&&w.LoginUserType==3).ToList();
            List<SpamAndBlockUser> listOfSpamAndBlockUsers = new List<SpamAndBlockUser>();
            foreach (var item in ob)
            {
                SpamAndBlockUser objSpamAndBlock = new SpamAndBlockUser();
                objSpamAndBlock.UserID = item.LoginID;
                objSpamAndBlock.UserName = context.RegisteredUsers.Single(w => w.LoginID == item.LoginID).FirstName;
                if (item.IsBlock)
                {                    
                    objSpamAndBlock.Status = "Blocked";
                    objSpamAndBlock.IsSpam = false;
                    objSpamAndBlock.IsBlock = true;
                }
                else
                {
                    objSpamAndBlock.IsSpam = true;
                    objSpamAndBlock.IsBlock = false;
                    objSpamAndBlock.Status = "Report Abuse";
                }
                listOfSpamAndBlockUsers.Add(objSpamAndBlock);
            }
            return View(listOfSpamAndBlockUsers);
        }

        /*method for Unspam*/
       
        public ActionResult UnSpam(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            User UL = new User();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsSpam = false;
            var unspam = context.SpamUsers.Where(x => x.LoginId == id).FirstOrDefault();
            if (unspam!=null)
            {
                context.SpamUsers.Remove(unspam);
            }
            context.SaveChanges();
            return RedirectToAction("SpamUser", "Admin");
        }

        /*method for block user from SpamUser*/
       
        public ActionResult BlockFromSpamUser(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            User UL = new User();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsBlock = true;
            ob.IsSpam = false;
            context.SaveChanges();
            return RedirectToAction("SpamUser", "Admin");
        }

        /*method for unblock user from SpamUser*/

        public ActionResult UnBlockFromSpamUser(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            User UL = new User();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsBlock = false;
            ob.IsActive = true;
            context.SaveChanges();
            return RedirectToAction("SpamUser", "Admin");
        }

        /*get dat monthly basis for graph*/


        //public async Task<ActionResult> GetDataByMonth(int year = 0)
        //{

        //    var result = await Task.Run(() => APIHelper<Count>.PostJson("demo/Get", year));

        //    return View(result);

        //}







        public JsonResult GetDataByMonth(int year = 0)
        {
            if (year == 0)
            {
                year = DateTime.Now.Year;
            }
            CommonWealEntities context = new CommonWealEntities();
            List<int> ngo = new List<int>();
            List<int> user = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                ngo.Add(context.Users.Where(x => x.LoginUserType == 1 && x.CreatedOn.Value.Month == i && x.CreatedOn.Value.Year == year).Count());
                user.Add(context.Users.Where(x => x.LoginUserType == 3 && x.CreatedOn.Value.Month == i && x.CreatedOn.Value.Year == year).Count());

            }
            List<BarChartModel> barChartData = new List<BarChartModel>();
            barChartData.Add(new BarChartModel()
            {
                name = "NGO",
                data = ngo
            });

            barChartData.Add(new BarChartModel()
            {
                name = "User",
                data = user
            });

            return Json(barChartData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Unblock(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            User UL = new User();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsActive = true;
            ob.IsBlock = false;
            var ob1 = context.NGOUsers.Where(w => w.LoginID == id).FirstOrDefault();
            ob1.IsActive = true;
            ob1.IsBlock = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
            return RedirectToAction("Blocked_Users", "Admin");
        }

        public ActionResult BlockUsers(int id)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                User UL = new User();
                var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
                ob.IsActive = false;
                ob.IsBlock = true;
                context.SaveChanges();
                return RedirectToAction("All_Users", "Admin");
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

        public ActionResult unblockUsers(int id)
        {
            CommonWealEntities context = new CommonWealEntities();
            User UL = new User();
            var ob = context.Users.Where(w => w.LoginID == id).FirstOrDefault();
            ob.IsActive = true;
            ob.IsBlock = false;
            context.SaveChanges();
            return RedirectToAction("Blocked_NormalUsers", "Admin");
        }


        //to change password of any user
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changePassword(NGOUser obj)
        {
            using (CommonWealEntities context = new CommonWealEntities())
            {
                var objngo = context.NGOUsers.Where(x => x.NGOEmailID == obj.NGOEmailID).FirstOrDefault();
                var objuser = context.Users.Where(x => x.LoginEmailID == obj.NGOEmailID).FirstOrDefault();

                context.Configuration.ValidateOnSaveEnabled = false;

                objngo.NGOPassword = obj.NGOPassword;
                objuser.LoginPassword = obj.NGOPassword;
                context.SaveChanges();
                TempData["msg"] = "<script>alert('Password changed succesfully');</script>";

                return RedirectToAction("index", "Admin");
            }
        }
       

        //public ActionResult DisplayGraph()
        //{
        //    AdminChart objChart = new AdminChart();
        //    objChart.DataValue = new AdminChartData();
        //    objChart.DataValue = GetChartData();
        //    objChart.MonthsTitle = "Months";
        //    objChart.NGOTitle = "NGO";
        //    objChart.UserTitle = "User";
        //    return View(objChart);

        //}

        /// <summary>
        /// Code to get the data which we will pass to chart
        /// </summary>
        /// <returns></returns>
        //public AdminChartData GetChartData()
        //{
        //    AdminChartData objChartData = new AdminChartData();
        //    /*Get the data from databse and prepare the chart record data in string form.*/
        //    objChartData.Months = "1,2,3,4,5,6,7,8,9,10,11,12";
        //    CommonWealEntities obj = new CommonWealEntities();
        //    var users = obj.NGOUsers.Where(w => w.IsActive == true && w.IsBlock == false).Count();
        //    var user1 = obj.Users.Where(w => w.IsActive == true && w.IsBlock == false && w.LoginUserType == 3).Count();
        //    objChartData.NGO = users.ToString();
        //    objChartData.User = user1.ToString();
        //    return objChartData;
        //}



        //[HttpGet]
        //public async Task<ActionResult> getdata(DoNut obj)
        //{
        //    //var result = await APIHelper<string>.GetJsonAsync1("Donut/listdata");

        //    var result = await APIHelper<DoNut>.GetJsonAsync1("Donut/listdata");


        //    ViewBag.data = result;
        //    return RedirectToAction("Index", "Admin");
        //}

     
        //public JsonResult GetData()
        //{
        //    CommonWealEntities context = new CommonWealEntities();
        //    var list = context.usp_GetAllData().AsEnumerable();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


    }

    public class BarChartModel
    {
        public string name { get; set; }
        public List<int> data { get; set; }
    }

}
