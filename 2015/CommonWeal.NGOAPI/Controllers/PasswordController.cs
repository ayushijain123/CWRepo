using CommonWeal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CommonWeal.NGOAPI.Controllers
{
    [AllowAnonymous]
    public class PasswordController : BaseController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "ASJ", "AJ" };
        }

        public class EnteredEmail
        {
            public string EmailId { get; set; }
            public string OTP { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }
        [HttpPost]
        public bool SendOTP(EnteredEmail UserEmail)
        {
            // var UserEmail = otp1.EmailId;
            var EnteredEmail = UserEmail.EmailId;
            CommonWealEntities context = new CommonWealEntities();
            Functions obj = new Functions();
            var request = obj.RegisteredUserIsAccepted();
            var request2 = obj.UserIsAccepted();
            var request1 = obj.RegisteredNGOIsAccepted();
            var ob = request.Where(w => w.UserEmail == EnteredEmail).FirstOrDefault();
            var ob1 = request1.Where(w => w.NGOEmailID == EnteredEmail).FirstOrDefault();
            bool result = false;
            if (ob != null || ob1 != null)
            {
                foreach (var EmailId in request2)
                {
                    if
                        (EnteredEmail == EmailId.LoginEmailID.ToString()) //
                    {
                        //return( RedirectToAction("ForgotPasswordConfirm",EmailId));
                        //ViewBag.Email = UserEmail;
                        var Randomcode = obj.GenerateRandomPassword(7);
                        obj.SendActivationEmail(EnteredEmail, Randomcode);
                        var code = Randomcode;
                        ForgotPassword entry = new ForgotPassword();
                        entry.EmailId = EnteredEmail;
                        entry.OTP = Randomcode;
                        entry.CreatedOn = System.DateTime.Now;
                        entry.ModifiedOn = System.DateTime.Now;
                        entry.CreatedBy = EnteredEmail;
                        context.ForgotPasswords.Add(entry);
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        [HttpPost]
        public bool ConfirmOTP(EnteredEmail UserOTP)
        {
            var FinalEmail = UserOTP.EmailId;
            var FinalOTP = UserOTP.OTP;
            CommonWealEntities context = new CommonWealEntities();
            Functions obj = new Functions();
            bool result = false;
            var checkOTP = obj.UserDetail(FinalOTP);
            if (checkOTP != null)
            {
                var matchEmail = checkOTP.EmailId;
                if (matchEmail == FinalEmail)
                {
                    result = true;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        [HttpPost]
        public bool UpdatePassword(EnteredEmail changepassword)//,string ConfirmOTP, string OTP
        {

            var EnteredEmail = changepassword.EmailId;
            var NewPassword = changepassword.NewPassword;
            var ConfirmPassword = changepassword.ConfirmPassword;
            CommonWealEntities context = new CommonWealEntities();
            Functions obj = new Functions();
            bool result = false;
            if (NewPassword == ConfirmPassword)
            {
                var request = obj.RegisteredUserIsAccepted();
                var ob = context.RegisteredUsers.Where(w => w.UserEmail == EnteredEmail).FirstOrDefault();
                if (ob != null) //.UserEmail
                {
                    try
                    {
                        ob.UserPassword = NewPassword;
                        ob.ConfirmPassword = ConfirmPassword;
                        var LoginId = ob.LoginID;
                        var NewUserPassword = context.Users.Where(w => w.LoginID == LoginId).FirstOrDefault();
                        NewUserPassword.LoginPassword = NewPassword;
                        context.SaveChanges();
                        result = true;
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

                var ob1 = context.NGOUsers.Where(w => w.NGOEmailID == EnteredEmail).FirstOrDefault();
                if (ob1 != null)
                {
                    ob1.NGOPassword = NewPassword;
                    var LoginId = ob1.LoginID;
                    ob1.ConfirmPassword = ConfirmPassword;
                    //var LoginId = ob.LoginID;
                    var ChangePassword = context.Users.Where(w => w.LoginID == LoginId).FirstOrDefault();
                    ChangePassword.LoginPassword = NewPassword;
                    context.SaveChanges();
                    result = true;
                }

            }
            //}


            else
            {
                result = false;
            }

            return result;
        }
    }
}
