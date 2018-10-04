using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Configuration;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Channels;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Levels;
using TamoCRM.Domain.Phones;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.ImportExcels;
using TamoCRM.Services.Levels;
using TamoCRM.Services.Phones;
using TamoCRM.Services.PackageFeeEdu;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.ActionFilters;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFilter;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Services.StatusCare;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.LogsMoney;
using TamoCRM.Services.MoneyLogs;
using RestSharp;
using System.Configuration;
using TamoCRM.Services.LogFixMoney;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TamoCRM.Persitence;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.Report;
using TamoCRM.Domain.LogDashBoard;
using TamoCRM.Services.Logs;
using RestSharp.Authenticators;
using System.Diagnostics;
using static TamoCRM.Services.Logs.TmpLogServiceRepository;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ContactFilterController : AdminController
    {
        public ActionResult duplicatemo()
        {
            return View();
        }
        public ActionResult CCL1OfFilter()
        {
            return View();
        }
        public ActionResult CCL2OfFilter()
        {
            return View();
        }
        public ActionResult HOTOfFilter()
        {
            return View();
        }
        public ActionResult MOOfFilter()
        {
            return View();
        }
        public ActionResult MFOfFilter()
        {
            return View();
        }

        public ActionResult PrintForCollaborators()
        {
            var channels = ChannelRepository.GetAll();
            var lstChannelWithCount = new List<ChannelWithContactCountModel>();
            foreach (var channel in channels)
            {
                var obj = ObjectHelper.Transform<ChannelWithContactCountModel, ChannelInfo>(channel);
                lstChannelWithCount.Add(obj);
            }
            ViewBag.Channels = lstChannelWithCount;
            return View();
        }
        [HttpPost]
        [NotAuthorizeAttribute]
        public string PrintForCollaborators(PrintForCollaboratorsModel model, FormCollection forms)
        {
            // Param
            var channels = ChannelRepository.GetAll().Count > 0 ? ChannelRepository.GetAll() : ChannelRepository.GetAll();
            var maxRowPerPage = model.MaxRowsPerPage;
            var branchId = UserContext.GetDefaultBranch();
            var userId = UserContext.GetCurrentUser().UserID;
            var fromdate = string.IsNullOrEmpty(model.FromDate)
                ? DateTime.Now.Date
                : DateTime.ParseExact(model.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var todate = string.IsNullOrEmpty(model.ToDate)
               ? DateTime.Now.Date.AddDays(1).AddSeconds(-1)
               : DateTime.ParseExact(model.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
            var channelIds = string.Empty;
            var channelAmounts = string.Empty;
            foreach (var channelInfo in channels
                .Where(c => !forms.Get(c.ChannelId + "_chkChannel").IsStringNullOrEmpty())
                .Where(c => forms.Get(c.ChannelId + "_txtChannelItems").ToInt32() > 0))
            {
                var amount = forms.Get(channelInfo.ChannelId + "_txtChannelItems").ToInt32();
                channelAmounts = channelAmounts.IsStringNullOrEmpty() ? amount.ToString() : "," + amount;
                channelIds = channelIds.IsStringNullOrEmpty() ? channelInfo.ChannelId.ToString() : "," + channelInfo.ChannelId;
            }
            if (maxRowPerPage.IsIntegerNull() ||
                channelIds.IsStringNullOrEmpty() ||
                channelAmounts.IsStringNullOrEmpty())
                return string.Empty;

            // Print
            var printId = ContactRepository.ContactPrintAll(branchId, fromdate, todate, channelIds, channelAmounts, maxRowPerPage, userId, DateTime.Now);
            return printId.ToString();
        }

        public ActionResult FilterContactReuse()
        {
            return View();
        }

        public ActionResult FilterContactSearch()
        {
            return View();
        }

        public ActionResult FilterContactCCL2Plus()
        {
            return View();
        }
        public ActionResult FilterContactRecovered()
        {
            var listStatusCare = StatusCareRepository.GetAll();
            const int total = 2;
            var array = new List<List<StatusCareInfo>>();
            for (var i = 0; i < total; i++)
            {
                var divice = (int)Math.Ceiling((double)listStatusCare.Count / (total - i));
                array.Add(listStatusCare.Take(divice).ToList());
                listStatusCare.RemoveRange(0, divice);
            }
            ViewBag.ArrayStatusCare = array;
            return View();
        }
        public ActionResult FilterContactRecoveredDistributor()
        {
            var listStatusCare = StatusCareRepository.GetAll();
            const int total = 2;
            var array = new List<List<StatusCareInfo>>();
            for (var i = 0; i < total; i++)
            {
                var divice = (int)Math.Ceiling((double)listStatusCare.Count / (total - i));
                array.Add(listStatusCare.Take(divice).ToList());
                listStatusCare.RemoveRange(0, divice);
            }
            ViewBag.ArrayStatusCare = array;
            return View();
        }

        public ActionResult FilterContactForImporter()
        {
            var statusConnectTypes = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(StatusConnectType)))
                statusConnectTypes.Add((int)item, ObjectExtensions.GetEnumDescription((StatusConnectType)item));
            ViewBag.StatusConnectTypes = statusConnectTypes.Select(c => new { Id = c.Key, Name = c.Value });

            var importExcels = ImportExcelRepository.FilterForCampain(UserContext.GetDefaultBranch());
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.ImportExcels = importExcels;

            var channelCCs = ChannelRepository.FilterForCampain(UserContext.GetDefaultBranch());
            ViewBag.ChannelCCs = channelCCs;
            return View();
        }

        [NotAuthorizeAttribute]
        public JsonResult GetForFilterTeam(int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int schoolId, int statusId, int page, int rows)
        {
            var list = new MyContactListModel();
            var lstModel = new List<ContactModel>();
            int totalRecords;
            var data = ContactRepository.GetForFilterTeam(branchId, collectorId, channelId, importId, levelId, sourceTypeId, schoolId, statusId, page, rows, out totalRecords);
            foreach (var info in data)
            {
                var objModel = ObjectHelper.Transform<ContactModel, ContactInfo>(info);
                objModel.StatusName = ObjectExtensions.GetEnumDescription((StatusType)info.StatusId);
                var contactLevel = ContactLevelInfoRepository.GetInfo(info.Id);
                if (contactLevel != null)
                {
                    objModel.Notes = contactLevel.EducationSchoolName;
                }
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
        public int CountByChannel(int branchId, int sourceTypeId, int levelId, int channelId, string from, string to)
        {
            try
            {
                var dtFrom = string.IsNullOrEmpty(from)
                                 ? DateTime.Now.Date
                                 : DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InstalledUICulture);
                var dtTo = string.IsNullOrEmpty(from)
                                 ? DateTime.Now.AddDays(1).Date
                                 : DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InstalledUICulture);
                return ContactRepository.CountByChannel(branchId, sourceTypeId, levelId, channelId, dtFrom, dtTo);
            }
            catch
            {
                return 0;
            }
        }
        public JsonResult BuilGridAcceptL2()
        {
            var objListModel = new AcceptL2ContactListModel();
            var data = new List<AcceptL2ContactModel>();
            for (var i = 0; i < 10; i++)
            {
                var objModel = new AcceptL2ContactModel();
                data.Add(objModel);
            }
            objListModel.Rows = data;
            objListModel.Page = 1;
            objListModel.Total = 1;
            objListModel.Records = 10;
            return new JsonResult { Data = objListModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #region Handover
        public ActionResult FilterContactHandover()
        {
            // Check account user form
            var status = StoreData.CheckAccountUseHandover(EmployeeType.Consultant);

            // ImportExcelCCs
            var importExcels = ImportExcelRepository.FilterForCampain(UserContext.GetDefaultBranch(), (int)SourceType.CC);
            foreach (var item in importExcels) item.FilePath = (new FileInfo(item.FilePath)).Name;
            ViewBag.Channels = ChannelRepository.GetAll();
            ViewBag.ImportExcelCCs = importExcels;
            ViewBag.StatusMessage = status;
            ViewBag.StatusCares = StatusCareRepository.GetAll();
            ViewBag.StatusMaps = StatusMapRepository.GetAll();

            return View(new ContactCreateModel());
        }
        public ActionResult FilterContactHandoverCollaborator()
        {
            // Check account user form
            var status = StoreData.CheckAccountUseHandover(EmployeeType.Collaborator);
            ViewBag.Channels = ChannelRepository.GetAll();
            ViewBag.StatusMessage = status;
            return View(new ContactCreateModel());
        }
        #endregion

        #region Recovery
        public ActionResult FilterContactRecovery()
        {
            return View();
        }
        public ActionResult FilterContactRecoveryCollaborator()
        {
            return View();
        }
        #endregion

        #region Change
        public ActionResult FilterContactForChange()
        {
            return View();
        }
        public ActionResult FilterContactForChangeCollaborator()
        {
            return View();
        }
        #endregion

        #region Search
        public ActionResult FilterContactSearchFast()
        {
            return View();
        }
        public ActionResult FilterContactSearchFastCollaborator()
        {
            return View();
        }
        public ActionResult FilterContactSearchFastManagerContact()
        {
            return View();
        }
        
        public ActionResult FilterContactSearchManager()
        {
            return View();
        }

        public ActionResult FilterContactSearchFowardManager()
        {
            return View();
        }

        public ActionResult FilterContactSearchManagerCollaborator()
        {
            return View();
        }

        public ActionResult HandoverContactFast()
        {
            return View();
        }    

        //Danh sach contact
        public ActionResult FilterContactSearchAdvance()
        {

            #region "Log Checkpoint - Begin Load Danh sach contact cho TVTS 28/10/2016"
            int SessionLog = TmpJobReportRepository.GetSessionLog();
            ViewBag.SessionLog = SessionLog;
            DateTime TimeBegin = DateTime.Now;
            ViewBag.TimeBegin = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff");

            var logbegin = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "BEGIN_LOAD_DANH_SACH_CONTACT_TVTS",
                ContactId = 0,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };
            LogDashBoardRepository.CreateLogDashBoard(logbegin);
            #endregion

            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = user.IsCollaborator
                ? EmployeeType.Collaborator
                : user.IsConsultant ? EmployeeType.Consultant : EmployeeType.All;
            var userIds = user.UserID.ToString();
           
            var levels = LevelRepository.GetAllWithContactCount(userIds, branchId, employeeType) ?? new List<LevelInfo>();
            foreach (var level in StoreData.ListLevel.Where(c => !levels.Exists(p => p.LevelId == c.LevelId)))
                levels.Add(level);
            ViewBag.LevelWithContactCount = levels.OrderBy(c => c.LevelId).ToList();

            #region "Log Checkpoint - End Load Danh sach contact cho TVTS 28/10/2016 "
            DateTime TimeEnd = DateTime.Now;
            var logend = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeEnd.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "END_CONTROLLER_LOAD_DANH_SACH_CONTACT_TVTS",
                ContactId = 0,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };
            LogDashBoardRepository.CreateLogDashBoard(logend);
            #endregion

            return View();
        }
        public ActionResult FilterContactSearchAdvanceManager()
        {
            return View();
        }
        public ActionResult FilterContactSearchAdvanceCollaborator()
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = user.IsCollaborator
                ? EmployeeType.Collaborator
                : user.IsConsultant ? EmployeeType.Consultant : EmployeeType.All;
            var userIds = user.UserID.ToString();
            //if (user.IsCollaborator)
            //{
            //    if (user.GroupCollaboratorType == (int)GroupCollaboratorType.LeaderCollaborator)
            //        userIds = string.Join(",", StoreData.ListUser.Where(c => c.GroupId == user.GroupId).Select(c => c.UserID).Distinct());
            //    else if (user.GroupCollaboratorType == (int)GroupCollaboratorType.ManagerCollaborator)
            //        userIds = string.Empty;
            //}
            //else if (user.IsConsultant)
            //{
            //    if (user.GroupConsultantType == (int)GroupConsultantType.LeaderConsultant)
            //        userIds = string.Join(",", StoreData.ListUser.Where(c => c.GroupId == user.GroupId).Select(c => c.UserID).Distinct());
            //    else if (user.GroupConsultantType == (int)GroupConsultantType.ManagerConsultant)
            //        userIds = string.Empty;
            //}
            var levels = LevelRepository.GetAllWithContactCount(userIds, branchId, employeeType) ?? new List<LevelInfo>();
            foreach (var level in StoreData.ListLevel.Where(c => !levels.Exists(p => p.LevelId == c.LevelId)))
                levels.Add(level);
            ViewBag.LevelWithContactCount = levels.OrderBy(c => c.LevelId).ToList();
            return View();
        }
        #endregion

        #region Duplicate
        public ActionResult FilterContactDuplicate()
        {
            return View();
        }
        public ActionResult FilterContactDuplicateCollaborator()
        {
            return View();
        }
        public ActionResult FilterContactDuplicateConsultant()
        {
            return View();
        }
        #endregion

        #region Hiện danh sách contact chưa bàn giao sang Advisor
        public ActionResult ListContactHandoverAdvisor()
        {
            return View();
        }

        #endregion

        #region Hiện danh sách contact chưa bàn giao sang Advisor (cho Quản lý TVTS)
        public ActionResult ListContactHandoverAdvisorManager()
        {
            return View();
        }

        #endregion

        #region Today
        public ActionResult FilterContactToday()
        {
            #region "Start Checkpoint BEGIN_LOAD_LICH_LAM_VIEC_CONTACT"
            int SessionLog = TmpJobReportRepository.GetSessionLog();
            ViewBag.SessionLog = SessionLog;
            DateTime TimeBegin = DateTime.Now;
            ViewBag.TimeBegin = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff");

            var logbegin = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "BEGIN_LOAD_LICH_LAM_VIEC_CONTACT",
                ContactId = 0,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            LogDashBoardRepository.CreateLogDashBoard(logbegin);
            #endregion

            var branchId = UserContext.GetDefaultBranch();
            var userId = UserContext.GetCurrentUser().UserID;

            var user = UserContext.GetCurrentUser();
            var employeeType = user.IsCollaborator
                ? EmployeeType.Collaborator
                : user.IsConsultant ? EmployeeType.Consultant : EmployeeType.All;
            
           
            var levels = LevelRepository.GetAllWithContactCountForConsultant(userId, branchId, employeeType) ?? new List<LevelInfo>();
            foreach (var level in StoreData.ListLevel
                          .Where(c => !levels.Exists(p => p.LevelId == c.LevelId)))
                levels.Add(level);
            ViewBag.LevelWithContactCount = levels.OrderBy(c => c.LevelId).ToList();

            #region "Start Checkpoint END_CONTROLLER_LOAD_LICH_LAM_VIEC_CONTACT"
            DateTime TimeEnd = DateTime.Now;
            var logend = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeEnd.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "END_CONTROLLER_LOAD_LICH_LAM_VIEC_CONTACT",
                ContactId = 0,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            LogDashBoardRepository.CreateLogDashBoard(logend);
            #endregion

            return View();
        }

        public ActionResult FilterContactTodayCollaborator()
        {
            //int totalExists;
            //int handoverCount;
            //int completeInCount;
            //int completeOutCount;
            //int notCompleteCount;

            var branchId = UserContext.GetDefaultBranch();
            var userId = UserContext.GetCurrentUser().UserID.ToString();
            //ContactRepository.ContactsFilterStatictsToday(userId, branchId, DateTime.Now, out handoverCount, out completeInCount, out completeOutCount, out notCompleteCount, EmployeeType.Collaborator);
            //ContactRepository.FilterTodayAll(userId, DateTime.Now.AddMonths(-1).AddDays(-1).Date, DateTime.Now.Date.AddSeconds(-1), branchId, EmployeeType.Collaborator, 1, 1, out totalExists);
            //ViewBag.HandoverCount = handoverCount;
            //ViewBag.CompleteInCount = completeInCount;
            //ViewBag.CompleteOutCount = completeOutCount;
            //ViewBag.NotCompleteCount = notCompleteCount;
            //ViewBag.TotalExists = totalExists;

            var user = UserContext.GetCurrentUser();
            var employeeType = user.IsCollaborator
                ? EmployeeType.Collaborator
                : user.IsConsultant ? EmployeeType.Consultant : EmployeeType.All;
            var userIds = user.UserID.ToString();
            //if (user.IsCollaborator)
            //{
            //    if (user.GroupCollaboratorType == (int)GroupCollaboratorType.LeaderCollaborator)
            //        userIds = string.Join(",", StoreData.ListUser.Where(c => c.GroupId == user.GroupId).Select(c => c.UserID).Distinct());
            //    else if (user.GroupCollaboratorType == (int)GroupCollaboratorType.ManagerCollaborator)
            //        userIds = string.Empty;
            //}
            //else if (user.IsConsultant)
            //{
            //    if (user.GroupConsultantType == (int)GroupConsultantType.LeaderConsultant)
            //        userIds = string.Join(",", StoreData.ListUser.Where(c => c.GroupId == user.GroupId).Select(c => c.UserID).Distinct());
            //    else if (user.GroupConsultantType == (int)GroupConsultantType.ManagerConsultant)
            //        userIds = string.Empty;
            //}
            var levels = LevelRepository.GetAllWithContactCount(userIds, branchId, employeeType) ?? new List<LevelInfo>();
            foreach (var level in StoreData.ListLevel
                .Where(c => c.LevelId <= 3)
                .Where(c => !levels.Exists(p => p.LevelId == c.LevelId)))
                levels.Add(level);
            ViewBag.LevelWithContactCount = levels.OrderBy(c => c.LevelId).ToList();

            return View();
        }

        public ActionResult FilterContactTodayManager()
        {
            return View();
        }
        #endregion

        public ActionResult ListContactHandoverSuccessAdvisor()
        {
            return View();
        }

        public ActionResult ListContactHandoverSuccessAdvisorManager()
        {
            ViewBag.PackageFeeEdu = PackageFeeEduRepository.GetAll();
            return View();
        }

        public ActionResult ListContactHandoverSuccessAdvisorManagerL7()
        {
            ViewBag.PackageFeeEdu = PackageFeeEduRepository.GetAll();
            return View();
        }

        #region Deleted
        public ActionResult FilterContactDeleted()
        {
            return View();
        }
        #endregion

        #region Fowarded
        public ActionResult FilterContactForward()
        {
            return View();
        }
        #endregion

        public ActionResult SearchTopicaTestInfo()
        {
            return View();
        }

        public ActionResult FixedMoneyManagerConsulant()
        {

            return View();
        }

        [HttpPost]
        public ActionResult FixedMoneyManagerConsulant(FormCollection forms)
        {
            var valueRow = Request["countRowTable"].ToInt32();

            List<ContactFixedMoney> lstInfoCts = new List<ContactFixedMoney>();
            AllDealMoney listDealSesssion = new AllDealMoney();
            listDealSesssion.pricing = new List<ContactInfoMoney>();
            listDealSesssion.deposit = new List<ContactInfoMoney>();
            listDealSesssion.transfer = new List<ContactInfoMoneyTranfer>();
            listDealSesssion.balance = new List<ContactInfoMoneyBalance>();

            string notes = Request["NotesManagerConsulant"].ToString();

            var userTVTS = UserContext.GetCurrentUser();

            for (int i = 1; i <= valueRow; i++)
            {
                ContactInfoMoney infoCts = new ContactInfoMoney();

                infoCts.ContactId = Request["CodeCts_" + i.ToString()].ToString(); 
                infoCts.UserName = Request["NameCts_" + i.ToString()].ToString();
                infoCts.UserPhone = forms.Get("MobileCts_" + i.ToString()).ToString();
                infoCts.UserEmail = Request["EmailCts_" + i.ToString()].ToString(); 
                infoCts.TransactionBy = "CRM";
                infoCts.Reason = Request["NotesManagerConsulant"].ToString(); 
                infoCts.OtherInfo = "";
                infoCts.Time = DateTime.Now.ToString();
                infoCts.DisableWarning = true;                

                if (forms.Get("FeeEdu_" + i.ToString()).ToInt32() > forms.Get("FeeEduChange_" + i.ToString()).ToInt32())
                {
                    int feeEdu = forms.Get("FeeEdu_" + i.ToString()).ToInt32();
                    int feeEduChange = forms.Get("FeeEduChange_" + i.ToString()).ToInt32();
                    int feeReturn = forms.Get("FeeReturn_" + i.ToString()).ToInt32();

                    infoCts.Value = (forms.Get("FeeEdu_" + i.ToString()).ToInt32()
                                    - forms.Get("FeeEduChange_" + i.ToString()).ToInt32()
                                    - forms.Get("FeeReturn_" + i.ToString()).ToInt32()).ToString();
                    if(infoCts.Value != "0")
                    {
                        listDealSesssion.pricing.Add(infoCts);
                    }
                }
                else if (forms.Get("FeeEdu_" + i.ToString()).ToInt32() < forms.Get("FeeEduChange_" + i.ToString()).ToInt32())
                {
                    infoCts.Value = (forms.Get("FeeEduChange_" + i.ToString()).ToInt32()
                                    - forms.Get("FeeEdu_" + i.ToString()).ToInt32()
                                    - forms.Get("FeeReturn_" + i.ToString()).ToInt32()).ToString();
                    listDealSesssion.deposit.Add(infoCts);
                }
                else
                {
                    //nothing 
                }

                if (forms.Get("FeeReturn_" + i.ToString()).ToInt32() > 0)
                {
                    //tranfer
                    ContactInfoMoneyTranfer infoCtsTranfer = new ContactInfoMoneyTranfer();

                    infoCtsTranfer.ContactId = Request["CodeCts_" + i.ToString()].ToString();
                    infoCtsTranfer.Value = Request["NameCts_" + i.ToString()].ToString();
                    infoCtsTranfer.UserPhone = forms.Get("MobileCts_" + i.ToString()).ToString();
                    infoCtsTranfer.UserEmail = Request["EmailCts_" + i.ToString()].ToString();
                    infoCtsTranfer.TransactionBy = "CRM";
                    infoCtsTranfer.Reason = Request["NotesManagerConsulant"].ToString();
                    infoCtsTranfer.OtherInfo = "";
                    infoCtsTranfer.Time = DateTime.Now.ToString();
                    infoCtsTranfer.DisableWarning = true;

                    infoCtsTranfer.Value = forms.Get("FeeReturn_" + i.ToString()).ToString();
                    listDealSesssion.transfer.Add(infoCtsTranfer);

                    //balance
                    ContactInfoMoneyBalance infoCtsBalance = new ContactInfoMoneyBalance();

                    infoCtsBalance.ContactId = Request["CodeCts_" + i.ToString()].ToString();
                    infoCtsBalance.Value = Request["NameCts_" + i.ToString()].ToString();
                    infoCtsBalance.UserPhone = forms.Get("MobileCts_" + i.ToString()).ToString();
                    infoCtsBalance.UserEmail = Request["EmailCts_" + i.ToString()].ToString();
                    infoCtsBalance.Type = "-1";
                    infoCtsBalance.BillNumber = "0";
                    infoCtsBalance.TransactionBy = "CRM";
                    infoCtsBalance.Reason = Request["NotesManagerConsulant"].ToString();
                    infoCtsBalance.OtherInfo = "";
                    infoCtsBalance.Time = DateTime.Now.ToString();
                    infoCtsBalance.DisableWarning = true;

                    infoCtsBalance.Value = forms.Get("FeeReturn_" + i.ToString()).ToString();
                    listDealSesssion.balance.Add(infoCtsBalance);
                }
            }

            JsonResult retVal = new JsonResult() { Data = listDealSesssion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            string sRetVal = JsonConvert.SerializeObject(retVal.Data);

            //===============Goi API LocMX =============//
          
            string username = "crm";
            string password = "crm.topica.";
            string linkApiPayment = ConfigurationManager.AppSettings["LinkAPIPayment"].ToString();

            var client = new RestClient(linkApiPayment);
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("Tcoin/CrmTransactionBatch", Method.POST);

            request.AddHeader("Accept", "application/json");

            request.AddParameter("key", "6WxDCFTjlgjH6L6YLA03LNW1JbJbCZGCLHa0DIXT");
            request.AddParameter("data", sRetVal);
            request.JsonSerializer.ContentType = "application/json; charset=utf-8";

            IRestResponse response = client.Execute(request);
            string s = response.Content;
            ReturnApiCallFixedMoney output = JsonConvert.DeserializeObject<ReturnApiCallFixedMoney>(s);
            
            //Neu goi thanh cong tao log save lai giao dich trong bang LogFixedMoney           
            if (output.status_code == "DONE_ALL")  
            {
                SqlTransaction tran = null;
                try
                {  
                    string conString = getNewConnection;
                    SqlConnection conn = new SqlConnection(conString);
                    conn.Open();
                    tran = conn.BeginTransaction();

                    //Update FeeEdu, PackageSale
                    for (int i = 1; i <= valueRow; i++)
                    {
                        var sCode = Request["CodeCts_" + i.ToString()].ToString();
                        var sFeeEdu = Request["FeeEduChange_" + i.ToString()].ToInt32();
                        ContactLevelInfoRepository.Update_FeeEdu_PackagePriceSale(sCode, sFeeEdu);
                    }
                    var log = new LogFixedMoney
                    {
                        Description = sRetVal,
                        Notes = notes,
                        UserConsulantCreate =  userTVTS.UserName,
                        CreateDate = DateTime.Now
                    };

                    LogFixMoneyRepository.Create(log, tran);

                    int idMax = GetIdentity("LogFixMoney", conn, tran); //lay ID max trong bang LogFixMoney

                    foreach (var a in listDealSesssion.pricing)
                    {
                        var logmoney_pricing = new LogsMoneyInfo
                        {
                            TienHVNop = a.Value.ToInt32(),
                            TienBanGoi = 0, //hoi lai
                            NoteChuyenKhoan = a.Reason,
                            NguoiTao = a.UserName,
                            NgayThucThu = DateTime.Now,
                            KieuThanhToan = 0,
                            DiaPhuong = 1,
                            CreateDate = DateTime.Now,
                            ContactId = 0,
                            Code = a.ContactId,
                            ChuDuToan = 0,
                            TrangThai = true,
                            HistoryId = 0,
                            IdFixMoney = idMax
                        };
                        MoneyLogsRepository.Create(logmoney_pricing, tran);
                    }

                    foreach (var a in listDealSesssion.deposit)
                    {
                        var logmoney_deposit = new LogsMoneyInfo
                        {
                            TienHVNop = a.Value.ToInt32(),
                            TienBanGoi = 0, //hoi lai
                            NoteChuyenKhoan = a.Reason,
                            NguoiTao = a.UserName,
                            NgayThucThu = DateTime.Now,
                            KieuThanhToan = 0,
                            DiaPhuong = 1,
                            CreateDate = DateTime.Now,
                            ContactId = 0,
                            Code = a.ContactId,
                            ChuDuToan = 0,
                            TrangThai = true,
                            HistoryId = 0,
                            IdFixMoney = idMax
                        };
                        MoneyLogsRepository.Create(logmoney_deposit, tran);
                    }

                    foreach (var a in listDealSesssion.transfer)
                    {
                        var logmoney_transfer = new LogsMoneyInfo
                        {
                            TienHVNop = a.Value.ToInt32(),
                            TienBanGoi = 0, //hoi lai
                            NoteChuyenKhoan = a.Reason,
                            NguoiTao = "",
                            NgayThucThu = DateTime.Now,
                            KieuThanhToan = 0,
                            DiaPhuong = 1,
                            CreateDate = DateTime.Now,
                            ContactId = 0,
                            Code = a.ContactId,
                            ChuDuToan = 0,
                            TrangThai = true,
                            HistoryId = 0,
                            IdFixMoney = idMax
                        };
                        MoneyLogsRepository.Create(logmoney_transfer, tran);
                    }
                    tran.Commit();

                    ViewBag.Message = "Giao dịch chuyển tiền thành công";

                    return FixedMoneyManagerConsulant();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.Data);
                    if (tran != null)
                        tran.Rollback();
                    ViewBag.Message = "Có lỗi xảy ra, vui lòng thực hiện lại";
                    return FixedMoneyManagerConsulant();
                }
            }
            else {
                //return lỗi ko thành công.
                ViewBag.Message = "Giao dịch chuyển tiền không thành công: " + output.msg;
                return FixedMoneyManagerConsulant();
            }
        }

        public int GetIdentity(string tableName, SqlConnection connection, SqlTransaction transaction)
        {
            int identity;

            // Get the last customer identity
            using (var sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText = string.Format("SELECT IDENT_CURRENT('{0}')", tableName);
                sqlCommand.Transaction = transaction;
                identity = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }

            return identity;
        }

        public static string getNewConnection
        {
            get { return ConfigurationManager.ConnectionStrings["LocalSql"].ConnectionString; }
        }


        //List danh sach contact dang ky lai
        public ActionResult ListContactDuplicateManager()
        {
            return View();
        }
    }
}






 



