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


        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Password Length should be minimum 8 characters with uppercase lowercase and special character")]
        [DataType(DataType.Password)]
        [Display(Name = "UserPassword")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Password and Confirm Password does not match")]
        [DataType(DataType.Password)]
        [Display(Name = "CnfrmPassword")]
        [Compare("UserPassword", ErrorMessage = "Password and Confirm Password does not match")]
        public string CnfrmPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string LastName { get; set; }

        public string UserKey { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression(@"^((0091)|(\+91)|0?)[789]{1}\d{9}$", ErrorMessage = "Invalid Phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit Number only")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter Numbers only")]
        public string Mobile { get; set; }

        //public int LoginID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual User UserLogin { get; set; }


    }
}