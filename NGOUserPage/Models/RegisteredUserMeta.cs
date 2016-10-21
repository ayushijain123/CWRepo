using NGOUserPage;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comonweal.Models
{
    //public class UniqueEmailAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        CommonWealEntities context = new CommonWealEntities();
    //        string Email = value.ToString();

    //        var result = context.UserLogins.Where(w => w.LoginEmailID == Email).ToList().Count;
    //        if (result != 0)
    //        {
    //            return new ValidationResult("Email already exist");
    //        }
    //        return ValidationResult.Success;
    //    }

    //}


    [MetadataType(typeof(RegisteredUserMeta))]
    public partial class RegisteredUser
    {
    }

    public class RegisteredUserMeta
    {

        [Required]
        //[UniqueEmail]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string UserKey { get; set; }
        [Required]
        public string Mobile { get; set; }
        //public int LoginID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual UserLogin UserLogin { get; set; }


    }
}