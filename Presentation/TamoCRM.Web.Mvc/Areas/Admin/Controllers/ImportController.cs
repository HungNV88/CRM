using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Phones;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Services.Phones;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.ActionFilters;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ImportController : AdminController
    {
        [HttpGet]
        public ActionResult GoFilter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GoFilter(GoFilterModel model, FormCollection forms)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult GoFilterCCL0()
        {
            // ChannelCCs
            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.CC);
            ViewBag.ChannelCCs = channels;

            return View();
        }

        [HttpGet]
        public ActionResult GoFilterMOL0()
        {
            // ChannelMOs
            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.MO);
            ViewBag.ChannelMOs = channels;

            return View();
        }

        public ActionResult CCL2()
        {
            var collectors = CollectorRepository.GetAll();
            collectors.Insert(0, new Domain.Collectors.CollectorInfo { CollectorId = 0, Name = "Tất cả" });
            ViewBag.Collectors = collectors;
            var channels = ChannelRepository.GetAll();
            channels.Insert(0, new Domain.Channels.ChannelInfo { ChannelId = 0, Name = "Tất cả" });
            ViewBag.Channels = channels;
            var imports = ImportExcelRepository.GetAll();
            foreach (Domain.ImportExcels.ImportExcelInfo info in imports)
            {
                if (!string.IsNullOrEmpty(info.FilePath))
                {
                    int index = info.FilePath.LastIndexOf("\\");
                    if (index > -1) info.FilePath = info.FilePath.Substring(index);
                }
            }
            imports.Insert(0, new Domain.ImportExcels.ImportExcelInfo { ImportId = 0, FilePath = "Tất cả" });
            ViewBag.ImportExcels = imports;
            return View();
        }

        //Lay danh sach CC
        public ActionResult CCL0()
        {
            // Collectors
            var collectors = CollectorRepository.GetAll();
            ViewBag.Collectors = collectors;

            // LevelCCs
            ViewBag.LevelCCs = StoreData.ListLevel.Where(x => x.LevelId < 3).ToList();

            // ChannelCCs
            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.CC);
            ViewBag.ChannelCCs = channels;

            // ImportExcelCCs
            var importExcels = ImportExcelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.CC);
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.ImportExcelCCs = importExcels;
            return View();
        }

        //Lay danh sach MO
        public ActionResult MOL0()
        {
            // Collectors
            var collectors = CollectorRepository.GetAll();
            ViewBag.Collectors = collectors;

            // LevelMOs
            ViewBag.LevelMOs = StoreData.ListLevel.Where(x => x.LevelId < 3).ToList();

            // ChannelMOs
            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.MO);
            ViewBag.ChannelMOs = channels;

            // ImportExcelMOs
            var importExcels = ImportExcelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.MO);
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.ImportExcelMOs = importExcels;

            return View();
        }

        public ActionResult ImportErrors()
        {
            var collectors = CollectorRepository.GetAll();
            collectors.Insert(0, new Domain.Collectors.CollectorInfo { CollectorId = 0, Name = "Tất cả" });
            ViewBag.Collectors1 = collectors;
            var channels = ChannelRepository.GetAll();
            channels.Insert(0, new Domain.Channels.ChannelInfo { ChannelId = 0, Name = "Tất cả" });
            ViewBag.Channels1 = channels;
            var imports = ImportExcelRepository.GetAll();
            foreach (var info in imports)
            {
                if (string.IsNullOrEmpty(info.FilePath)) continue;
                var index = info.FilePath.LastIndexOf("\\");
                if (index > -1) info.FilePath = info.FilePath.Substring(index);
            }
            imports.Insert(0, new Domain.ImportExcels.ImportExcelInfo { ImportId = 0, FilePath = "Tất cả" });
            ViewBag.ImportExcels1 = imports;
            return View();
        }

        public ActionResult DuplicateMo()
        {
            // Collectors
            var collectors = CollectorRepository.GetAll();
            ViewBag.Collectors = collectors;

            // ChannelMOs
            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.MO);
            ViewBag.ChannelMOs = channels;

            // ImportExcelMOs
            var importExcels = ImportExcelRepository.FilterForCampain(UserContext.GetDefaultBranch(), 0);
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.ImportExcelMOs = importExcels;

            return View();
        }

        [NotAuthorizeAttribute]
        public JsonResult GetForImportTeam(int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int schoolId, int statusId, int page, int rows)
        {
            var list = new MyContactListModel();
            var lstModel = new List<ContactModel>();
            int totalRecords;
            var data = ContactRepository.GetForImportTeam(branchId, collectorId, channelId, importId, levelId, sourceTypeId, schoolId, statusId, page, rows, out totalRecords);
            foreach (var info in data)
            {
                var objModel = ObjectHelper.Transform<ContactModel, ContactInfo>(info);
                var collector = CollectorRepository.GetInfo(info.CollectorId);
                var channel = ChannelRepository.GetInfo(info.ChannelId);
                var phones = PhoneRepository.GetByContact(info.Id);
                foreach (var phone in phones)
                {
                    if (phone.IsPrimary == 1)
                    {
                        objModel.Mobile = phone.PhoneNumber;
                    }
                    else if (phone.PhoneType == PhoneType.HomePhone.ToString())
                    {
                        objModel.Tel = phone.PhoneNumber;
                    }
                    else
                    {
                        objModel.Mobile2 = phone.PhoneNumber;
                    }

                }
                if (collector != null) objModel.CollectorName = collector.Name;
                if (channel != null) objModel.ChannelName = channel.Name;
                lstModel.Add(objModel);
            }
            list.UserData = totalRecords;
            list.Rows = lstModel;
            list.Page = page;
            list.Total = (totalRecords / rows) + 1;
            list.Records = rows;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DuplicateMOContainer()
        {
            // Collectors
            var collectors = CollectorRepository.GetAll();
            ViewBag.Collectors = collectors;

            // ChannelMOs
            var channels = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.MO);
            ViewBag.ChannelMOs = channels;

            // ImportExcelMOs
            var importExcels = ImportExcelRepository.FilterForMOLContainer(UserContext.GetDefaultBranch(), 0);
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.ImportExcelMOs = importExcels;

            return View();
        }
    }
}
