using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel.Admin
{
    public class GenerateEstimation
    {

        public string Product { get; set; }
        public int TotalQuantity { get; set; }
        public int Received { get; set; }
        public int Unitprice { get; set; }
        public int linetotal { get; set; }
         public int RequestId { get; set; }
         public int Itemid { get; set; }
    }
}