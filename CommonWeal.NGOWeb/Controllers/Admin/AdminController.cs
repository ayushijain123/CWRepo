using CommonWeal.Data;
using System;
using System.Linq;
using System.Web.Mvc;


namespace CommonWeal.NGOWeb.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminController : BaseController
    {
        dbOperations obj = new dbOperations();
        public ActionResult Index()
        {
            CommonWealEntities obj = new CommonWealEntities();
            var ngo = obj.NGOUsers;
            var users = obj.Users;

            //NGOs Table
            var CountOfRequests = ngo.Where(w => w.IsActive == false && w.IsBlock == false).Count();
            ViewBag.COR = CountOfRequests;

            var CountOfActiveNGO = ngo.Where(w => w.IsActive == true && w.IsBlock == false).Count();
            ViewBag.COAN = CountOfActiveNGO;

            var CountOfBlockedNGO = ngo.Where(w => w.IsBlock == true).Count();
            ViewBag.COBN = CountOfBlockedNGO;

            //Users Table
            var CountOfAllUsers = users.Where(w => w.IsActive == true && w.IsBlock == false && w.LoginUserType == 3).Count();
            ViewBag.COAL = CountOfAllUsers;

            var CountOfBlockedUsers = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            ViewBag.COBU = CountOfBlockedUsers;

            var CountOfWarnedUsers = users.Where(w => w.IsWarn == true).Count();
            ViewBag.COWU = CountOfWarnedUsers;

            //Total NGOs Count
            var CountOfTotalActiveNGO = ngo.Where(w => w.IsActive == true).Count();
            ViewBag.COTAN = CountOfTotalActiveNGO;

            var CountOfTotalBlockNGO = ngo.Where(w => w.IsBlock == true).Count();
            ViewBag.COTBN = CountOfTotalBlockNGO;

            var CountOfTotalNGO = CountOfTotalActiveNGO + CountOfTotalBlockNGO;
            ViewBag.COTN = CountOfTotalNGO;

            //Total Users Count
            var CountOfTotalActiveUsers = users.Where(w => w.IsActive == true && w.LoginUserType == 3).Count();
            ViewBag.COTAU = CountOfTotalActiveUsers;

            var CountOfTotalBlockUsers = users.Where(w => w.IsBlock == true && w.LoginUserType == 3).Count();
            ViewBag.COTBU = CountOfTotalBlockUsers;

            var CountOfTotalUsers = CountOfTotalActiveUsers + CountOfTotalBlockUsers;
            ViewBag.COTU = CountOfTotalUsers;
            return View();

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
                TempData["WS"] = "<script>alert('Warning send!');</script>";
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
                TempData["msg"] = "<script>alert('Password Changed succesfully');</script>";

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
    }
}
