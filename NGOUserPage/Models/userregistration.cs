using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NGOUserPage.Models
{
    public class userregistration
    {

        [DisplayName("First Name:")]
        [Required (ErrorMessage="Provide first name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter valid name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Provide last name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter valid name")]
        public string lastName { get; set; }
        
        [Required(ErrorMessage = "Provide Email")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string login_Email { get; set; }
        [Required(ErrorMessage = "Provide password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(.{6,12})$", ErrorMessage = "Password lenght must be greater then 6 and lower then 12,  ")] 
        public string login_password { get; set; }
        [Required(ErrorMessage = "Provide Mobile Number")]
        [RegularExpression( @"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$",ErrorMessage = "Entered phone format is not valid.")]
        public string Login_mobileNumber { get; set; }
         [Required(ErrorMessage = "Re-enetr password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("login_password", ErrorMessage = "The password and confirmation did not match.")]
        public string confirmpassword { get; set; }

    }
}