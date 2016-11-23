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
    public class NGORegistrationController : BaseController
    {
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "ASJ", "AJ" };
        }
        [HttpPost]
        public bool CreateNGO(NGOUser objngo)
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
                    obj.ModifiedOn = DateTime.Now;
                    obj.CreatedOn = DateTime.Now;
                    context.Users.Add(obj);
                    context.SaveChanges();
                    /*to append data in ngoUser object objngo which are not submitted through form */
                    ImageHandler imgobj = new ImageHandler();
                    byte[] data = Convert.FromBase64String(objngo.ChairmanID);
                    //byte[] data = Encoding.ASCII.GetBytes(objngo.ChairmanID);
                    imgobj.Image = data;
                    context.ImageHandlers.Add(imgobj);
                    context.SaveChanges();
                    objngo.LoginID = obj.LoginID;/*reference key from user table */
                    objngo.IsActive = false;
                    objngo.NGOEmailID = objngo.NGOEmailID.ToLower();
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
