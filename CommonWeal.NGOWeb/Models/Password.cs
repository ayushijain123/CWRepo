using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.Models
{
    public class Password
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail")]
        [Required(ErrorMessage = "This field is required")]
        public string UserEmail { get; set; }

        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Password Length should be minimum 8 characters with uppercase lowercase and special character")]
        [Required(ErrorMessage = "This field is required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        [Required(ErrorMessage = "This field is required")]
        public string ConfirmPassword { get; set; }
    }
}