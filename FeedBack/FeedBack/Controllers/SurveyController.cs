using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using FeedBack.Models;

namespace FeedBack.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        [HttpGet]
        public ActionResult Index(string link)
        {
            if(link!=null)
            { 
            FeedBack_180Entities objfeedback = new FeedBack_180Entities();
                
            Employee_Details objempdetails = new Employee_Details();
            SurveyInfo objsurveyinfo = new SurveyInfo();
            var linkdetails = objfeedback.SurveyLinks.Where(x=>x.Link==link).FirstOrDefault();
                if (linkdetails.Status == true)
                {
                    TempData["feedback"] = "You have already submitted feedback";
                    return RedirectToAction("SurveyClosed", "Survey");
                } 
            if(linkdetails!=null)
            {        
            var empdetails = objfeedback.Employee_Details.Where(emp => emp.EmpID == linkdetails.EmpID).FirstOrDefault();
            objsurveyinfo.Nameoftheemployee = empdetails.Name;               
            var relation=objfeedback.EmployeeRelationships.Where(x => x.SuperiorID == linkdetails.SurveyFor_ID && x.EmpID == linkdetails.EmpID).FirstOrDefault().Relation;
            objsurveyinfo.Designation = empdetails.Designation;
            objsurveyinfo.SurveyID = linkdetails.ID;
            objsurveyinfo.Location = empdetails.Location;
                if (relation==1)
                {
                    objsurveyinfo.NameoftheReportingMgr = empdetails.L1;
                    objsurveyinfo.PositionoftheReportingMgr = "L1";
                }
               else if (relation == 2)
                {
                    objsurveyinfo.NameoftheReportingMgr = empdetails.L2;
                    objsurveyinfo.PositionoftheReportingMgr = "L2";
                }
              else  if (relation == 3)
                {
                    objsurveyinfo.NameoftheReportingMgr = empdetails.L3;
                    objsurveyinfo.PositionoftheReportingMgr = "L3";
                }
                
            }
                return View(objsurveyinfo);
            }
            
            return RedirectToAction("Error", "Survey");
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            try
            {     
                if(frm!=null)
                {         
                FeedBack_180Entities objfeedback = new FeedBack_180Entities();
                List<QuestionAnswer> QueAnsList = new List<QuestionAnswer>();               
                var SurveyId = Convert.ToInt32(frm[ConfigurationManager.AppSettings["SurveyId"]]);
                foreach (var item in frm.AllKeys)
                {
                    QuestionAnswer objQA = new QuestionAnswer();                    
                    objQA.SurveyID = SurveyId;
                    if (!item.ToString().Equals(ConfigurationManager.AppSettings["SurveyId"]))
                    {
                        objQA.QuestionID = Convert.ToInt32(item.Split('-')[1]);
                        objQA.AnswerContent = frm[item];
                        QueAnsList.Add(objQA);
                    }
                }
                objfeedback.QuestionAnswers.AddRange(QueAnsList);
               var slob= objfeedback.SurveyLinks.Where(x => x.ID == SurveyId).FirstOrDefault();
                slob.Status = true;
                objfeedback.SaveChanges();
                TempData["feedback"] = "Feedback submitted successfully";
                return RedirectToAction("SurveyClosed", "Survey");
                }
                TempData["feedback"] = "Null Value Exception";
                return RedirectToAction("SurveyClosed", "Survey");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Survey",ex);
                throw;
            }
        }
        public ActionResult SurveyClosed()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult DynamicSurveyForm() {
            FeedBack_180Entities obj = new FeedBack_180Entities();
            List<questioncontent> questioncontent = new List<questioncontent>();
            var res=obj.Questions.ToList();
            foreach (var item in res)
            {
                questioncontent objcontent = new questioncontent();
                objcontent.QuestionContent = item.Question_Content;
                objcontent.QuestionId = item.ID;
                objcontent.AnswerType = "1";
                objcontent.ColumnCount = 4;
                questioncontent.Add(objcontent);

            }
            return View(questioncontent);

        }
    }
}