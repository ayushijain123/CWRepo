using CommonWeal.Data;
using System.ComponentModel.DataAnnotations;

namespace CommonWeal.Data
{
    [MetadataType(typeof(RegisteredUserMeta))]
    public partial class RegisteredUser
    {
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_ConfirmPassword")]
        public string ConfirmPassword { get; set; }

    }

    public partial class RegisteredUserMeta
    {
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Email")]
        [System.Web.Mvc.Remote("checkEmail", "UserRegistration", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_UniqueEmail")]
        public string UserEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_passwordStrength")]
        [DataType(DataType.Password)]
        [Display(Name = "UserPassword")]
        public string UserPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "CnfrmPassword")]
        [Compare("UserPassword", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string LastName { get; set; }


        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_MobileNumberLength")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyNumbers")]
        public string Mobile { get; set; }


    }
}
