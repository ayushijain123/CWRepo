using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.ViewModel
{
    public class NGOProfileCustom
    {
        public int UserID { get; set; }
        public string imageurl { get; set; }
        public NGOUser NgoUser { get; set; }
    }
}