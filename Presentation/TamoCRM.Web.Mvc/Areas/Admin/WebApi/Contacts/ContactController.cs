using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Channels;
using TamoCRM.Domain.ContactDeleted;
using TamoCRM.Domain.ContactForward;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Levels;
using TamoCRM.Domain.Phones;
using TamoCRM.Domain.SourceTypes;
using TamoCRM.Domain.StatusMap;
using TamoCRM.Domain.UserRole;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Reports;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.Channels;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Phones;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.UserDraft;
using TamoCRM.Services.UserRole;
using TamoCRM.Services.TopicaAccounts;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactDeleted;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFowarded;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactFilter;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts;
using TamoCRM.Core.Commons.Extensions;
using UserDraftInfo = TamoCRM.Domain.UserDraft.UserDraftInfo;
using Microsoft.Reporting.WebForms;
using Excel;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Reports;
using static TamoCRM.Services.Logs.TmpLogServiceRepository;
using TamoCRM.Domain.LogDashBoard;
using TamoCRM.Services.Report;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using TamoCRM.ImportExcel.Library;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Contacts
{
    public class ContactController : CustomApiController
    {

        [HttpGet]
        public IEnumerable<ContactInfo> SearchTopicaTest(string accounttopica)
        {
            return TopicaAccountRepository.GetInfoTopicaTest(accounttopica);
        }

        [HttpPost]
        public string CheckDuplicate(FormDataCollection form)
        {
            string mobile1 = form.Get("mobile1");
            string mobile2 = form.Get("mobile2");
            string tel = form.Get("tel");
            string email = form.Get("email");
            int duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(mobile1, mobile2, tel, email, string.Empty);
            return duplicateId.ToString();
        }

        // GET api/<controller>
        public IEnumerable<ContactInfo> Get()
        {
            return ContactRepository.GetAll();
        }

        [HttpPost]
        public string Count(FormDataCollection form)
        {
            var schoolIds = form.Get("schoolIds");
            var channelIds = form.Get("channelIds");
            var levelId = form.Get("levelId").ToInt32();
            var statusId = form.Get("statusId").ToInt32();
            var sourceTypeIds = form.Get("sourceTypeIds");

            var strfromDate = form.Get("fromDate");
            var strtoDate = form.Get("toDate");
            try
            {
                var toDate = strtoDate.ToDateTime();
                if (!toDate.HasValue) toDate = DateTime.Now.Date;
                toDate = toDate.Value.AddDays(1).AddSeconds(-1);

                var fromDate = strfromDate.ToDateTime();
                if (!fromDate.HasValue) fromDate = DateTime.Now.Date;

                schoolIds = schoolIds.Trim('|').Replace('|', ',');
                channelIds = channelIds.Trim('|').Replace('|', ',');
                sourceTypeIds = sourceTypeIds.Trim('|').Replace('|', ',');

                var retVal = ContactRepository.Count(sourceTypeIds, channelIds, schoolIds, levelId, statusId, fromDate.Value, toDate.Value);
                return retVal.ToString();
            }
            catch
            {
                return "0";
            }
        }

        [HttpGet]
        public ContactListModel GetAllHandoverContactAdvisor(string name, string phone, string email, int statusHandoverId, int page, int rows)
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var userid = user.UserID;
            List<ContactInfo> listContacts = new List<ContactInfo>();

            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.GetHandoverContactAdvisorList(userid, name, phone, email, statusHandoverId, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };

            foreach (var contact in list.Rows)
            {
                contact.sHandoverStatusAdvisor = ObjectExtensions.GetEnumDescription((StatusHandoverAdvisor)contact.iHandoverStatusAdvisor);
            }
            return list;
        }

        public ContactListModel GetAllHandoverContactAdvisorManager(string name, string phone, string email, int statusHandoverId, int page, int rows)
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var userid = user.UserID;
            List<ContactInfo> listContacts = new List<ContactInfo>();

            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.GetHandoverContactAdvisorManagerList(branchId, name, phone, email, statusHandoverId, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            int tongTien = 0;
            foreach (var contact in list.Rows)
            {
                contact.sHandoverStatusAdvisor = ObjectExtensions.GetEnumDescription((StatusHandoverAdvisor)contact.iHandoverStatusAdvisor);
                tongTien += contact.FeeEdu.ToInt32();
            }
            list.TotalMoney = tongTien;
            return list;
        }

        #region Lấy danh sách contact bàn giao thành công
        public ContactListModel GetAllHandoverSuccessContactAdvisor(string name, string phone, string email, int page, int rows)
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var userid = user.UserID;
            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.GetHandoverSuccessContactAdvisorList(userid, name, phone, email, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };

            foreach (var contact in list.Rows)
            {
                contact.sHandoverStatusAdvisor = ObjectExtensions.GetEnumDescription((StatusHandoverAdvisor)contact.iHandoverStatusAdvisor);
            }
            return list;
        }
        #endregion

        [HttpPost]
        public MyContactListModel GetForAcceptance(FormDataCollection form)
        {
            var printId = ConvertHelper.ToInt32(form.Get("printId"));
            var pageCode = ConvertHelper.ToString(form.Get("pageCode"));
            var levelId = ConvertHelper.ToInt32(form.Get("levelId"));
            var page = ConvertHelper.ToInt32(form.Get("page"));
            var rows = ConvertHelper.ToInt32(form.Get("rows"));
            int totalRecords;
            var list = new MyContactListModel();
            var lstModel = new List<ContactModel>();
            var data = ContactRepository.GetForAcceptance(printId, pageCode, levelId, page, rows, out totalRecords) ?? new List<ContactInfo>();
            if (!data.IsNullOrEmpty())
            {
                var lstPhones = PhoneRepository.GetByContacts(data.Select(c => c.Id.ToString()).Aggregate((total, curent) => total + "," + curent));
                foreach (var info in data)
                {
                    var objModel = new ContactModel();
                    ObjectHelper.Transform(info, ref objModel);

                    var contactLevel = ContactLevelInfoRepository.GetInfo(info.Id);
                    ObjectHelper.Transform(contactLevel, ref objModel);

                    foreach (var phone in lstPhones.Where(c => c.ContactId == info.Id))
                    {
                        if (phone.IsPrimary == 1) objModel.Mobile = phone.PhoneNumber;
                        if (phone.IsPrimary != 1 && phone.PhoneType == PhoneType.HomePhone.ToString())
                            objModel.Tel = phone.PhoneNumber;
                        if (phone.IsPrimary != 1 && phone.PhoneType == ((int)PhoneType.MobilePhone).ToString())
                            objModel.Mobile2 = phone.PhoneNumber;
                    }

                    // StatusName
                    objModel.StatusName = ObjectExtensions.GetEnumDescription((StatusType)objModel.StatusId);

                    // StatusMap
                    if (objModel.StatusMapDistributorId != null)
                        objModel.StatusMapId = objModel.StatusMapDistributorId.Value;
                    lstModel.Add(objModel);
                }
            }

            list.UserData = totalRecords;
            list.Rows = lstModel;
            list.Page = page;
            list.Total = (totalRecords / rows) + 1;
            list.Records = rows;
            return list;
        }

        public MyContactListModel Get(string action, int branchId, string importDate, int importId, int page, int rows)
        {
            int totalRecords;
            var list = new MyContactListModel();
            var lstModel = new List<ContactModel>();
            var importFDate = importDate.ToDateTime("ddMMyyyy");
            var data = ContactRepository.SearchDuplicateMo(branchId, importFDate, importId, page, rows, out totalRecords);
            foreach (var info in data)
            {
                var objModel = ObjectHelper.Transform<ContactModel, ContactInfo>(info);
                var collector = CollectorRepository.GetInfo(info.CollectorId);
                var channel = ChannelRepository.GetInfo(info.ChannelId);
                var phones = PhoneRepository.GetByContact(info.Id);
                foreach (PhoneInfo phone in phones)
                {
                    if (phone.IsPrimary == 1)
                    {
                        objModel.Mobile = phone.PhoneNumber;
                    }
                    else if (phone.PhoneType == PhoneType.HomePhone.ToString())
                    {
                        objModel.Tel = phone.PhoneNumber;
                    }
                    else {
                        objModel.Mobile2 = phone.PhoneNumber;
                    }
                }
                objModel.UserName = info.UserName;
                objModel.CallConsultantDate = info.CallConsultantDate;
                if (collector != null) objModel.CollectorName = collector.Name;
                if (channel != null) objModel.ChannelName = channel.Name;

                lstModel.Add(objModel);
            }
            list.UserData = totalRecords;
            list.Rows = lstModel;
            list.Page = page;
            list.Total = (totalRecords / rows) + 1;
            list.Records = rows;
            return list;
        }
        public MyContactListModel Get(string action, int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int schoolId, int statusId, string importDate, int page, int rows)
        {
            int totalRecords;
            var list = new MyContactListModel();
            var lstModel = new List<ContactModel>();
            var importFDate = importDate.ToDateTime("ddMMyyyy");
            var data = action == "duplicatemo"
                            ? ContactRepository.SearchDuplicateMo(collectorId, channelId, importId, levelId, sourceTypeId, branchId, importFDate, page, rows, out totalRecords)
                            : ContactRepository.Search(collectorId, channelId, importId, levelId, sourceTypeId, statusId, page, rows, out totalRecords);

            foreach (var info in data)
            {
                var objModel = ObjectHelper.Transform<ContactModel, ContactInfo>(info);
                var collector = CollectorRepository.GetInfo(info.CollectorId);
                var channel = ChannelRepository.GetInfo(info.ChannelId);
                var phones = PhoneRepository.GetByContact(info.Id);
                foreach (PhoneInfo phone in phones)
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
            return list;
        }

        // GET api/<controller>/5
        public ContactInfo Get(int id)
        {
            return ContactRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("Id").Split(',')[0]);
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "del":
                        var entity = InitContactDeletedInfo(id);
                        ContactRepository.Delete(id, entity, EmployeeType.Consultant);
                        break;
                }
            }
            return retVal;
        }

        [HttpPost]
        public ContactFixedMoney GetInfoByPhoneContact(FormDataCollection form) //Lấy thông tin cho nghiệp vụ sửa tiền
        {
            ContactFixedMoney listContactsInfo = new ContactFixedMoney();
            string mobile = form.Get("mobile").ToString();
            listContactsInfo = ContactRepository.GetInfoFeeEduByMobile(mobile);
            return listContactsInfo;
        }

        [HttpPost]
        public string EditFullName(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("Id").Split(',')[0]);
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "edit":
                        var fullName = form.Get("Fullname");
                        ContactRepository.ContactUpdateFullName(id, fullName);
                        break;
                }
            }
            return retVal;
        }

        [HttpGet]
        public ContactRecoveryModel FilterRecovered(string sourceTypes, string levels, int statusMapId, int statusCareId, int branchId, string recoveryFDate, string recoveryTDate, int page, int rows)
        {
            int totalRecords;

            var recoveryToDate = recoveryTDate.ToDateTime("ddMMyyyy");
            var recoveryFromDate = recoveryFDate.ToDateTime("ddMMyyyy");

            var listContact = ContactRepository.FilterRecovered(sourceTypes, levels, statusMapId, statusCareId, branchId, recoveryFromDate, recoveryToDate, page, rows, out totalRecords) ?? new List<ContactInfo>();
            var lstData = new List<ContactRecoveryInfo>();
            foreach (var info in listContact)
            {
                var objModel = CreateContactRecoveryInfo(info, EmployeeType.ManagerContact);
                lstData.Add(objModel);
            }

            var list = new ContactRecoveryModel
            {
                Rows = lstData,
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            return list;
        }

        [HttpGet]
        public ContactListModel FilterSearch(string mobile, string email, int branchId, int page, int rows)
        {
            if (string.IsNullOrEmpty(email) &&
                string.IsNullOrEmpty(mobile))
                return null;

            if (string.IsNullOrEmpty(email)) email = string.Empty;
            if (string.IsNullOrEmpty(mobile)) mobile = string.Empty;
            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.FilterSearch(mobile, email, branchId, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            return list;
        }

        [HttpGet]
        public ContactListModel FilterDuplicateTvts(string statusIds, int branchId, int page, int rows)
        {
            var userId = UserContext.GetCurrentUser().UserID;
            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.FilterDuplicateTvts(userId, statusIds, branchId, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            return list;
        }

        private static ContactDeletedInfo InitContactDeletedInfo(int id)
        {
            var phones = PhoneRepository.GetByContact(id) ?? new List<PhoneInfo>();
            var phoneInfo = phones.Where(c => c.IsPrimary == 1).FirstOrDefault();
            var entityJson = new ContactDeletedJson
            {
                CallHistories = CallHistoryRepository.GetAllByContactId(id),
                ContactInfo = ContactRepository.GetInfo(id) ?? new ContactInfo(),
                ContactLevelInfo = ContactLevelInfoRepository.GetInfoByContactId(id),
            };
            var entity = new ContactDeletedInfo
            {
                DeletedTime = DateTime.Now,
                Json = entityJson.ToJson(),
                Email = entityJson.ContactInfo.Email,
                ContactId = entityJson.ContactInfo.Id,
                UserId = entityJson.ContactInfo.UserConsultantId,
                BranchId = entityJson.ContactInfo.BranchId,
                DeletedBy = UserContext.GetCurrentUser().UserID,
                Phone = phoneInfo != null ? phoneInfo.PhoneNumber : string.Empty,
            };
            return entity;
        }



        private static ContactRecoveryInfo CreateContactRecoveryInfo(ContactInfo info, EmployeeType employeeType)
        {
            var objModel = new ContactRecoveryInfo
            {
                Id = info.Id,
                Email = info.Email,
                Fullname = info.Fullname,
                LocationName = info.Address,
                Level = Enum.GetName(typeof(LevelType), info.LevelId),
                SourceTypeName = Enum.GetName(typeof(SourceType), info.TypeId),
                RegisteredDate = info.RegisteredDate != null ? info.RegisteredDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                UserName = info.UserName,
                StatusCare = info.StatusCare,
                StatusMap = info.StatusMap,
            };

            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        objModel.HandoverDate = info.HandoverCollaboratorDate != null
                            ? info.HandoverCollaboratorDate.Value.ToString("dd/MM/yyyy")
                            : string.Empty;

                        // StatusCare
                        var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == info.StatusCareCollaboratorId);
                        if (statusCare != null) objModel.StatusCare = statusCare.Name;

                        // StatusMap
                        var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == info.StatusMapCollaboratorId) ?? StatusMapRepository.GetInfo(info.StatusMapCollaboratorId);
                        if (statusMap != null) objModel.StatusMap = statusMap.Name;

                        // Notes
                        if (info.CallInfoCollaborator.IsStringNullOrEmpty())
                        {
                            var callHistories = CallHistoryRepository.GetAllByContactId(info.Id);
                            if (!callHistories.IsNullOrEmpty()) if (!callHistories.IsNullOrEmpty())
                                {
                                    var callHistory = callHistories.LastOrDefault(c => c.UserLogType == (int)employeeType) ?? new CallHistoryInfo();
                                    objModel.Notes = callHistory.CallCenterInfo;
                                }
                        }
                        else objModel.Notes = info.CallInfoCollaborator;

                        // Collaborator
                        var consultant = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserCollaboratorId) ??
                                         UserRepository.GetInfo(info.UserCollaboratorId);
                        if (consultant != null) objModel.UserName = consultant.FullName;
                    }
                    break;
                case EmployeeType.Consultant:
                    {
                        objModel.HandoverDate = info.HandoverConsultantDate != null
                            ? info.HandoverConsultantDate.Value.ToString("dd/MM/yyyy")
                            : string.Empty;

                        // StatusCare
                        var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == info.StatusCareConsultantId);
                        if (statusCare != null) objModel.StatusCare = statusCare.Name;

                        // StatusMap
                        var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == info.StatusMapConsultantId) ?? StatusMapRepository.GetInfo(info.StatusMapConsultantId);
                        if (statusMap != null) objModel.StatusMap = statusMap.Name;

                        // Notes
                        if (info.CallInfoConsultant.IsStringNullOrEmpty())
                        {
                            var callHistories = CallHistoryRepository.GetAllByContactId(info.Id);
                            if (!callHistories.IsNullOrEmpty())
                            {
                                var callHistory = callHistories.LastOrDefault(c => c.UserLogType == (int)employeeType) ?? new CallHistoryInfo();
                                objModel.Notes = callHistory.CallCenterInfo;
                            }
                        }
                        else objModel.Notes = info.CallInfoConsultant;

                        // Consultant
                        var consultant = StoreData.ListUser.FirstOrDefault(c => c.UserID == info.UserConsultantId) ??
                                         UserRepository.GetInfo(info.UserConsultantId);
                        if (consultant != null) objModel.UserName = consultant.FullName;
                    }
                    break;
            }

            // Phone
            var phones = PhoneRepository.GetByContact(info.Id);
            if (phones != null)
            {
                var phoneEntity = phones.FirstOrDefault(phone => phone.IsPrimary > 0) ?? phones.FirstOrDefault();
                if (phoneEntity != null) objModel.Mobile = phoneEntity.PhoneNumber;
            }

            // Channel
            if (objModel.SourceTypeName == Enum.GetName(typeof(SourceType), SourceType.MO))
            {
                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId) ??
                              ChannelRepository.GetInfo(info.ChannelId);
                if (channel != null) objModel.ChannelName = channel.Name;
            }
            else if (objModel.SourceTypeName == Enum.GetName(typeof(SourceType), SourceType.CC))
            {
            }
            else
            {
                if (objModel.ChannelName.IsStringNullOrEmpty())
                {
                    var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId) ??
                                  ChannelRepository.GetInfo(info.ChannelId);
                    if (channel != null) objModel.ChannelName = channel.Name;
                }
            }
            return objModel;
        }

        [HttpPost]
        public int HandoverToDistributor(FormDataCollection form)
        {
            var total = form.Get("Total").ToInt32();
            var typeCc = form.Get("typeCc").ToInt32();
            var statusMapIds = form.Get("statusMapIds");
            var branchId = UserContext.GetDefaultBranch();
            var schoolIds = form.Get("schoolIds").Trim(',');
            var channelIds = form.Get("channelIds").Trim(',');
            const int statusId = ((int)StatusType.New);
            var sourceTypeId = form.Get("SourceTypeId").ToInt32();
            var fromDate = form.Get("from").ToDateTime() ?? DateTime.Now.Date;
            var toDate = form.Get("to").ToDateTime() ?? DateTime.Now.Date; toDate = toDate.AddDays(1).AddSeconds(-1);

            var createdDate = DateTime.Now;
            const int statusMapDistributorId = 0;
            var handoverDistributorDate = createdDate;
            var userId = UserContext.GetCurrentUser().UserID;
            var createdBy = UserContext.GetCurrentUser().UserID;
            const int statusIdNext = (int)StatusType.HandoverCollaborator;
            return ContactRepository.ContactHandoverDistributor(branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds, channelIds, schoolIds, total, userId, statusIdNext, statusMapDistributorId, handoverDistributorDate, createdBy, createdDate);
        }

        [HttpGet]
        public ContactListModel FilterForCollaborator(int importId, int channelId, int schoolId, string startImportDate, string endImportDate, string startRegisterDate, string endRegisterDate, int connectStatus, int todayType, string statusMapIds, int campainId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var endImport = endImportDate.ToDateTime("ddMMyyyy");
            var startImport = startImportDate.ToDateTime("ddMMyyyy");
            var endRegister = endRegisterDate.ToDateTime("ddMMyyyy");
            var startRegister = startRegisterDate.ToDateTime("ddMMyyyy");
            if (statusMapIds.IsStringNullOrEmpty()) statusMapIds = string.Empty;
            statusMapIds = statusMapIds.Trim('|').Replace('|', ',');

            try
            {

                if (startImport.HasValue)
                {
                    startImport = startImport.Value.Date;
                    if (!endImport.HasValue) endImport = DateTime.Now;
                }
                if (endImport.HasValue) endImport = endImport.Value.Date.AddDays(1).AddSeconds(-1);

                if (startRegister.HasValue)
                {
                    startRegister = startRegister.Value.Date;
                    if (!endRegister.HasValue) endRegister = DateTime.Now;
                }
                if (endRegister.HasValue) endRegister = endRegister.Value.Date.AddDays(1).AddSeconds(-1);

                int totalRecords;
                var listContacts = ContactRepository.FilterForCollaborator(branchId, importId, channelId, schoolId, startImport, endImport, startRegister, endRegister, connectStatus, todayType, statusMapIds, campainId, page, rows, out totalRecords);
                var list = new ContactListModel
                {
                    Rows = listContacts,
                    Total = (listContacts.Count / rows) + 1,
                    UserData = totalRecords,
                    Records = rows,
                    Page = page,
                };
                return list;
            }
            catch
            {
                return new ContactListModel();
            }
        }

        [HttpGet]
        public ContactListModel FilterForCollaboratorL1(int importId, int channelId, int schoolId, string startImportDate, string endImportDate, string startRegisterDate, string endRegisterDate, int connectStatus, int todayType, string statusMapIds, int campainId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var endImport = endImportDate.ToDateTime("ddMMyyyy");
            var startImport = startImportDate.ToDateTime("ddMMyyyy");
            var endRegister = endRegisterDate.ToDateTime("ddMMyyyy");
            var startRegister = startRegisterDate.ToDateTime("ddMMyyyy");
            if (statusMapIds.IsStringNullOrEmpty()) statusMapIds = string.Empty;
            statusMapIds = statusMapIds.Trim('|').Replace('|', ',');

            try
            {

                if (startImport.HasValue)
                {
                    startImport = startImport.Value.Date;
                    if (!endImport.HasValue) endImport = DateTime.Now;
                }
                if (endImport.HasValue) endImport = endImport.Value.Date.AddDays(1).AddSeconds(-1);

                if (startRegister.HasValue)
                {
                    startRegister = startRegister.Value.Date;
                    if (!endRegister.HasValue) endRegister = DateTime.Now;
                }
                if (endRegister.HasValue) endRegister = endRegister.Value.Date.AddDays(1).AddSeconds(-1);

                int totalRecords;
                var listContacts = ContactRepository.FilterForCollaboratorL1(branchId, importId, channelId, schoolId, startImport, endImport, startRegister, endRegister, connectStatus, todayType, statusMapIds, campainId, page, rows, out totalRecords);
                var list = new ContactListModel
                {
                    Rows = listContacts,
                    Total = (listContacts.Count / rows) + 1,
                    UserData = totalRecords,
                    Records = rows,
                    Page = page,
                };
                return list;
            }
            catch
            {
                return new ContactListModel();
            }
        }

        [HttpPost]
        public int HandoverToCampain(FormDataCollection form)
        {
            var createdDate = DateTime.Now;
            var total = form.Get("total").ToInt32();
            var statusMapIds = form.Get("statusMapIds");
            var branchId = UserContext.GetDefaultBranch();
            var importId = form.Get("importId").ToInt32();
            var schoolId = form.Get("schoolId").ToInt32();
            var channelId = form.Get("channelId").ToInt32();
            var campainId = form.Get("campainId").ToInt32();
            var todayType = form.Get("todayType").ToInt32();
            var createdBy = UserContext.GetCurrentUser().UserID;
            var connectStatus = form.Get("connectStatus").ToInt32();
            var endImport = form.Get("endImportDate").ToDateTime("ddMMyyyy");
            var startImport = form.Get("startImportDate").ToDateTime("ddMMyyyy");
            var endRegister = form.Get("endRegisterDate").ToDateTime("ddMMyyyy");
            var startRegister = form.Get("startRegisterDate").ToDateTime("ddMMyyyy");

            // statusMapIds
            if (statusMapIds.IsStringNullOrEmpty()) statusMapIds = string.Empty;
            statusMapIds = statusMapIds.Trim('|').Replace('|', ',');

            // ImportDate
            if (startImport.HasValue)
            {
                startImport = startImport.Value.Date;
                if (!endImport.HasValue) endImport = DateTime.Now;
            }
            if (endImport.HasValue) endImport = endImport.Value.Date.AddDays(1).AddSeconds(-1);

            // RegisterDate
            if (startRegister.HasValue)
            {
                startRegister = startRegister.Value.Date;
                if (!endRegister.HasValue) endRegister = DateTime.Now;
            }
            if (endRegister.HasValue) endRegister = endRegister.Value.Date.AddDays(1).AddSeconds(-1);

            try
            {
                return ContactRepository.ContactHandoverToCampain(total, campainId, branchId, importId, channelId, schoolId, startImport, endImport, startRegister, endRegister, connectStatus, todayType, statusMapIds, createdBy, createdDate);
            }
            catch
            {
                return 0;
            }
        }

        [HttpGet]
        public MyContactListModel FilterCCL2Plus(string levelIds, int page, int rows)
        {
            if (levelIds.IsStringNullOrEmpty()) return new MyContactListModel();

            int totalRecords;
            var lstModel = new List<ContactModel>();
            const int sourceTypeId = (int)SourceType.CC;
            var branchId = UserContext.GetDefaultBranch();
            var data = ContactRepository.FilterCCL2Plus(branchId, levelIds, sourceTypeId, page, rows, out totalRecords);
            foreach (var info in data)
            {
                var objModel = ObjectHelper.Transform<ContactModel, ContactInfo>(info);
                objModel.StatusName = ObjectExtensions.GetEnumDescription((StatusType)objModel.StatusId);

                var contactLevel = ContactLevelInfoRepository.GetInfo(info.Id);
                if (contactLevel != null)
                {
                    objModel.Notes = contactLevel.EducationSchoolName;
                }
                var collector = CollectorRepository.GetInfo(info.CollectorId);
                if (collector != null) objModel.CollectorName = collector.Name;

                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == info.ChannelId);
                if (channel != null) objModel.ChannelName = channel.Name;

                var phones = PhoneRepository.GetByContact(info.Id);
                foreach (var phone in phones)
                {
                    if (phone.IsPrimary == 1) objModel.Mobile = phone.PhoneNumber;
                    else if (phone.PhoneType == PhoneType.HomePhone.ToString()) objModel.Tel = phone.PhoneNumber;
                    else objModel.Mobile2 = phone.PhoneNumber;

                }
                lstModel.Add(objModel);
            }
            return new MyContactListModel
            {
                Page = page,
                Records = rows,
                Rows = lstModel,
                UserData = totalRecords,
                Total = (totalRecords / rows) + 1
            };
        }

        [HttpGet]
        public ContactRecoveryModel FilterRecoveryDistributor(int statusMapId, string fromDate, string toDate, int page, int rows)
        {

            return null;
        }

        [HttpPost]
        public int RecoveryDistributor(FormDataCollection form)
        {
            // Params
            var ids = form.Get("ids");
            var recoveryDistributorDate = DateTime.Now;
            const int statusId = (int)StatusType.New;
            var createdBy = UserContext.GetCurrentUser().UserID;

            // Recovery
            return ContactRepository.ContactRecoveryDistributor(ids, statusId, recoveryDistributorDate, createdBy);
        }

        [HttpPost]
        public int RecoveryDistributorAll(FormDataCollection form)
        {
            // Params
            var statusMapIds = form.Get("statusMapId");
            if (statusMapIds == "0")
            {
                var statusMaps = StoreData.ListStatusMap
                    .Where(c => c.StatusMapType == (int)EmployeeType.Collaborator)
                    .Where(c => c.LevelIdNext <= (int)LevelType.L1)
                    .Select(c => c.Id)
                    .ToList();
                if (!statusMaps.IsNullOrEmpty())
                    statusMapIds = "0," + string.Join(",", statusMaps);
            }
            const int levelId = (int)LevelType.L1;
            const int sourceTypeId = (int)SourceType.CC;
            var branchId = UserContext.GetDefaultBranch();
            var statusIds = string.Join(",", new[]
                                             {
                                                 (int) StatusType.HandoverCollaborator,
                                             });
            var fromDateTime = form.Get("fromDate").ToDateTime();
            var toDateTime = form.Get("toDate").ToDateTime();
            if (toDateTime.HasValue)
                toDateTime = toDateTime.Value.AddDays(1).AddSeconds(-1);

            var recoveryDistributorDate = DateTime.Now;
            const int statusId = (int)StatusType.New;
            var createdBy = UserContext.GetCurrentUser().UserID;

            // Recovery
            return ContactRepository.ContactRecoveryDistributorAll(levelId, sourceTypeId, statusIds, statusMapIds, fromDateTime, toDateTime, branchId, statusId, recoveryDistributorDate, createdBy);
        }

        [HttpPost]
        public int ForwardDistributor(FormDataCollection form)
        {
            var ids = form.Get("ids");
            var branchId = form.Get("branchId").ToInt32();
            var createdBy = UserRepository.GetCurrentUserInfo().UserID;

            // Params
            return ContactRepository.ContactForwardDistributor(ids, branchId, createdBy);
        }

        [HttpPost]
        public int ForwardAllDistributor(FormDataCollection form)
        {
            var userId = form.Get("userId").ToInt32();
            var groupId = form.Get("groupId").ToInt32();
            var levelId = form.Get("levelId").ToInt32();
            var branchId = form.Get("branchId").ToInt32();
            var statusMapId = form.Get("statusMapId").ToInt32();
            var statusCareId = form.Get("statusCareId").ToInt32();
            var targetBranchId = form.Get("targetBranchId").ToInt32();
            var fromDateTime = form.Get("fromDate").ToDateTime() ?? DateTime.Now.Date;
            var statusIds = (int)StatusType.HandoverConsultant + "," + (int)StatusType.RecoveryConsultant;
            var toDateTime = form.Get("toDate").ToDateTime() ?? DateTime.Now.Date; toDateTime = toDateTime.AddDays(1).AddSeconds(-1);
            var userIds = string.Empty;
            if (userId > 0) userIds = userId.ToString();
            else if (groupId > 0)
            {
                var listUser = StoreData.ListUser.Where(c => c.GroupId == groupId).ToList();
                if (!listUser.IsNullOrEmpty())
                    userIds = string.Join(",", listUser.Select(c => c.UserID));
            }
            else
            {
                var groupIds = StoreData.ListGroup.Where(c => c.BranchId == branchId).ToList();
                if (!groupIds.IsNullOrEmpty())
                {
                    var listUser = new List<UserInfo>();
                    foreach (var groupInfo in groupIds)
                    {
                        var users = StoreData.ListUser.Where(c => c.GroupId == groupInfo.GroupId).ToList();
                        if (users.IsNullOrEmpty()) continue;
                        listUser.AddRange(users);
                    }
                    if (!listUser.IsNullOrEmpty())
                        userIds = string.Join(",", listUser.Select(c => c.UserID));
                }
            }
            var createdBy = UserRepository.GetCurrentUserInfo().UserID;

            // Params
            return ContactRepository.ContactForwardAllDistributor(levelId, statusIds, statusMapId, statusCareId, branchId, userIds, fromDateTime, toDateTime, targetBranchId, createdBy);
        }

        #region HandoverFastManagerContact 
        [HttpPost]
        public int HandoverContactFast(FormDataCollection form)
        {
            var ids = form.Get("ids");
            var targetUserId = form.Get("targetUserId").ToInt32();
            var targetBranchId = form.Get("targetBranchId").ToInt32();
            var createdBy = UserRepository.GetCurrentUserInfo().UserID;
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();
            var handoverDate = form.Get("handoverDate").ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;

            // Params
            ContactRepository.ContactHandoverFast(ids, employeeType, targetUserId, targetBranchId, handoverDate, createdBy);
            return ids.Split(',').Length;
        }

        #endregion

        #region Handover
        [HttpPost]
        public int Handover(FormDataCollection form)
        {
            var branchId = UserContext.GetDefaultBranch();
            var productSellId = form.Get("productSellId").ToInt32();
            var createdBy = UserRepository.GetCurrentUserInfo().UserID;
            var datetime = form.Get("datetime").ToDateTime() ?? DateTime.Now;
            var employeeTypeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();

            // Params
            const int statusMapId = 1;
            const int statusCareId = 1;
            var appointmentDate = datetime;
            var handoverDate = datetime.Date;
            const int levelId = (int)LevelType.L1;

            var statusId = 0;
            switch (employeeTypeType)
            {
                case EmployeeType.Collaborator:
                    statusId = (int)StatusType.HandoverCollaborator;
                    break;
                case EmployeeType.Consultant:
                    statusId = (int)StatusType.HandoverConsultant;
                    break;
            }

            // Handover
            var count = ContactRepository.ContactUpdateHandover(branchId, levelId, statusId, statusMapId, statusCareId, productSellId, handoverDate, appointmentDate, createdBy, employeeTypeType);

            // Update Account Draft
            var account = new UserDraftInfo
            {
                IsDraftConsultant = false,
                IsDraftCollabortor = false,
                CreatedDate = DateTime.Now,
                BranchId = UserContext.GetDefaultBranch(),
                UserId = UserContext.GetCurrentUser().UserID,
            };
            UserDraftRepository.Update(account);

            // Return
            return count;
        }

        [HttpPost]
        public string SaveDraft(FormDataCollection form)
        {
            int userImportId = UserContext.GetCurrentUser().UserID;
            // Check account user form
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();
            var status = StoreData.CheckAccountUseHandover(employeeType);
            if (!status.IsStringNullOrEmpty()) return status;

            var typeIds = form.Get("typeIds");
            var levelIds = form.Get("levelIds");
            var importIds = form.Get("importIds");
            var statusIds = form.Get("statusIds");
            var channelIds = form.Get("channelIds");
            var containerIds = form.Get("containerIds");
            var branchId = UserContext.GetDefaultBranch();
            var channelAmounts = form.Get("channelAmounts");
            var statusMapId = form.Get("statusMapId").ToInt32();
            var statusCareId = form.Get("statusCareId").ToInt32();

            try
            {
                // var listContact = ContactRepository.FilterHandover(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId) ?? new List<ContactInfo>();

                var arrUser = ConvertHelper.ToString(form.Get("users")).Split(',');
                if (arrUser.Length <= 0) return "Không có TVTS để lưu nháp";
                var arrAmount = ConvertHelper.ToString(form.Get("amounts")).Split(',');
                if (arrAmount.Length <= 0) return "Không có Contact để lưu nháp";
                if (arrUser.Length != arrAmount.Length) return "TVTS và số lượng Contact không phù hợp";


                int tongChiTieu = 0;
                for (int z = 0; z < arrAmount.Length; z++)
                {
                    tongChiTieu += Int32.Parse(arrAmount[z]);
                }
                // 29/05/2020
                var listContact = ContactRepository.FilterHandover(tongChiTieu, branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId) ?? new List<ContactInfo>();

                var index = 0;
                int i = 0;
                int j = 0;

                string[] arrayCts = new string[arrUser.Length];   //Mảng chuỗi lưu các Id contacts các user             
                int[] dem = new int[arrUser.Length]; //số lượng contacts cho mỗi user
                for (int h = 0; h < arrUser.Length; h++)
                {
                    dem[h] = 0; //gán tất cả biến đếm số contatc từng user là 0
                }

                int vonglap = tongChiTieu;

                //if (tongChiTieu < listContact.Count)
                //{
                //    vonglap = tongChiTieu;
                //}
                //else {
                //    vonglap = listContact.Count;
                //}

                while (i < vonglap)
                {
                    while (j < arrUser.Length)
                    {
                        var amount = ConvertHelper.ToInt32(arrAmount[j]);
                        if (dem[j] == amount) //Nếu đã phân đủ chỉ tiêu
                        {
                            j++;
                            if (j >= arrUser.Length)
                            {
                                j = 0;
                            }
                            continue;
                        }
                        if (amount == 0)
                        {
                            j++;
                            if (j >= arrUser.Length)
                            {
                                j = 0;
                            }
                            break;
                        }

                        var id = listContact
                            .Skip(index)
                            .Take(1)
                            .Select(c => c.Id.ToString())
                            .Aggregate((totalItem, current) => totalItem + ", " + current);

                        if (dem[j] == 0)
                        {
                            arrayCts[j] += id;
                        }
                        else
                        {
                            arrayCts[j] += "," + id;
                        }
                        dem[j] += 1;
                        i++;
                        j++;
                        if (j >= arrUser.Length)
                        {
                            j = 0;
                        }
                        break;
                    }
                    index++;
                }

                for (var k = 0; k < arrUser.Length; k++)
                {
                    var user = ConvertHelper.ToInt32(arrUser[k]);
                    ContactRepository.ContactUpdateDraft(arrayCts[k], user, 1, employeeType);
                }

                // Update Account Draft
                var account = new UserDraftInfo
                {
                    CreatedDate = DateTime.Now,
                    BranchId = UserContext.GetDefaultBranch(),
                    UserId = UserContext.GetCurrentUser().UserID,
                    IsDraftConsultant = employeeType == EmployeeType.Consultant,
                    IsDraftCollabortor = employeeType == EmployeeType.Collaborator,
                };
                UserDraftRepository.Update(account);
                return "Lưu nháp thành công";
            }
            catch (Exception ex)
            {
                //return ex.Message;
                return "Lưu nháp không thành công" + ex.Message;
            }
        }

        [HttpPost]
        public string ClearDraft(FormDataCollection form)
        {
            try
            {
                var employeeTypeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();
                ContactRepository.ContactUpdateClearDraft(employeeTypeType);

                // Update Account Draft
                var account = new UserDraftInfo
                {
                    IsDraftConsultant = false,
                    IsDraftCollabortor = false,
                    CreatedDate = DateTime.Now,
                    BranchId = UserContext.GetDefaultBranch(),
                    UserId = UserContext.GetCurrentUser().UserID,
                };
                UserDraftRepository.Update(account);

                // Return
                return "Xóa draft thành công";
            }
            catch
            {
                return "Xóa draft không thành công";
            }
        }

        [HttpGet]
        public int FilterCountHandover(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds)
        {
            var branchId = UserContext.GetDefaultBranch();
            return ContactRepository.FilterCountHandover(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds);
        }

        [HttpGet]
        public ContactListModel FilterHandover(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, int employeeTypeId, int statusCareId, int statusMapId)
        {
            var branchId = UserContext.GetDefaultBranch();
            var userImportId = UserContext.GetCurrentUser().UserID;
            var employeeType = (EmployeeType)employeeTypeId;
            var listContact = ContactRepository.FilterHandover(9999999, branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId) ?? new List<ContactInfo>();

            var list = new ContactListModel
            {
                Rows = listContact,
            };
            return list;
        }

        
        [HttpGet]
        public ContactListModel FilterHandoverForMOL(string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, int employeeTypeId, int statusCareId, int statusMapId, string fromRegisterDate, string toRegisterDate)
        {
            var branchId = UserContext.GetDefaultBranch();
            var userImportId = UserContext.GetCurrentUser().UserID;
            var employeeType = (EmployeeType)employeeTypeId;
            var fromRegisterDateTime = fromRegisterDate.ToDateTime("ddMMyyyy");
            var toRegisterDateTime = toRegisterDate.ToDateTime("ddMMyyyy");

            var listContact = ContactRepository.FilterHandoverForMOL(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId, fromRegisterDateTime, toRegisterDateTime) ?? new List<ContactInfo>();

            var list = new ContactListModel
            {
                Rows = listContact,
            };
            return list;
        }
        #endregion

        #region Recovery
        [HttpPost]
        public int RecoveryAll(FormDataCollection form)
        {
            var userIds = form.Get("userIds");
            var levelIds = form.Get("levelIds");
            var day = form.Get("day").ToInt32();
            var branchId = UserContext.GetDefaultBranch();
            var statusMapId = form.Get("statusMapId").ToInt32();
            var statusCareId = form.Get("statusCareId").ToInt32();
            var todayType = (TodayType)form.Get("todayType").ToInt32();
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();
            var sourceType = form.Get("sourceType").ToInt32();

            var callToDate = form.Get("callToDate").ToDateTime("ddMMyyyy");
            var callFromDate = form.Get("callFromDate").ToDateTime("ddMMyyyy");
            if (!callToDate.HasValue) callToDate = callFromDate;
            if (callToDate.HasValue) callToDate = callToDate.Value.AddDays(1).AddSeconds(-1);

            var handoverToDate = form.Get("handoverToDate").ToDateTime("ddMMyyyy");
            var handoverFromDate = form.Get("handoverFromDate").ToDateTime("ddMMyyyy");
            if (!handoverToDate.HasValue) handoverToDate = handoverFromDate;
            if (handoverToDate.HasValue) handoverToDate = handoverToDate.Value.AddDays(1).AddSeconds(-1);

            // Params
            const int userId = 0;
            var recoveryDate = DateTime.Now;
            var createdBy = UserContext.GetCurrentUser().UserID;

            return ContactRepository.ContactRecoveryAll(userIds, levelIds, statusMapId, statusCareId, day, branchId, sourceType, todayType, employeeType, userId, recoveryDate, createdBy, callToDate, callFromDate, handoverToDate, handoverFromDate);
        }

        [HttpPost]
        public int Recovery(FormDataCollection form)
        {
            // Params
            const int userId = 0;
            var ids = form.Get("ids");
            var recoveryDate = DateTime.Now;
            var createdBy = UserContext.GetCurrentUser().UserID;
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();

            // Recovery
            ContactRepository.ContactRecovery(ids, userId, recoveryDate, employeeType, createdBy);
            return ids.Split(',').Length;
        }

        [HttpPost]
        public int RecoveryContainerDuplicate(FormDataCollection form)
        {
            // Params
            const int userId = 0;
            var ids = form.Get("ids");
            var recoveryDate = DateTime.Now;
            var createdBy = UserContext.GetCurrentUser().UserID;
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();

            // Recovery
            ContactRepository.ContactRecoveryContainerDuplicate(ids, userId, (int)StatusType.ContainerMOL, recoveryDate, employeeType, createdBy);
            return ids.Split(',').Length;
        }

        [HttpPost]
        public int RecoveryContainerDuplicate1_6(FormDataCollection form)
        {
            // Params
            const int userId = 0;
            var ids = form.Get("ids");
            var recoveryDate = DateTime.Now;
            var createdBy = UserContext.GetCurrentUser().UserID;
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();

            // Recovery
            ContactRepository.ContactRecoveryContainerDuplicate(ids, userId, (int)StatusType.Container_Recovery_1_6, recoveryDate, employeeType, createdBy);
            return ids.Split(',').Length;
        }

        [HttpGet]
        public ContactRecoveryModel FilterRecovery(string userIds, string levelIds, int statusMapId, int statusCareId, int todayType, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int sourceType, int employeeTypeId, int page, int rows)
        {
            int totalRecords;
            var branchId = UserContext.GetDefaultBranch();

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            var listContact = ContactRepository.FilterRecovery(userIds, levelIds, statusMapId, statusCareId, branchId, (TodayType)todayType, handoverFDate, handoverTDate, callFDate, callTDate, sourceType, (EmployeeType)employeeTypeId, page, rows, out totalRecords) ?? new List<ContactInfo>();

            var lstData = new List<ContactRecoveryInfo>();
            foreach (var info in listContact)
            {
                var objModel = CreateContactRecoveryInfo(info, (EmployeeType)employeeTypeId);
                lstData.Add(objModel);
            }
            var list = new ContactRecoveryModel
            {
                Page = page,
                Records = rows,
                Rows = lstData,
                UserData = totalRecords,
                Total = (totalRecords / rows) + 1,
            };
            return list;
        }
        #endregion

        #region Change
        [HttpPost]
        public int ChangeContainer(FormDataCollection form)
        {
            var typeIds = form.Get("typeIds");
            var levelIds = form.Get("levelIds");
            var importIds = form.Get("importIds");
            var statusIds = form.Get("statusIds");
            var channelIds = form.Get("channelIds");
            var containerIds = form.Get("containerIds");
            var branchId = UserContext.GetDefaultBranch();
            var channelAmounts = form.Get("channelAmounts");
            var createdBy = UserContext.GetCurrentUser().UserID;
            var productSellId = form.Get("productSellId").ToInt32();
            var containerTargerId = form.Get("containerTargerId").ToInt32();
            return ContactRepository.UpdateChangeContainer(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, containerTargerId, productSellId, createdBy);
        }

        [HttpPost]
        public int ChangeStatusContact(FormDataCollection form)
        {
            var typeIds = form.Get("typeIds");
            var levelIds = form.Get("levelIds");
            var importIds = form.Get("importIds");
            int statusId = Convert.ToInt32(form.Get("statusId").ToString());
            int statusIdMOL = Convert.ToInt32(form.Get("statusIdMOL").ToString());
            var channelIds = form.Get("channelIds");

            var branchId = UserContext.GetDefaultBranch();
            var channelAmounts = form.Get("channelAmounts");
            var createdBy = UserContext.GetCurrentUser().UserID;
            DateTime? fromRegisterDate = form.Get("FromRegisterDate").ToDateTime("ddMMyyyy") ?? null;
            DateTime? toRegisterDate = form.Get("ToRegisterDate").ToDateTime("ddMMyyyy") ?? null;

            var containerTargerId = form.Get("containerTargerId").ToInt32();
            return ContactRepository.UpdateChangeStatusContact(branchId, typeIds, levelIds, importIds, statusId, statusIdMOL, channelIds, channelAmounts, createdBy, fromRegisterDate, toRegisterDate);
        }

        [HttpPost]
        public int Forward(FormDataCollection form)
        {
            var ids = form.Get("ids");
            var targetUserId = form.Get("targetUserId").ToInt32();
            var targetBranchId = form.Get("targetBranchId").ToInt32();
            var createdBy = UserRepository.GetCurrentUserInfo().UserID;
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();
            var handoverDate = form.Get("handoverDate").ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;

            // Params
            ContactRepository.ContactForward(ids, employeeType, targetUserId, targetBranchId, handoverDate, createdBy);
            return ids.Split(',').Length;
        }

        public int ForwardTVTSContainer(FormDataCollection form)
        {
            var ids = form.Get("ids");
            var statusId = form.Get("statusId").ToInt32();
            var employeeTypeId = (EmployeeType)form.Get("employeeTypeId").ToInt32();

            ContactRepository.ForwardTVTSContainer(ids, employeeTypeId, statusId);
            return ids.Split(',').Length;
        }

        [HttpPost]
        public int ForwardAll(FormDataCollection form)
        {
            var userIds = form.Get("userIds");
            var branchId = form.Get("branchId").ToInt32();
            var statusMapId = form.Get("statusMapId").ToInt32();
            var statusCareId = form.Get("statusCareId").ToInt32();
            var employeeType = (EmployeeType)form.Get("employeeTypeId").ToInt32();
            var fromDate = form.Get("fromDate").ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;
            var toDate = form.Get("toDate").ToDateTime("ddMMyyyy") ?? DateTime.Now.Date; toDate = toDate.AddDays(1).AddSeconds(-1);

            var targetUserId = form.Get("targetUserId").ToInt32();
            var targetBranchId = form.Get("targetBranchId").ToInt32();
            var createdBy = UserRepository.GetCurrentUserInfo().UserID;
            var handoverDate = form.Get("handoverDate").ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;

            // Params
            return ContactRepository.ContactForwardAll(branchId, statusMapId, statusCareId, userIds, fromDate, toDate, employeeType, targetUserId, targetBranchId, handoverDate, createdBy);
        }

        [HttpPost]
        public int ForwardAllContainer(FormDataCollection form)
        {
            DateTime importDate = (DateTime)form.Get("importDate").ToDateTime();
            var importId = form.Get("importId").ToInt32();
            var statusId = form.Get("statusId").ToInt32();
            EmployeeType employeeTypeId = (EmployeeType)form.Get("employeeTypeId").ToInt32();

            return ContactRepository.ContactForwardAllContainer(importDate, importId, statusId, employeeTypeId);
        }
        [HttpGet]
        public ContactListModel FilterContactForChange(int branchId, string levelids, int statusMapId, int statusCareId, string userIds, string fromDate, string toDate, int employeeTypeId, int page, int rows)
        {
            int totalRecords;
            var employeeType = (EmployeeType)employeeTypeId;
            var fDate = fromDate.ToDateTime("ddMMyyyy") ?? DateTime.Now.Date;
            var tDate = toDate.ToDateTime("ddMMyyyy") ?? DateTime.Now.Date; tDate = tDate.AddDays(1).AddSeconds(-1);
            var items = ContactRepository.FilterContactForChange(branchId, levelids, statusMapId, statusCareId, userIds, fDate, tDate, employeeType, page, rows, out totalRecords) ?? new List<ContactInfo>();

            var listContacts = new List<ContactInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            var list = new ContactListModel
            {
                Rows = listContacts,
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            return list;
        }
        #endregion

        #region Search
        [HttpGet]
        public void Delete(int id, int employeeTypeId)
        {
            var employeeType = (EmployeeType)employeeTypeId;

            var entity = InitContactDeletedInfo(id);
            StoreData.DeleteRedis(id);
            ContactRepository.Delete(id, entity, employeeType);
        }

        [HttpGet]
        public ContactListModel FilterSearchFast(string name, string mobile, string email, int employeeTypeId, int page, int rows)
        {
            #region "Start log BEGIN_TIM_KIEM_CONTACT_TVTS"
            int SessionLog = TmpJobReportRepository.GetSessionLog();
            DateTime TimeBegin = DateTime.Now;

            var logbegin = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "BEGIN_TIM_KIEM_CONTACT_TVTS",
                ContactId = 0,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            LogDashBoardRepository.CreateLogDashBoard(logbegin);
            #endregion 


            int totalRecords;
            int branchId = 0;
            branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            var user = UserContext.GetCurrentUser();
            var userId = user.UserID;
            if (!mobile.IsStringNullOrEmpty()) mobile = Util.CleanAlphabetAndFirstZero(mobile);
            List<ContactInfo> items = new List<ContactInfo>();
            switch (employeeType)
            {
                case EmployeeType.Consultant:
                    items = ContactRepository.FilterSearchFastForConsultant(branchId, userId, name, mobile, email, page, rows, out totalRecords) ?? new List<ContactInfo>();
                    break;
                case EmployeeType.Collaborator: 
                    items = ContactRepository.FilterSearchFastForCollaborator(branchId, userId, name, mobile, email, page, rows, out totalRecords) ?? new List<ContactInfo>();
                    break;
                default:
                    items = ContactRepository.FilterSearchFastForConsultant(branchId, userId, name, mobile, email, page, rows, out totalRecords) ?? new List<ContactInfo>();
                    break;
            }
            
            var listContacts = new List<ContactInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            var list = new ContactListModel
            {
                Rows = listContacts,
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };

            #region "Save log END_TIM_KIEM_CONTACT_TVTS"
            DateTime TimeEnd = DateTime.Now;
            var logend = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeEnd.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "END_TIM_KIEM_CONTACT_TVTS",
                ContactId = 0,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            LogDashBoardRepository.CreateLogDashBoard(logend);
            #endregion

            return list;
            
        }

        [HttpGet]
        public ContactListModel FilterSearchForManagerContact(string name, string mobile, string email, int employeeTypeId, int page, int rows)
        {
            int totalRecords;
            int branchId = 0;
            branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            var user = UserContext.GetCurrentUser();
            var userId = 0;
            if (!mobile.IsStringNullOrEmpty()) mobile = Util.CleanAlphabetAndFirstZero(mobile);

            var items = ContactRepository.FilterSearchFastForManagerConsultant(branchId, userId, name, mobile, email, employeeType, page, rows, out totalRecords) ?? new List<ContactInfo>();
            var listContacts = new List<ContactInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            var list = new ContactListModel
            {
                Rows = listContacts,
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            
            return list;
        }


        [HttpGet]
        public ContactListModel FilterSearchManager(string name, string mobile, string email, int employeeTypeId, int page, int rows)
        {
            int totalRecords;
            //var branchId = UserContext.GetDefaultBranch();
            int branchId = 0;  //Quan ly TVTS tim kiem tren toan he thong
            var employeeType = (EmployeeType)employeeTypeId;
            if (!mobile.IsStringNullOrEmpty()) mobile = Util.CleanAlphabetAndFirstZero(mobile);

            var items = ContactRepository.FilterSearchManager(branchId, name, mobile, email, employeeType, page, rows, out totalRecords) ?? new List<ContactInfo>();
            var listContacts = new List<ContactInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            var list = new ContactListModel
            {
                Rows = listContacts,
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };

            return list;
        }

        [HttpPost]
        public ContactListModel FilterSearchManager_copy(FormDataCollection form) //cai nay de dem so luong contact xuat la de tim kiem
        {
            int totalRecords;
            var mobile = ConvertHelper.ToString(form.Get("mobile"));
            var name = ConvertHelper.ToString(form.Get("name"));
            var email = ConvertHelper.ToString(form.Get("email"));
            var employeeTypeId = EmployeeType.Consultant;
            var rows = 30;
            var page = 1;

            //var branchId = UserContext.GetDefaultBranch();
            int branchId = 0;  //Quan ly TVTS tim kiem tren toan he thong
            //var employeeType = (EmployeeType)employeeTypeId;
            if (!mobile.IsStringNullOrEmpty()) mobile = Util.CleanAlphabetAndFirstZero(mobile);

            var items = ContactRepository.FilterSearchManager(branchId, name, mobile, email, employeeTypeId, page, rows, out totalRecords) ?? new List<ContactInfo>();
            var listContacts = new List<ContactInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            var list = new ContactListModel
            {
                Rows = listContacts,
                Total = (listContacts.Count / rows) + 1,
                UserData = listContacts.Count,
                Records = rows,
                Page = page,
            };

            return list;
        }

        //Ham dem so luong contact duoc tim thay, muc dich tra ve ket qua de thong bao co ton tai contact do hay ko. chuc nang cua tvts
        [HttpPost]
        public int FilterSearchCountConsulant(FormDataCollection form)
        {

            var mobile = ConvertHelper.ToString(form.Get("mobile"));
            var name = ConvertHelper.ToString(form.Get("name"));
            var email = ConvertHelper.ToString(form.Get("email"));
            var employeeTypeId = EmployeeType.Consultant;
            var userIds = UserContext.GetCurrentUser().UserID.ToString();

            var branchId = UserContext.GetDefaultBranch();

            if (!mobile.IsStringNullOrEmpty()) mobile = Util.CleanAlphabetAndFirstZero(mobile);

            int countResult = ContactRepository.FilterSearchCountConsultant(branchId, userIds, name, mobile, email, employeeTypeId);

            return countResult;
        }
        // ham tim kiem nhanh trong kho contact de phan luon cho TVTS
        [HttpGet]
        public ContactListModel FilterSearchHandoverContact(string name, string mobile, string email, int employeeTypeId, int page, int rows)
        {
            int totalRecord;
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            if (!mobile.IsStringNullOrEmpty()) mobile = Util.CleanAlphabetAndFirstZero(mobile);
            var items = ContactRepository.FilterSearchHandoverContact(branchId, name, mobile, email, employeeType, page, rows, out totalRecord);
            var listContacts = new List<ContactInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);
            var list = new ContactListModel
            {
                Rows = listContacts,
                Total = (listContacts.Count / rows) + 1,
                UserData = listContacts.Count,
                Records = rows,
                Page = page,
            };
            return list;
        }

        [HttpGet]
        public ContactListModel FilterSearchAdvance(int statusMapId, int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int qualityId, int productSellId, int employeeTypeId, int page, int rows)
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            var userIds = user.UserID.ToString();


            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.FilterSearchAdvance(branchId, userIds, statusMapId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, qualityId, productSellId, employeeType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            return list;
        }

        [HttpGet]
        public ContactListModel FilterSearchAdvanceManager(int statusMapId, int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int qualityId, int productSellId, string userIds, int employeeTypeId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            var callTDate = callToDate.ToDateTime("ddMMyyyy");
            var callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            var handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            var handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            int totalRecords;
            var list = new ContactListModel
            {
                Rows = ContactRepository.FilterSearchAdvanceManager(branchId, userIds, statusMapId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, qualityId, productSellId, employeeType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            return list;
        }
        #endregion

        #region Duplicate
        [HttpGet]
        public ContactDuplicateListModel FilterDuplicate(string statusIds, int employeeTypeId, string dateDuplicate, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            var userId = UserContext.GetCurrentUser().UserID;
            var dateDuplicateN = dateDuplicate.ToDateTime("ddMMyyyy");

            int totalRecords;
            var results = new List<ContactDuplicateModel>();
            var items = ContactRepository.FilterDuplicate(userId, statusIds, branchId, employeeType, dateDuplicateN, page, rows, out totalRecords) ?? new List<ContactInfo>();
            var listContacts = new List<ContactInfo>();
            foreach (var item in items)
                listContacts.Add(item);

            foreach (var item in listContacts)
            {
                var status = ObjectExtensions.GetEnumDescription((StatusType)item.StatusId);
                var level = StoreData.ListLevel.FirstOrDefault(c => c.LevelId == item.LevelId) ?? new LevelInfo();
                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == item.ChannelId) ?? new ChannelInfo();
                var type = StoreData.ListSourceType.FirstOrDefault(c => c.SourceTypeId == item.TypeId) ?? new SourceTypeInfo();

                string appointmentDate = string.Empty, appointmentAmpm = string.Empty, appointmentTime = string.Empty;
                string userName = string.Empty, statusMapName = string.Empty, statusCareName = string.Empty, callInfo = string.Empty;
                switch ((StatusType)item.StatusId)
                {
                    case StatusType.New:
                        {
                            userName = string.Empty;
                            var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapCollaboratorId) ?? new StatusMapInfo();
                            statusMapName = statusMap.Name;

                            var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == item.StatusCareCollaboratorId) ?? new StatusCareInfo();
                            statusCareName = statusCare.Name;
                        }
                        break;
                    case StatusType.HandoverCollaborator:
                    case StatusType.AcceptCollaborator:
                    case StatusType.RecoveryCollaborator:
                        {
                            var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.UserCollaboratorId) ?? new UserInfo();
                            userName = user.FullName;

                            var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapCollaboratorId) ?? new StatusMapInfo();
                            statusMapName = statusMap.Name;

                            var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == item.StatusCareCollaboratorId) ?? new StatusCareInfo();
                            statusCareName = statusCare.Name;

                            appointmentDate = item.AppointmentCollaboratorDate.HasValue ? item.AppointmentCollaboratorDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                            appointmentTime = item.AppointmentCollaboratorTime.HasValue ? item.AppointmentCollaboratorTime.Value.ToString("HH:mm") : string.Empty;
                            appointmentAmpm = item.AppointmentCollaboratorAmPm;
                            callInfo = item.CallInfoCollaborator;
                        }
                        break;
                    case StatusType.HandoverConsultant:
                    case StatusType.RecoveryConsultant:
                        {
                            var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.UserConsultantId) ?? new UserInfo();
                            userName = user.FullName;

                            var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapConsultantId) ?? new StatusMapInfo();
                            statusMapName = statusMap.Name;

                            var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == item.StatusCareConsultantId) ?? new StatusCareInfo();
                            statusCareName = statusCare.Name;

                            appointmentDate = item.AppointmentConsultantDate.HasValue ? item.AppointmentConsultantDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                            appointmentTime = item.AppointmentConsultantTime.HasValue ? item.AppointmentConsultantTime.Value.ToString("HH:mm") : string.Empty;
                            appointmentAmpm = item.AppointmentConsultantAmPm;
                            callInfo = item.CallInfoConsultant;
                        }
                        break;
                }
                results.Add(new ContactDuplicateModel
                {
                    Id = item.Id,
                    CallInfo = callInfo,
                    UserName = userName,
                    StatusName = status,
                    TypeName = type.Name,
                    Mobile = item.Mobile1,
                    LevelName = level.Name,
                    Fullname = item.Fullname,
                    ChannelName = channel.Name,
                    StatusMapName = statusMapName,
                    StatusCareName = statusCareName,
                    AppointmentDate = appointmentDate,
                    AppointmentAmPm = appointmentAmpm,
                    AppointmentTime = appointmentTime,
                    DuplicateInfo = item.DuplicateInfo,
                    Email = item.Email.IsStringNullOrEmpty() ? item.Email2 : item.Email,
                    ImportedDate = item.ImportedDate.HasValue
                                    ? item.ImportedDate.Value
                                    : item.CreatedDate.HasValue ? item.CreatedDate.Value : DateTime.Now,
                });
            }
            var list = new ContactDuplicateListModel
            {
                Page = page,
                Records = rows,
                Rows = results,
                UserData = totalRecords,
                Total = (totalRecords / rows) + 1,
            };
            return list;
        }
        #endregion

        #region Today
        [HttpGet]
        public ContactListModel FilterTodayOneMonth(int employeeTypeId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            var userIds = UserContext.GetCurrentUser().UserID.ToString();

            int totalRecords;
            var result = new ContactListModel
            {
                Rows = ContactRepository.FilterTodayAll(userIds, DateTime.Now.AddMonths(-1).AddDays(-1).Date, DateTime.Now.Date.AddSeconds(-1), String.Empty, branchId, employeeType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            if (result.Rows.Any())
            {
                foreach (var item in result.Rows.Where(c => c.AppointmentConsultantDate == DateTime.MinValue))
                {
                    item.AppointmentConsultantDate = item.HandoverConsultantDate;
                }
            }
            return result;
        }

        [HttpGet]
        public ContactListModel FilterTodayOneMonthManager(string userIds, int employeeTypeId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            int totalRecords;
            var result = new ContactListModel
            {
                Rows = ContactRepository.FilterTodayAll(userIds, DateTime.Now.AddMonths(-1).AddDays(-1).Date, DateTime.Now.Date.AddSeconds(-1), String.Empty, branchId, employeeType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            if (result.Rows.Any())
            {
                foreach (var item in result.Rows.Where(c => c.AppointmentConsultantDate == DateTime.MinValue))
                {
                    item.AppointmentConsultantDate = item.HandoverConsultantDate;
                }
            }
            return result;
        }

        [HttpGet]
        public ContactListModel FilterToday(string from, string to, int type, string levels, int employeeTypeId, int page, int rows)
        {
            var fromDate = string.IsNullOrEmpty(from)
                ? DateTime.Now.Date
                : DateTime.ParseExact(from, "ddMMyyyy", CultureInfo.InvariantCulture);
            var toDate = string.IsNullOrEmpty(to)
                ? fromDate.AddDays(1).AddSeconds(-1)
                : DateTime.ParseExact(to, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);

            if (string.IsNullOrEmpty(levels)) levels = string.Empty;
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            var userIds = UserContext.GetCurrentUser().UserID.ToString();

            int totalRecords;

            var result = new ContactListModel();
            switch (type)
            {
                case (int)TodayType.NewAndAppointment:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayNewAndAppointment(userIds, fromDate, toDate, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.Appointment:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAppointment(userIds, fromDate, toDate, levels, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.AllByLevels:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAppointment(userIds, fromDate, toDate, levels, branchId, (EmployeeType)6, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.AllByLevelsCollaborator:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAppointment(userIds, fromDate, toDate, levels, branchId, (EmployeeType)7, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.New:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayNew(userIds, fromDate, toDate, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.All:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAll(userIds, fromDate, toDate, levels, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
            }

            if (result.Rows.Any())
            {
                foreach (var item in result.Rows.Where(c => c.AppointmentConsultantDate == DateTime.MinValue))
                {
                    item.AppointmentConsultantDate = item.HandoverConsultantDate;
                }
            }
            return result;
        }

        [HttpGet]
        public ContactListModel FilterTodayManager(string from, string to, int type, string levels, string userIds, int employeeTypeId, int page, int rows)
        {
            var fromDate = string.IsNullOrEmpty(from)
                ? DateTime.Now.Date
                : DateTime.ParseExact(from, "ddMMyyyy", CultureInfo.InvariantCulture);
            var toDate = string.IsNullOrEmpty(to)
                ? fromDate.AddDays(1).AddSeconds(-1)
                : DateTime.ParseExact(to, "ddMMyyyy", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
            if (string.IsNullOrEmpty(levels)) levels = string.Empty;
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            int totalRecords;

            var result = new ContactListModel();
            switch (type)
            {
                case (int)TodayType.NewAndAppointment:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayNewAndAppointment(userIds, fromDate, toDate, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.Appointment:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAppointment(userIds, fromDate, toDate, levels, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.New:
                    {

                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayNew(userIds, fromDate, toDate, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
                case (int)TodayType.All:
                    {
                        result = new ContactListModel
                        {
                            Rows = ContactRepository.FilterTodayAll(userIds, fromDate, toDate, String.Empty, branchId, employeeType, page, rows, out totalRecords),
                            Total = (totalRecords / rows) + 1,
                            UserData = totalRecords,
                            Records = rows,
                            Page = page,
                        };
                    }
                    break;
            }

            if (result.Rows.Any())
            {
                foreach (var item in result.Rows.Where(c => c.AppointmentConsultantDate == DateTime.MinValue))
                {
                    item.AppointmentConsultantDate = item.HandoverConsultantDate;
                }
            }

            return result;
            //if (typeOutput == (int)ReportType.Excel)
            //{
            //    var localReport = new LocalReport { ReportPath = System.Web.HttpContext.Current.Server.MapPath("~/Areas/Admin/Reports/rptBC43.rdlc") };
            //    localReport.SetParameters(new ReportParameter("Infomation", "Thông tin contact"));
            //    var reportDataSource = new ReportDataSource("BC43", result.Rows);
            //    localReport.DataSources.Add(reportDataSource);

            //    string[] streams;
            //    Warning[] warnings;
            //    string mimeType, encoding, fileNameExtension;
            //    var renderedBytes = localReport.Render("excel", string.Empty, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //    //Render the report             
            //    return File(renderedBytes, mimeType, "Baocao" + DateTime.Now.ToString() + ".xls");
            //}
            //else
            //{
            //    return result;
            //}         
        }

        [HttpGet]
        public StatictsTodayModel FilterStatictsToday(string userIds, int employeeTypeId)
        {

            #region "Start Checkpoint"
            CheckPointApi checkPointApi = new CheckPointApi();
            var watch = new Stopwatch();
            watch.Start();
            checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "FilterStatictsToday", "Start", 0);
            #endregion 

            int totalExists;
            int handoverCount;
            int completeInCount;
            int completeOutCount;
            int notCompleteCount;
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            ContactRepository.ContactsFilterStatictsToday(userIds, branchId, DateTime.Now, out handoverCount, out completeInCount, out completeOutCount, out notCompleteCount, employeeType);
            ContactRepository.FilterTodayAll(userIds, DateTime.Now.AddMonths(-1).AddDays(-1).Date, DateTime.Now.Date.AddSeconds(-1), String.Empty, branchId, employeeType, 1, 30, out totalExists);

            #region "End CheckPoint"
            watch.Stop();
            checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "FilterStatictsToday", "End", watch.ElapsedMilliseconds);
            #endregion

            return new StatictsTodayModel
            {
                TotalExists = totalExists,
                HandoverCount = handoverCount,
                CompleteInCount = completeInCount,
                CompleteOutCount = completeOutCount,
                NotCompleteCount = notCompleteCount,
            };
        }
        #endregion

        #region Update
        [HttpPost]
        public string UpdateL1(FormDataCollection form)
        {
            var name = form.Get("name");
            var notes = form.Get("notes");
            var email1 = form.Get("email1");
            var email2 = form.Get("email2");
            var address = form.Get("address");
            var id = form.Get("id").ToInt32();
            var gender = form.Get("gender").ToInt32();
            var birthday = form.Get("birthday").ToDateTime();
            var productSellId = form.Get("productSellId").ToInt32();
            //Begin: 12/12/2020 cho phép thay đổi chiến dịch
            var CampaindTpe = form.Get("CampaindTpe");
            //End: 12/12/2020
            var mobile1 = Util.CleanAlphabetAndFirstZero(form.Get("mobile1"));
            var mobile2 = Util.CleanAlphabetAndFirstZero(form.Get("mobile2"));
            var mobile3 = Util.CleanAlphabetAndFirstZero(form.Get("mobile3"));

            try
            {
                var duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, mobile2, string.Empty, string.Empty, string.Empty, id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, mobile3, string.Empty, string.Empty, id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, string.Empty, email1, string.Empty, id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, string.Empty, string.Empty, email2, id);
                if (duplicateId == 0) duplicateId = ContactRepository.ContactIsDuplicate(mobile1, mobile2, mobile3, email1, email2, id);
                if (duplicateId > 0 && duplicateId != id)
                {
                    var contactInfoDb = ContactRepository.GetInfo(duplicateId);
                    if (contactInfoDb == null)
                        return "Cập nhật contact bị lỗi do trùng với contact khác trong hệ thống";

                    var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == contactInfoDb.UserConsultantId) ??
                               UserRepository.GetInfo(contactInfoDb.UserConsultantId);
                    return user == null
                        ? "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfoDb.Id + " - " + contactInfoDb.Fullname + " - Level " + contactInfoDb.LevelId + " - TVTS: chưa có ai chăm sóc"
                        : "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfoDb.Id + " - " + contactInfoDb.Fullname + " - Level " + contactInfoDb.LevelId + " - TVTS: " + user.FullName;
                }

                // Delete Redis
                StoreData.DeleteRedis(id);

                // Update
                var entity = new ContactInfo
                {
                    Id = id,
                    Notes = notes,
                    Email = email1,
                    Email2 = email2,
                    Fullname = name,
                    Gender = gender,
                    Mobile1 = mobile1,
                    Mobile2 = mobile2,
                    Mobile3 = mobile3,
                    Address = address,
                    Birthday = birthday,
                    ProductSellId = productSellId,
                    //Begin: 12/12/2020 cho phép thay đổi chiến dịch
                    CampaindTpeId = CampaindTpe.IsStringNullOrEmpty() ? 0 : StaticData.GetCampaindTpeId(CampaindTpe),
                    //End: 12/12/2020
                };
                ContactRepository.ContactUpdateL1(entity);

                // Load Redis
                StoreData.LoadRedis(entity.Id);
                return "Cập nhật contact thành công";
            }
            catch
            {
                // Redis
                StoreData.DeleteRedis(mobile1, mobile2, mobile3, email1, email2, id);
                return "Cập nhật contact bị lỗi, vui lòng thử lại sau";
            }
        }
        #endregion

        #region ContactDeleted
        [HttpGet]
        public ContactDeletedListModel FilterDeleted(int employeeTypeId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;

            int totalRecords;
            var results = new List<ContactDeletedModel>();
            var items = ContactRepository.FilterDeleted(branchId, employeeType, page, rows, out totalRecords) ?? new List<ContactDeletedInfo>();
            var listContacts = new List<ContactDeletedInfo>();
            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            foreach (var item in listContacts)
            {
                var itemJson = item.Json.ToObject<ContactDeletedJson>();

                var level = itemJson.ContactInfo != null
                    ? StoreData.ListLevel.FirstOrDefault(c => c.LevelId == itemJson.ContactInfo.LevelId) ?? new LevelInfo()
                    : new LevelInfo();

                var fullName = itemJson.ContactInfo != null ? itemJson.ContactInfo.Fullname : string.Empty;
                var deleteBy = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.DeletedBy) ?? new UserInfo();
                var branch = StoreData.ListBranch.FirstOrDefault(c => c.BranchId == item.BranchId) ?? new BranchInfo();

                string userName = string.Empty, statusMapName = string.Empty;
                if (itemJson.ContactInfo != null)
                {
                    switch ((StatusType)itemJson.ContactInfo.StatusId)
                    {
                        case StatusType.New:
                            {
                                userName = string.Empty;
                                var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == itemJson.ContactInfo.StatusMapCollaboratorId) ?? new StatusMapInfo();
                                statusMapName = statusMap.Name;
                            }
                            break;
                        case StatusType.HandoverCollaborator:
                        case StatusType.AcceptCollaborator:
                        case StatusType.RecoveryCollaborator:
                            {
                                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == itemJson.ContactInfo.UserCollaboratorId) ?? new UserInfo();
                                userName = user.FullName;

                                var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == itemJson.ContactInfo.StatusMapCollaboratorId) ?? new StatusMapInfo();
                                statusMapName = statusMap.Name;
                            }
                            break;
                        case StatusType.HandoverConsultant:
                        case StatusType.RecoveryConsultant:
                            {
                                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == itemJson.ContactInfo.UserConsultantId) ?? new UserInfo();
                                userName = user.FullName;

                                var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == itemJson.ContactInfo.StatusMapConsultantId) ?? new StatusMapInfo();
                                statusMapName = statusMap.Name;
                            }
                            break;
                    }
                }

                results.Add(new ContactDeletedModel
                {
                    Id = item.Id,
                    Email = item.Email,
                    UserName = userName,
                    Mobile = item.Phone,
                    FullName = fullName,
                    LevelName = level.Name,
                    StatusMapName = statusMapName,
                    DeletedTime = item.DeletedTime.HasValue
                        ? item.DeletedTime.Value.ToString("dd/MM/yyyy")
                        : string.Empty,
                    BranchName = branch.Name,
                    ContactId = item.ContactId,
                    DeletedName = deleteBy.FullName,
                });
            }
            var list = new ContactDeletedListModel
            {
                Page = page,
                Records = rows,
                Rows = results,
                UserData = totalRecords,
                Total = (totalRecords / rows) + 1,
            };
            return list;
        }
        #endregion

        #region Chuyển contact giữa các TVTS
        [HttpGet]
        public ContactForwardedListModel FilterForwarded(int employeeTypeId, int page, int rows)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            int totalRecords;
            var results = new List<ContactForwardedModel>();

            var items = ContactRepository.FilterForward(branchId, employeeType, page, rows, out totalRecords) ?? new List<ContactForwardInfo>();
            var listContacts = new List<ContactForwardInfo>();

            foreach (var item in items.Where(c => !listContacts.Exists(p => p.Id == c.Id)))
                listContacts.Add(item);

            foreach (var item in listContacts)
            {
                results.Add(new ContactForwardedModel
                {
                    Id = item.Id,
                    Fullname = item.Fullname,
                    Email = item.Email,
                    Level = item.Level,
                    Mobile = item.Mobile,
                    StatusCare = item.StatusCare,
                    ReceivedPerson = item.ReceivedPerson,
                    ForwardedPerson = item.ForwardedPerson,
                    ForwardPerson = item.ForwardPerson,
                    ForwardDate = item.ForwardDate
                });
            }

            var list = new ContactForwardedListModel
            {
                Page = page,
                Records = rows,
                Rows = results,
                UserData = totalRecords,
                Total = (totalRecords / rows) + 1,
            };
            return list;
        }
        #endregion

        #region Lấy danh sách contact bàn giao thành công cho QL TVTS L8

        public ContactListModel GetAllHandoverSuccessContactAdvisorManager(string name, string phone, string email, string userids, string listedprice, int sourcetype, string datewanttolearn, string handoverfromdateadvisor, string handovertodateadvisor, int packageFeeEdu, int appointmentType, int page, int rows)
        {

            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var dateWantToLearn = datewanttolearn.ToDateTime("ddMMyyyy");

            var handoverFromDateAdvisor = handoverfromdateadvisor.ToDateTime("ddMMyyyy");
            var handoverToDateAdvisor = handovertodateadvisor.ToDateTime("ddMMyyyy");
            if (!handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverFromDateAdvisor;
            if (handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverToDateAdvisor.Value.AddDays(1).AddSeconds(-1);

            int totalRecords;
            if (!phone.IsStringNullOrEmpty()) phone = Util.CleanAlphabetAndFirstZero(phone);

            var list = new ContactListModel
            {
                Rows = ContactRepository.GetHandoverSuccessContactAdvisorListManager(branchId, name, phone, email, userids, listedprice, sourcetype, dateWantToLearn, handoverFromDateAdvisor, handoverToDateAdvisor, packageFeeEdu, appointmentType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };
            int tongTien = 0;
            foreach (var contact in list.Rows)
            {
                contact.sHandoverStatusAdvisor = ObjectExtensions.GetEnumDescription((StatusHandoverAdvisor)contact.iHandoverStatusAdvisor);
                contact.AppointmentType = ObjectExtensions.GetEnumDescription((AppointmentType)contact.AppointmentTypeId);
                tongTien += contact.FeeEdu.ToInt32();
            }
            list.TotalMoney = tongTien;
            return list;
        }
        #endregion

        #region Tính tổng tiền học viên
        [HttpPost]
        public string GetTotalMoney(FormDataCollection form)
        {

            var user = UserContext.GetCurrentUser();
            var name = form.Get("name").ToString();
            var email = form.Get("email").ToString();
            var userids = form.Get("userids").ToString();
            var listedprice = form.Get("listedprice");
            var sourcetype = form.Get("sourcetype").ToInt32();
            var packageFeeEdu = form.Get("packageFeeEdu").ToInt32();
            var appointmentType = form.Get("appointmentType").ToInt32();
            var branchId = UserContext.GetDefaultBranch();

            var dateWantToLearn = form.Get("datewanttolearn").ToDateTime("ddMMyyyy");
            var handoverFromDateAdvisor = form.Get("handoverfromdateadvisor").ToDateTime("ddMMyyyy");
            var handoverToDateAdvisor = form.Get("handovertodateadvisor").ToDateTime("ddMMyyyy");
            if (!handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverFromDateAdvisor;
            if (handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverToDateAdvisor.Value.AddDays(1).AddSeconds(-1);

            int totalRecords;
            var phone = form.Get("mobile");
            if (!phone.IsStringNullOrEmpty()) phone = Util.CleanAlphabetAndFirstZero(phone);

            var list = new ContactListModel
            {
                Rows = ContactRepository.GetHandoverSuccessContactAdvisorListManager(branchId, name, phone, email, userids, listedprice, sourcetype, dateWantToLearn, handoverFromDateAdvisor, handoverToDateAdvisor, packageFeeEdu, appointmentType, out totalRecords),
            };

            decimal tongTien = 0;
            foreach (var contact in list.Rows)
            {
                tongTien += contact.FeeEdu.ToDecimal();
            }
            return tongTien.ToString();
        }
        #endregion

        #region Lấy danh sách contact bàn giao thành công cho QL TVTS L7
        public ContactListModel GetAllHandoverSuccessContactAdvisorManagerL7(string name, string phone, string email, string userids, string listedprice, int sourcetype, string datewanttolearn, string handoverfromdateadvisor, string handovertodateadvisor, int packageFeeEdu, int appointmentType, int page, int rows)
        {
            var user = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var dateWantToLearn = datewanttolearn.ToDateTime("ddMMyyyy");

            var handoverFromDateAdvisor = handoverfromdateadvisor.ToDateTime("ddMMyyyy");
            var handoverToDateAdvisor = handovertodateadvisor.ToDateTime("ddMMyyyy");
            if (!handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverFromDateAdvisor;
            if (handoverToDateAdvisor.HasValue) handoverToDateAdvisor = handoverToDateAdvisor.Value.AddDays(1).AddSeconds(-1);

            int totalRecords;
            if (!phone.IsStringNullOrEmpty()) phone = Util.CleanAlphabetAndFirstZero(phone);

            var list = new ContactListModel
            {
                Rows = ContactRepository.GetHandoverSuccessContactAdvisorListManagerL7(branchId, name, phone, email, userids, listedprice, sourcetype, dateWantToLearn, handoverFromDateAdvisor, handoverToDateAdvisor, packageFeeEdu, appointmentType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = totalRecords,
                Records = rows,
                Page = page,
            };

            foreach (var contact in list.Rows)
            {
                contact.sHandoverStatusAdvisor = ObjectExtensions.GetEnumDescription((StatusHandoverAdvisor)contact.iHandoverStatusAdvisor);
                contact.AppointmentType = ObjectExtensions.GetEnumDescription((AppointmentType)contact.AppointmentTypeId);
            }
            return list;
        }
        #endregion

        [HttpGet]
        public int GetCountContactDulicate()
        {
            var userConsulant = UserContext.GetCurrentUser();
            int relVal = ContactRepository.FilterDuplicateCountConsulant(userConsulant.UserID);
            return relVal;
        }
        #region Lấy danh sách contact duplicate cho quản lý
        [HttpGet]
        public ContactDuplicateListModel GetListContactDuplicateManager(string statusIds, int statusMapId, int statusCareId, string levelIds, string handoverFromDate, string handoverToDate, string callFromDate, string callToDate, int qualityId, int productSellId, string userIds, int employeeTypeId, string dateDuplicate, int page, int rows)
        {
            int branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType)employeeTypeId;
            int userId = UserContext.GetCurrentUser().UserID;
            DateTime? dateDuplicateN = dateDuplicate.ToDateTime("ddMMyyyy");

            DateTime? callTDate = callToDate.ToDateTime("ddMMyyyy");
            DateTime? callFDate = callFromDate.ToDateTime("ddMMyyyy");
            if (!callTDate.HasValue) callTDate = callFDate;
            if (callTDate.HasValue) callTDate = callTDate.Value.AddDays(1).AddSeconds(-1);

            DateTime? handoverTDate = handoverToDate.ToDateTime("ddMMyyyy");
            DateTime? handoverFDate = handoverFromDate.ToDateTime("ddMMyyyy");
            if (!handoverTDate.HasValue) handoverTDate = handoverFDate;
            if (handoverTDate.HasValue) handoverTDate = handoverTDate.Value.AddDays(1).AddSeconds(-1);

            int totalRecords;
            var results = new List<ContactDuplicateModel>();
            var items = ContactRepository.FilterDuplicateManager(statusIds, branchId, statusMapId, statusCareId, levelIds, handoverFDate, handoverTDate, callFDate, callTDate, qualityId, productSellId, userIds, employeeType, dateDuplicateN, page, rows, out totalRecords) ?? new List<ContactInfo>();

            var listContacts = new List<ContactInfo>();
            foreach (var item in items)
                listContacts.Add(item);

            foreach (var item in listContacts)
            {
                var status = ObjectExtensions.GetEnumDescription((StatusType)item.StatusId);
                var level = StoreData.ListLevel.FirstOrDefault(c => c.LevelId == item.LevelId) ?? new LevelInfo();
                var channel = ChannelRepository.GetAll().FirstOrDefault(c => c.ChannelId == item.ChannelId) ?? new ChannelInfo();
                var type = StoreData.ListSourceType.FirstOrDefault(c => c.SourceTypeId == item.TypeId) ?? new SourceTypeInfo();

                string appointmentDate = string.Empty, appointmentAmpm = string.Empty, appointmentTime = string.Empty;
                string userName = string.Empty, statusMapName = string.Empty, statusCareName = string.Empty, callInfo = string.Empty;
                switch ((StatusType)item.StatusId)
                {
                    case StatusType.New:
                        {
                            userName = string.Empty;
                            var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapCollaboratorId) ?? new StatusMapInfo();
                            statusMapName = statusMap.Name;

                            var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == item.StatusCareCollaboratorId) ?? new StatusCareInfo();
                            statusCareName = statusCare.Name;
                        }
                        break;
                    case StatusType.HandoverCollaborator:
                    case StatusType.AcceptCollaborator:
                    case StatusType.RecoveryCollaborator:
                        {
                            var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.UserCollaboratorId) ?? new UserInfo();
                            userName = user.FullName;

                            var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapCollaboratorId) ?? new StatusMapInfo();
                            statusMapName = statusMap.Name;

                            var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == item.StatusCareCollaboratorId) ?? new StatusCareInfo();
                            statusCareName = statusCare.Name;

                            appointmentDate = item.AppointmentCollaboratorDate.HasValue ? item.AppointmentCollaboratorDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                            appointmentTime = item.AppointmentCollaboratorTime.HasValue ? item.AppointmentCollaboratorTime.Value.ToString("HH:mm") : string.Empty;
                            appointmentAmpm = item.AppointmentCollaboratorAmPm;
                            callInfo = item.CallInfoCollaborator;
                        }
                        break;
                    case StatusType.HandoverConsultant:
                    case StatusType.RecoveryConsultant:
                        {
                            var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == item.UserConsultantId) ?? new UserInfo();
                            userName = user.FullName;

                            var statusMap = StoreData.ListStatusMap.FirstOrDefault(c => c.Id == item.StatusMapConsultantId) ?? new StatusMapInfo();
                            statusMapName = statusMap.Name;

                            var statusCare = StoreData.ListStatusCare.FirstOrDefault(c => c.Id == item.StatusCareConsultantId) ?? new StatusCareInfo();
                            statusCareName = statusCare.Name;

                            appointmentDate = item.AppointmentConsultantDate.HasValue ? item.AppointmentConsultantDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                            appointmentTime = item.AppointmentConsultantTime.HasValue ? item.AppointmentConsultantTime.Value.ToString("HH:mm") : string.Empty;
                            appointmentAmpm = item.AppointmentConsultantAmPm;
                            callInfo = item.CallInfoConsultant;
                        }
                        break;
                }
                results.Add(new ContactDuplicateModel
                {
                    Id = item.Id,
                    CallInfo = callInfo,
                    UserName = userName,
                    StatusName = status,
                    TypeName = type.Name,
                    Mobile = item.Mobile1,
                    LevelName = level.Name,
                    Fullname = item.Fullname,
                    ChannelName = channel.Name,
                    StatusMapName = statusMapName,
                    StatusCareName = statusCareName,
                    AppointmentDate = appointmentDate,
                    AppointmentAmPm = appointmentAmpm,
                    AppointmentTime = appointmentTime,
                    DuplicateInfo = item.DuplicateInfo,
                    Email = item.Email.IsStringNullOrEmpty() ? item.Email2 : item.Email,
                    ImportedDate = item.ImportedDate.HasValue
                                    ? item.ImportedDate.Value
                                    : item.CreatedDate.HasValue ? item.CreatedDate.Value : DateTime.Now,
                });
            }
            var list = new ContactDuplicateListModel
            {
                Page = page,
                Records = rows,
                Rows = results,
                UserData = totalRecords,
                Total = (totalRecords / rows) + 1,
            };
            return list;
        }
        #endregion 
        [HttpGet]
        public int HandoverContainerContact(string ids, int statusId, int employeeTypeId)
        {
            ContactRepository.ForwardTVTSContainer(ids, (EmployeeType)employeeTypeId, statusId);
            return ids.Split(',').Length;
        }
    }
}



