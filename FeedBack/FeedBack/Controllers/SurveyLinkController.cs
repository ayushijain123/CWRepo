using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBack.Controllers
{
    public class SurveyLinkController : Controller
    {
        // GET: SurveyLink
        public ActionResult Index()
        {
            return View();
        }
        public string OTP(string EmpID)
        {
           
            char[] charArr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            // int noofcharacters = Convert.ToInt32(txtCharacters.Text);
            for (int i = 0; i <= 10; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString()))
                    strrandom += charArr.GetValue(pos);
                else
                    i--;
            }            
            return strrandom+ EmpID;
        }
        public ActionResult Surveylink()
        {
            FeedBack_180Entities objfeedback = new FeedBack_180Entities();            
            Employee_Details objemp = new Employee_Details();
            var objemp1 = objfeedback.Employee_Details.ToList();          
            SurveyLink objsurvey = new SurveyLink();          
            foreach (var item in objemp1)
            {
                objsurvey.EmpID = item.EmpID;
                if (item.L1Email != null)
                {
                  //  var link = OTP(item.L1Email);
                    var guid = Guid.NewGuid();                 
                    objfeedback.SurveyLinks.Add(new SurveyLink { Link = guid.ToString(), EmpID = item.EmpID, Status = false, SurveyFor_ID = item.L1Email });
                    objfeedback.SaveChanges();
                }
                if (item.L2Email != null)
                {
                    //var link = OTP(item.L2Email);
                    var guid = Guid.NewGuid();
                    objfeedback.SurveyLinks.Add(new SurveyLink { Link = guid.ToString(), EmpID = item.EmpID, Status = false, SurveyFor_ID = item.L2Email });
                    objfeedback.SaveChanges();
                }
                if (item.L3Email != null)
                {
                   // var link = OTP(item.L3Email);
                    var guid = Guid.NewGuid();
                    objfeedback.SurveyLinks.Add(new SurveyLink { Link = guid.ToString(), EmpID = item.EmpID, Status = false, SurveyFor_ID = item.L3Email });
                    objfeedback.SaveChanges();
                }
              //  objfeedback.SaveChanges();
                
            }
            return View();
        }
    }
}