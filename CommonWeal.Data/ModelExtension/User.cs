using CommonWeal.Data.Properties;
using System.ComponentModel.DataAnnotations;

namespace CommonWeal.Data
{
    [MetadataType(typeof(UserMeta))]
    public partial class User { }


    public partial class UserMeta
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Email")]
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public string LoginEmailID { get; set; }
        //[RegularExpression(@"^(?=(.*\d){1})(.*\S)(?=.*[a-zA-Z\S])[0-9a-zA-Z\S]{8,}", ErrorMessage = "Password should be minimum 8  characters")]
        [Required(ErrorMessageResourceType = typeof(ModelMessages), ErrorMessageResourceName = "Generic_Required")]
        public string LoginPassword { get; set; }
    }

}
