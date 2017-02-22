using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBack.Models
{
    public class SurveyInfo
    {
        public string NameoftheReportingMgr { get; set; }
        public string Nameoftheemployee { get; set; }
        public string PositionoftheReportingMgr { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public int EmpID { get; set; }
        public int SurveyID { get; set; }
    }
}