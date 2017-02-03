using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeal.Data.ModelExtension
{
    

        public class NGOProfileCustom
        {
            public int WatchNgoID { get; set; }
            public int UserID { get; set; }
            public string imageurl { get; set; }
            public NGOUser NgoUser { get; set; }
        }
    
}
