using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonWeal.Data;
namespace CommonWeal.NGOWeb.ViewModel
{
    public class PostWithTopNgo
    {
      public  List<Post> post { get; set; }
      public List<NGOUser> ngouser { get; set; }
      

    }
}