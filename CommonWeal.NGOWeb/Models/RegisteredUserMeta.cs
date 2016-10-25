using CommonWeal.NGOWeb;
using System;
using System.ComponentModel.DataAnnotations;

namespace CommonWeal.NGOWeb.Models
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

        [Required(ErrorMessage = "This field is required")]
        //[UniqueEmail]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets allowed")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets allowed")]
        [StringLength(50)]
        public string LastName { get; set; }
        public string UserKey { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^\(?([7-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string Mobile { get; set; }
        //public int LoginID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual UserLogin UserLogin { get; set; }


    }
}