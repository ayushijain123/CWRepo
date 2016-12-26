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
    public class UserRegistrationController : BaseController
    {
        ////[HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "ASJ", "AJ" };
        }
        public class checkEmail
        {
            public string EmailId { get; set; }
        }
        [HttpPost]
        public bool CheckEmail(checkEmail email)
        {
            var EmailId = email.EmailId;
            CommonWealEntities context = new CommonWealEntities();

            var user = context.Users.Where(w => w.LoginEmailID == EmailId).FirstOrDefault();
            if (user == null)
            { return true; }
            else
                return false;

        }
       
        [HttpGet]
        public HttpResponseMessage loginget()
        {
            CommonWealEntities db = new CommonWealEntities();
            var response = Request.CreateResponse(HttpStatusCode.OK, db.Users);
            return response;
        }
  
        [HttpPost]
        public bool CreateUser(RegisteredUser ru)
        {
            CommonWealEntities conn = new CommonWealEntities();
            try
            {
                //conn.Tests.Add(objTest);
                User objusr = new User();
                objusr.LoginPassword = ru.UserPassword;
                objusr.LoginEmailID = ru.UserEmail.ToLower();
                var roleobj = conn.RoleTypes.Where(w => w.RoleName == "USER").FirstOrDefault();
                objusr.LoginUserType = roleobj.RoleID;
                objusr.IsActive = true;
                objusr.IsBlock = false;
                objusr.ModifiedOn = DateTime.Now;
                objusr.CreatedOn = DateTime.Now;
                conn.Users.Add(objusr);
                //conn.SaveChanges();
                ru.LoginID = objusr.LoginID;
                ru.UserEmail = objusr.LoginEmailID;
                ru.LoginUserType = 3;
                //conn.RegisteredUsers.Add(ru);
                //conn.Configuration.ValidateOnSaveEnabled = false;
                conn.RegisteredUsers.Add(ru);
                var year = DateTime.Now.Year;
                var objresyear = conn.RegistrationYears.Where(x => x.year == year).FirstOrDefault();
                if (objresyear == null)
                {
                    RegistrationYear objres = new RegistrationYear();
                    objres.year = year;
                    conn.RegistrationYears.Add(objres);
                }
                conn.SaveChanges();
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ru);
                return true;

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
