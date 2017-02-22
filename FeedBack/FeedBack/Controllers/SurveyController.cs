using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBack.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            int i = 0;
            FeedBack_180Entities objfeedback = new FeedBack_180Entities();
            List<QuestionAnswer> QueAnsList = new List<QuestionAnswer>();
            foreach (var item in frm.AllKeys)
            {
                
                QuestionAnswer objQA = new QuestionAnswer();
                objQA.EmpID =Convert.ToInt32( frm["EmpID"]);
                objQA.SurveyID = Convert.ToInt32(frm["MrgID"]);
                if(item.ToString()!="EmpID" && item.ToString()!="MrgId")
                {
                objQA.QuestionID = i++;
                objQA.AnswerContent = frm[item];
                QueAnsList.Add(objQA);
                }                
            }
            objfeedback.QuestionAnswers.AddRange(QueAnsList);
            objfeedback.SaveChanges();
            return View();
        }
    }
}