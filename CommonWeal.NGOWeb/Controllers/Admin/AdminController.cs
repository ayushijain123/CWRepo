using CommonWeal.Data;
using CommonWeal.NGOWeb.Models;
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
            var users = obj.NGOUsers;
            var CountOfRequests = users.Where(w => w.IsActive == false && w.IsBlock == false).Count();
            ViewBag.COR = CountOfRequests;

            var CountOfActiveNGO = users.Where(w => w.IsActive == true && w.IsBlock == false).Count();
            ViewBag.COAN = CountOfActiveNGO;

            var CountOfBlockedNGO = users.Where(w => w.IsBlock == true).Count();
            ViewBag.COBN = CountOfBlockedNGO;

            var CountOfAllUsers = users.Where(w => w.IsActive == false && w.IsBlock == false).Count();
            ViewBag.COAL = CountOfRequests;

            var CountOfBlockedUsers = users.Where(w => w.IsBlock == true).Count();
            ViewBag.COBU = CountOfBlockedUsers;

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
            var request = obj.All_Users();
            return View(request);
        }

        public ActionResult Blocked_NormalUsers()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations obj = new dbOperations();
            var request = obj.Blocked_NormalUsers();
            return View(request);
        }

        public ActionResult Settings()
        {
            return View();
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

        //CONTROLLER METHODS   
        public ActionResult DisplayGraph()
        {
            AdminChart objChart = new AdminChart();
            objChart.DataValue = new AdminChartData();
            objChart.DataValue = GetChartData();
            objChart.MonthsTitle = "Months";
            objChart.NGOTitle = "NGO";
            objChart.UserTitle = "User";
            return View(objChart);

        }

        /// <summary>
        /// Code to get the data which we will pass to chart
        /// </summary>
        /// <returns></returns>
        public AdminChartData GetChartData()
        {
            AdminChartData objChartData = new AdminChartData();
            /*Get the data from databse and prepare the chart record data in string form.*/
            objChartData.Months = "1,2,3,4,5,6,7,8,9,10,11,12";
            CommonWealEntities obj = new CommonWealEntities();
            var users = obj.NGOUsers.Where(w => w.IsActive == true && w.IsBlock == false).Count();
            var user1 = obj.Users.Where(w => w.IsActive == true && w.IsBlock == false && w.LoginUserType == 3).Count();
            objChartData.NGO = users.ToString();
            objChartData.User = user1.ToString();
            return objChartData;
        }
    }
}
