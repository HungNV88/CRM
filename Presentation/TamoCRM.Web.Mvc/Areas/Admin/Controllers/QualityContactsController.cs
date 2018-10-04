using Excel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.Contacts;
using TamoCRM.Services.Contacts;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Reports;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class QualityContactsController : AdminController
    {
        //
        // GET: /Admin/QualityContacts/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetInfoQuality(FormCollection forms, HttpPostedFileBase excelfile)
        {
            if (excelfile.FileName.EndsWith(".xls") || excelfile.FileName.EndsWith(".xlsx"))
            {
                var now = DateTime.Now;
                var fullFileDir = Server.MapPath("/UploadFileQuality");
                var fullFilePath = Server.MapPath("/UploadFileQuality/") + string.Format("{0}_{1}_{2}_{3}_{4}_{5}_", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) + excelfile.FileName;
                if (!Directory.Exists(fullFileDir)) Directory.CreateDirectory(fullFileDir);
                excelfile.SaveAs(fullFilePath);

                using (var stream = System.IO.File.Open(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var dr = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        List<BC48Model> lstCts = new List<BC48Model>();
                        while (dr.Read())
                        {
                            var phone = dr[0] == null ? string.Empty : dr[0].ToString().Trim();
                            ContactInfo cts = ContactRepository.GetInfoByMobile(phone);
                            BC48Model c = new BC48Model();

                            c.FullName = cts.FullName;
                            c.Mobile = cts.Mobile;
                            c.LevelId = cts.LevelId;
                            c.Email = String.IsNullOrEmpty(cts.Email) ? " ": cts.Email;
                            c.StatusCare = String.IsNullOrEmpty(cts.StatusCare) ? " " : cts.StatusCare;
                            c.StatusMap = String.IsNullOrEmpty(cts.StatusMap) ? " " : cts.StatusMap;

                            lstCts.Add(c);
                        }
                        var localReport = new LocalReport { ReportPath = Server.MapPath("~/Areas/Admin/Reports/rptBC48.rdlc")};
                        localReport.SetParameters(new ReportParameter("Infomation", "Xuất chất lượng"));
                        var reportDataSource = new ReportDataSource("BC48", lstCts);
                        localReport.DataSources.Add(reportDataSource);

                        string[] streams;
                        Warning[] warnings;
                        string mimeType, encoding, fileNameExtension, type = "excel";
                        var renderedBytes = localReport.Render(type, string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        //Render the report             
                        return File(renderedBytes, mimeType, "BC48_" + DateTime.Now.ToString() + ".xls");
                    }
                    
                }
            }
            else
            {
                ViewBag.Message = "InvalidFileFormat";
            }
            return View();
        }

    }
}
