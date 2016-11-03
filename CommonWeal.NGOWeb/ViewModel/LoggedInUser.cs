using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.ViewModel
{
    public class LoggedInUser : CommonWeal.Data.User
    {
        public string UserName { get; set; }
    }
}