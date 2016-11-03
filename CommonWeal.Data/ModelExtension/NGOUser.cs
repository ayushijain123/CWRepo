using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Enter Number only "), MaxLength(30)]
        public string UniqueuId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string NGOName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail")]
        [Required(ErrorMessage = "This field is required")]
        //[System.Web.Mvc.Remote("doesEmailExist", "NGORegistration", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        //[System.Web.Mvc.Remote("checkEmail", "ngoRegistration", ErrorMessage = "Email already exists")]
        public string NGOEmailID { get; set; }

        [RegularExpression(@"^.*(?=.{8,12})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Password Length should be minimum 8 characters with uppercase lowercase and special character")]
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "NGOPassword")]
        public string NGOPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "CnfrmPassword")]
        [Compare("NGOPassword", ErrorMessage = "Password and Confirm Password does not match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression(@"^((0091)|(\+91)|0?)[789]{1}\d{9}$", ErrorMessage = "Not a valid Phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit Number only")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter Numbers only")]
        public string Mobile { get; set; }

        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string City { get; set; }

        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string State { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        public string NGOAddress { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string ChairmanName { get; set; }

        //[Required(ErrorMessage = "This field is required")]
        public string ChairmanID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string ParentOrganisation { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string RegisteredWith { get; set; }

        [Required(ErrorMessage = "This field is required")]
        //[RegularExpression("[0-9]*$", ErrorMessage = "Enter Numbers only")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "Enter Alphanumeric only")]
        [StringLength(15)]
        public string RegistrationNumber { get; set; }

        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "Enter Alphabets only")]
        [StringLength(50)]
        public string CityOfRegistration { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime DateOfRegistration { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [Url(ErrorMessage = "Enter Valid URL")]
        public string WebsiteUrl { get; set; }


    }
}
