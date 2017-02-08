using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeal.Data.ModelExtension
{
    public class DonationData
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public int RequestNGOID { get; set; }
        //public string ItemName { get; set; }
        //public int  ItemCount { get; set; }
        public List<DonationDetail> donationdetaildata { get; set; }
    }
}
