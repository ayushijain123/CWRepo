using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeal.Data
{
    [MetadataType(typeof(RegisteredUserMeta))]
    public partial class RegisteredUser
    {
        public string ConfirmPassword { get; set; }

    }

    public partial class RegisteredUserMeta
    {
        [Required(ErrorMessage = "This field is required")]
        //[UniqueEmail]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail")]
        // [System.Web.Mvc.Remote("checkEmail", "UserRegistration", ErrorMessage = "Email already exists")]
        public string UserEmail { get; set; }


        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Password Length should be minimum 8 characters with uppercase lowercase and special character")]
        [DataType(DataType.Password)]
        [Display(Name = "UserPassword")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Password and Confirm Password does not match")]
        [DataType(DataType.Password)]
        [Display(Name = "CnfrmPassword")]
        [Compare("UserPassword", ErrorMessage = "Password and Confirm Password does not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string LastName { get; set; }
         

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression(@"^((0091)|(\+91)|0?)[789]{1}\d{9}$", ErrorMessage = "Invalid Phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit Number only")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter Numbers only")]
        public string Mobile { get; set; }

       
    }
}
