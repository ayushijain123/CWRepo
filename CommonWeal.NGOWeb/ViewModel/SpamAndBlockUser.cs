using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{
    public class SpamAndBlockUser
    {
        public string UserName { get; set; }
        public string ReportMessage { get; set; }
        public string Status { get; set; }
        public bool IsBlock { get; set; }
        public bool IsSpam { get; set; }
        public bool IsDecline { get; set; }
        public int UserID { get; set; }
    }
}