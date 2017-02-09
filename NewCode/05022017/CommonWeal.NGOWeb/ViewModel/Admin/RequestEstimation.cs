using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel.Admin
{
    public class RequestEstimation
    {
        public string NGOName { get; set; }
        public string Description { get; set; }
        public string DonateRequestdate { get; set; }
        public bool Status { get; set; }
        public int recievedCost { get; set; }
        public int RequestId { get; set; }

    }
}