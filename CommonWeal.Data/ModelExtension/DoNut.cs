using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeal.Data.ModelExtension
{
    public class Count
    {

        public int CountOfRequests { get; set; }
        public int CountOfActiveNGO { get; set; }
        public int CountOfBlockedNGO { get; set; }
        public int CountOfAllUsers { get; set; }
        public int CountOfBlockedUsers { get; set; }
        public int CountOfWarnedUsers { get; set; }
        public int CountOfTotalActiveNGO { get; set; }
        public int CountOfTotalBlockNGO { get; set; }
        public int CountOfTotalNGO { get; set; }
        public int CountOfTotalActiveUsers { get; set; }
        public int CountOfTotalBlockUsers { get; set; }
        public int CountOfTotalUsers { get; set; }

    }
}
