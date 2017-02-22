using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;

namespace FeedBack.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    //ExcelDataReader works on binary excel file
                    Stream stream = uploadfile.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (uploadfile.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadfile.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        //Shows error if uploaded file is not Excel file
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result = reader.AsDataSet();
                    reader.Close();

                    SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FeedBack_180Entities"].ConnectionString);
                    SqlBulkCopy objbulk = new SqlBulkCopy(connection);
                    objbulk.DestinationTableName = "Employee_Details";
                    //Mapping Table column    
                    objbulk.ColumnMappings.Add("EMP ID", "EmpID");
                    objbulk.ColumnMappings.Add("Name", "Name");
                    objbulk.ColumnMappings.Add("Designation", "Designation");
                    objbulk.ColumnMappings.Add("Department", "Department");
                    objbulk.ColumnMappings.Add("Vertical", "Vertical");
                    objbulk.ColumnMappings.Add("Location", "Location");
                    objbulk.ColumnMappings.Add("Gender", "Gender");
                    objbulk.ColumnMappings.Add("DOJ", "DOJ");
                    objbulk.ColumnMappings.Add("L1Email", "L1Email");
                    objbulk.ColumnMappings.Add("L2Email", "L2Email");
                    objbulk.ColumnMappings.Add("L3Email", "L3Email");
                    objbulk.ColumnMappings.Add("L1", "L1");
                    objbulk.ColumnMappings.Add("L2", "L2");
                    objbulk.ColumnMappings.Add("L3", "L3");

                    connection.Open();

                    objbulk.WriteToServer(result.Tables[0]);
                    connection.Close();

                    //Sending result data to View
                    return View(result.Tables[0]);
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }
            return View();
        }
        public ActionResult StartSurvey()
        {
            return View();
        }

        

    }
}