using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{
    public class NGOProfilePostCustom
    {

        public int CurrentUserID { get; set; }
        public string imageurl { get; set; }
        public PostWithTopNgo PostWithtopNgoModel { get; set; }
        public string searchNgoProfileName { get; set; }
    }
}