using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel.Admin
{
    public class RequestItemEstimation
    {
        public string NGOName { get; set; }
        public DateTime DonateRequestDate { get; set; }
        public List<GenerateEstimation> estimate { get; set; }
    }
}