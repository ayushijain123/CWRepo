using CommonWeal.Data.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace CommonWeal.Data
{

    [MetadataType(typeof(NGOUserMeta))]
    public partial class NGOUser
    {
        public string ConfirmPassword { get; set; }

    }

    public partial class NGOUserMeta
    {
        public int NGOUserId { get; set; }

        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "Enter Alphanumeric only")]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyNumbers"), MaxLength(30)]
        public string UniqueuId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string NGOName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Email")]
        [System.Web.Mvc.Remote("checkEmail", "ngoRegistration", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_UniqueEmail")]
        public string NGOEmailID { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_passwordStrength")]
        [DataType(DataType.Password)]
        [Display(Name = "NGOPassword")]
        public string NGOPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfrmPassword")]
        [Compare("NGOPassword", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_ConfirmPassword")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_MobileNumberLength")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyNumbers")]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string State { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string NGOAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string ChairmanName { get; set; }


        public string ChairmanID { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string ParentOrganisation { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string RegisteredWith { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "Enter Alphanumeric only")]
        [StringLength(15, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string RegistrationNumber { get; set; }

        [RegularExpression("[a-zA-Z ]*$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_OnlyAlphabets")]
        [StringLength(50, ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_StringLength")]
        public string CityOfRegistration { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public DateTime DateOfRegistration { get; set; }


        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        [Url(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_URL")]
        public string WebsiteUrl { get; set; }


    }
}
