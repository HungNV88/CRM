using Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Services.CasecAccounts;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class TopicaCasecController : AdminController
    {
        //
        // GET: /Admin/TopicaCasec/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SumAccountTopicaCasec()
        {
            return View();
        }
        
        public ActionResult CasecAccountInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CasecAccountInfo(FormCollection forms, HttpPostedFileBase excelfile)
        {
            if (excelfile.FileName.EndsWith(".xls") || excelfile.FileName.EndsWith(".xlsx"))
            {
                var now = DateTime.Now;
                var fullFileDir = Server.MapPath("/UploadFileCasecAccount");
                var fullFilePath = Server.MapPath("/UploadFileCasecAccount/") + string.Format("{0}_{1}_{2}_{3}_{4}_{5}_", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) + excelfile.FileName;
                if (!Directory.Exists(fullFileDir)) Directory.CreateDirectory(fullFileDir);
                excelfile.SaveAs(fullFilePath);

                List<CasecAccountInfo> list = new List<CasecAccountInfo>();
                list = CasecAccountRepository.GetAll();
                using (var stream = System.IO.File.Open(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var dr = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        while(dr.Read())
                        {
                            if(dr[0] != null)
                            {
                                string account = dr[0].ToString();
                                string password = dr[1] == null ? string.Empty : dr[1].ToString();

                                bool check = list.Any(x => x.Account == account);
                                if (!check)
                                {
                                    CasecAccountRepository.Insert(account, password);
                                }
                              
                            }
                            
                        }
                    }
                }
                ViewBag.Message = "Update thêm tài khoản thành công,  vui lòng check lại số lượng tài khoản";
            }
            else
            {
                ViewBag.Message = "File ko đúng định dạng";
            }
            return View();
        }
    }
}
