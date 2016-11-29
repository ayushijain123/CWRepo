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
        }
        [HttpPost]
        public bool ConfirmOTP(EnteredEmail  UserEmail)
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
    }
}
