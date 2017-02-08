using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeal.Data.ModelExtension
{
  public class DonateRequest
    {
        public string description { get; set; }
        public string Image { get; set; }
        public List<DonateItem1> itemlist { get; set; }
    }
}
