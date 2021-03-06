using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Core.Commons;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.ImportExcels;
using TamoCRM.Services.Branches;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Services.Levels;
using TamoCRM.Services.SourceTypes;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ImportExcelController : AdminController
    {
        [HttpPost]
        public ActionResult Upload(FormCollection forms, HttpPostedFileBase excelfile)
        {
            
            #region "Start Checkpoint"
            CheckPointApi checkPointApi = new CheckPointApi();
            var watch = new Stopwatch();
            watch.Start();
            checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "ImportExcel", "Start", 0);
            #endregion 

            //ViewBag.Filename = fileUpload.FileName;
            if (excelfile.FileName.EndsWith(".xls") || excelfile.FileName.EndsWith(".xlsx"))
            {
                var importInfo = new ImportExcelInfo
                                     {
                                         UserId = UserContext.GetCurrentUser().UserID,
                                         Status = ConvertHelper.ToInt32(forms["Status"]),
                                         LevelId = ConvertHelper.ToInt32(forms["LevelId"]),
                                         BranchId = ConvertHelper.ToInt32(forms["BranchId"]),
                                         ChannelId = ConvertHelper.ToInt32(forms["ChannelId"]),
                                         TypeId = ConvertHelper.ToInt32(forms["SourceTypeId"]),
                                         CollectorId = ConvertHelper.ToInt32(forms["CollectorId"]),
                                         
                                     };
                var now = DateTime.Now;
                var fullFileDir = Server.MapPath("/Uploads");
                var fullFilePath = Server.MapPath("/Uploads/") + string.Format("{0}_{1}_{2}_{3}_{4}_{5}_", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) + excelfile.FileName;
                if (!Directory.Exists(fullFileDir)) Directory.CreateDirectory(fullFileDir);
                excelfile.SaveAs(fullFilePath);

                importInfo.TotalRow = 0;
                importInfo.CheckCount = 0;
                importInfo.ErrorCount = 0;
                importInfo.DuplicateCount = 0;
                importInfo.FilePath = fullFilePath;
                importInfo.ImportedDate = DateTime.Now;
                var id = ImportExcelRepository.Create(importInfo);

                var socketClient = new SocketClient("localhost", Constant.PortImportExcel);
                socketClient.SendMessage(id.ToString());
                ViewBag.Message = id;
            }
            else
            {
                ViewBag.Message = "InvalidFileFormat";
            }

            #region "End CheckPoint"
            watch.Stop();
            checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "ImportExcel", "End", watch.ElapsedMilliseconds);
            #endregion

            return View();
        }


        //
        // GET: /Admin/ImportExcel/

        public ActionResult Index()
        {
            ViewBag.DefaultBranchId = UserContext.GetDefaultBranch();
            ViewBag.Collectors = CollectorRepository.GetAll();
            ViewBag.Channels = ChannelRepository.GetAll();
            ViewBag.SourceTypes = SourceTypeRepository.GetAll();
            var levels = LevelRepository.GetAll();
            var L1n2 = levels.Where(x => x.LevelId < 3).ToList();
            ViewBag.Levels = L1n2;
            ViewBag.Branches = BranchRepository.GetAll();
            return View();
        }
    }
}

