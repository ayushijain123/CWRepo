using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NGOUserPage.Models
{
    public class Reset
    {


        [Required(ErrorMessage = "Please Provide Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email_address { get; set; }
       // [Required(ErrorMessage = "Please provide password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string newpwd { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("login_password", ErrorMessage = "The password and confirmation do not match.")]
        public string cnfrmpwd { get; set; }

    }
}