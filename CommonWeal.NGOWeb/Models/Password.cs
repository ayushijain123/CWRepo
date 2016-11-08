using CommonWeal.Data;
using System.ComponentModel.DataAnnotations;

namespace CommonWeal.NGOWeb.Models
{
    public class Password
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail")]
       // [Required(ErrorMessage = "This field is required")]
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public string UserEmail { get; set; }

        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_passwordStrength")]
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public string FinalOTP { get; set; }
    }
}