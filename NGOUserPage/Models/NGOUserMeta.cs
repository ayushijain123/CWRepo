using NGOUserPage;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comonweal.Models
{
    //public class UrlAttribute : ValidationAttribute
    //{
    //    public UrlAttribute() { }

    //    public override bool IsValid(object value)
    //    {
    //        //may want more here for https, etc
    //        Regex regex = new Regex(@"(http://)?(www\.)?\w+\.(com|net|edu|org)");

    //        if (value == null) return false;

    //        if (!regex.IsMatch(value.ToString())) return false;

    //        return true;
    //    }
    //}

    [MetadataType(typeof(NGOUserMeta))]
    public partial class NGOUser
    {
    }

    public class NGOUserMeta
    {
        public int NGOUserId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "only numbers allowed "), MaxLength(30)]
        public string UniqueuId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string NGOName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail")]
        [Required(ErrorMessage = "This field is required")]
        public string NGOEmailID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string NGOPassword { get; set; }
        public Nullable<int> LoginID { get; set; }
        public string NGOKey { get; set; }
        public string NGOProfilePic { get; set; }
        public string Telephone { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^\(?([7-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string City { get; set; }
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string State { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        public string NGOAddress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string ChairmanName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string ChairmanID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string ParentOrganisation { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string RegisteredWith { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[0-9]*$", ErrorMessage = "only numbers are allowed")]
        public string RegistrationNumber { get; set; }
        [RegularExpression("[a-zA-Z ]*$", ErrorMessage = "only alphabets are allowed")]
        [StringLength(50)]
        public string CityOfRegistration { get; set; }
        public Nullable<System.DateTime> DateOfRegistration { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string RegistrationProof { get; set; }
        public string FCRANumber { get; set; }
        public string AreaOfIntrest { get; set; }
        public string OperationalArea { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlock { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual UserLogin UserLogin { get; set; }

    }
}