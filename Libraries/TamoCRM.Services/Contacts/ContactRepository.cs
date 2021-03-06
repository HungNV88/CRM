using System;
using System.Collections.Generic;
using System.Data;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactDeleted;
using TamoCRM.Domain.ContactForward;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.Reports;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.TemplateAds;
using TamoCRM.Domain.ContactLevelInfos;

namespace TamoCRM.Services.Contacts
{
    public class ContactRepository
    {

        public static List<ContactInfo> GetBC41(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC41(from, to));
        }
        public static List<ContactInfo> GetBC40(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC40(from, to));
        }
        public static List<ContactInfo> GetBC42(int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, EmployeeType employeeType)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC42( branchId,  userIds,  statusMapId,  statusCareId,  levels, handoverFromDate,  handoverToDate,  callFromDate,  callToDate,  qualityId,  productSellId,  employeeType));
        }
        public static List<ContactInfo> GetBC44(int statusId, int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, int sourceType, EmployeeType employeeType)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC44(statusId, branchId, userIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, sourceType, employeeType));
        }
        public static List<ContactInfo> GetBC45(DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? registerFromDate, DateTime? registerToDate, int channelId, int templateAdsId)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC45(handoverFromDate, handoverToDate, registerFromDate, registerToDate, channelId, templateAdsId));
        }
        public static List<ReportBC46Info> GetBC46(DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? registerFromDate, DateTime? registerToDate, int channelId,int statuscontact)
        {
            return ObjectHelper.FillCollection<ReportBC46Info>(DataProvider.Instance().Contacts_GetBC46(handoverFromDate, handoverToDate, registerFromDate, registerToDate, channelId, statuscontact));
        }
        public static List<ReportBC47Info> GetBC47(string name, string phone, string email, string userids, string listedprice,int sourcetype,DateTime? datewanttolearn,DateTime? handoverfromadvisor,DateTime? handovertoadvisor)
        {
            return ObjectHelper.FillCollection<ReportBC47Info>(DataProvider.Instance().Contacts_GetBC47(name, phone, email, userids, listedprice, sourcetype,datewanttolearn, handoverfromadvisor, handovertoadvisor));
        }
        public static List<ContactInfo> GetBC20(DateTime handoverDate)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC20(handoverDate));
        }

        public static List<ContactInfo> GetBC03(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC03(from, to));
        }
        public static List<ContactInfo> GetBC43(DateTime? fromRegister, DateTime? toRegister,DateTime? fromHandover,DateTime? toHandover, string userIds, int statusType)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC43(fromRegister, toRegister, fromHandover, toHandover, userIds, statusType));
        }

        public static List<ContactInfo> GetBC06(DateTime from, DateTime to)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC06(from, to));
        }

        public static ContactInfo GetByMobile(string mobile)
        {
            return ObjectHelper.FillObject<ContactInfo>(DataProvider.Instance().Contacts_GetByMobile(mobile));
        }

        //=========== 25/11/2015 ===========//
        public static ContactFixedMoney GetInfoFeeEduByMobile(string mobile) //Lấy thông tin sửa tiền 
        {
            return ObjectHelper.FillObject<ContactFixedMoney>(DataProvider.Instance().Contacts_GetInfoFeeEduByMobile(mobile));
        }

        public static int CountByChannel(int branchId, int sourceTypeId, int levelId, int channelId, DateTime from, DateTime to)
        {
            return DataProvider.Instance().Contacts_CountByChannel(branchId, sourceTypeId, levelId, channelId, from, to);
        }

        public static int Count(string sourceTypeIds, string channelIds, string schoolIds, int levelId, int statusId, DateTime from, DateTime to)
        {
            return DataProvider.Instance().Contacts_Count(sourceTypeIds, channelIds, schoolIds, levelId, statusId, from, to);
        }
        public static void UpdateAcceptance(int contactId, int acceptStatus, int contactStatusId, int statusMapId, int levelId, int createdBy)
        {
            DataProvider.Instance().Contacts_UpdateAcceptance(contactId, acceptStatus, contactStatusId, contactStatusId, levelId, createdBy);
        }

        public static void UpdateStatusMap(int contactId, int statusMapId)
        {
            DataProvider.Instance().Contacts_Update_StatusMap(contactId, statusMapId);
        }

        public static int Create(ContactInfo info)
        {
            return DataProvider.Instance().Contacts_Insert(info.Code, info.Fullname, info.Birthday, info.Email, info.Address, info.Gender, info.TypeId, info.ChannelId, info.LevelId, info.CollectorId, info.BranchId, info.StatusId, info.StatusCareConsultantId, info.RegisteredDate, info.Notes, info.ImportId);
        }

        public static void Update(ContactInfo info)
        {
            DataProvider.Instance().Contacts_Update(info.Id, info.Code, info.Fullname, info.Address, info.Birthday, info.Gender, info.Email, info.Email2, info.Notes, info.RegisteredDate, info.CreatedDate, info.ImportedDate, info.HandoverCollaboratorDate, info.RecoveryCollaboratorDate, info.AppointmentCollaboratorDate, info.CallCollaboratorDate, info.HandoverConsultantDate, info.RecoveryConsultantDate, info.AppointmentConsultantDate, info.CallConsultantDate, info.CollectorId, info.TypeId, info.ChannelId, info.CampaindTpeId, info.LandingPageId, info.TemplateAdsId, info.SearchKeywordId, info.PackageId, info.LevelId, info.BranchId, info.StatusId, info.ImportId, info.ContainerId, info.ProductSellId, info.ProductSoldId, info.StatusMapCollaboratorId, info.StatusCareCollaboratorId, info.StatusMapConsultantId, info.StatusCareConsultantId, info.UserImportId, info.UserErrorId, info.UserHandoverCollaboratorId, info.UserCollaboratorId, info.UserRecoveryCollaboratorId, info.UserHandoverConsultantId, info.UserConsultantId, info.UserRecoveryConsultantId, info.DraftCollaborator, info.DraftConsultant, info.CallInfoCollaborator, info.CallInfoConsultant, info.HandoverHistoryCollaboratorId, info.HandoverHistoryConsultantId, info.CallCount);
        }

        public static ContactInfo GetInfo(int id)
        {
            return ObjectHelper.FillObject<ContactInfo>(DataProvider.Instance().Contacts_GetInfo(id));
        }    

        public static ContactInfo GetInfoForAutoSale(int id)
        {
            return ObjectHelper.FillObject<ContactInfo>(DataProvider.Instance().Contacts_GetInfoForAutoSale(id));
        }

        public static ContactInfo GetInfo_OldDatabase(int id)
        {
            return ObjectHelper.FillObject<ContactInfo>(DataProvider.Instance().Contacts_GetInfo_OldDatabase(id));
        }

        public static List<ContactInfo> GetAll()
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetAll());
        }

        public static List<ContactInfo> GetHandoverContactAdvisorList(int userId, string name, string phone, string email, int statusHandoverId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetAllHandoverAdvisor(userId, name, phone, email, statusHandoverId, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> GetHandoverContactAdvisorManagerList(int branchId, string name, string phone, string email, int statusHandoverId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetAllHandoverManagerAdvisor(branchId, name, phone, email, statusHandoverId, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> GetHandoverSuccessContactAdvisorList(int userId, string name, string phone, string email, int page, int rows, out int totalRecords)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetAllSuccessHandoverAdvisor(userId, name, phone, email, page, rows), out totalRecords);
        }

        public static List<ContactInfo> GetHandoverSuccessContactAdvisorListManager(int branchId, string name, string phone, string email, string userids, string listedprice,int sourcetype,DateTime? datewanttolearn,DateTime? handoverfromadvisor,DateTime? handovertoadvisor,int packageFeeEdu,int appointmentType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetAllSuccessHandoverManagerAdvisor(branchId, name, phone, email, userids, listedprice, sourcetype, datewanttolearn,handoverfromadvisor, handovertoadvisor, packageFeeEdu, appointmentType, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> GetHandoverSuccessContactAdvisorListManager(int branchId, string name, string phone, string email, string userids, string listedprice, int sourcetype, DateTime? datewanttolearn, DateTime? handoverfromadvisor, DateTime? handovertoadvisor, int packageFeeEdu, int appointmentType, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetAllSuccessHandoverManagerAdvisor(branchId, name, phone, email, userids, listedprice, sourcetype, datewanttolearn, handoverfromadvisor, handovertoadvisor, packageFeeEdu, appointmentType), out totalRecord);
        }
        public static List<ContactInfo> GetHandoverSuccessContactAdvisorListManagerL7(int branchId, string name, string phone, string email, string userids, string listedprice, int sourcetype, DateTime? datewanttolearn, DateTime? handoverfromadvisor, DateTime? handovertoadvisor, int packageFeeEdu, int appointmentType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetAllSuccessHandoverManagerAdvisorL7(branchId, name, phone, email, userids, listedprice, sourcetype, datewanttolearn, handoverfromadvisor, handovertoadvisor, packageFeeEdu, appointmentType, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> GetAll_ForRedis()
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetAll_ForRedis());
        }
        public static List<ContactInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> Search(int collector, int channelId, int importId, int levelId, int sourceTypeId, int statusId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Search(collector, channelId, importId, levelId, sourceTypeId, statusId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> SearchDistributedContacts(int collector, int channelId, int importId, int levelId, int sourceTypeId, int statusId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_SearchDistributedContacts(collector, channelId, importId, levelId, sourceTypeId, statusId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> SearchDuplicateMo(int branchId, DateTime? importDate, int importId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_SearchDuplicateMo(branchId, importDate, importId, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> SearchDuplicateMo(int branchId, DateTime? importDate, int importId)
        {
            //return FillContactCollection(DataProvider.Instance().Contacts_SearchDuplicateMo(branchId, importDate, importId));
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_SearchDuplicateMo(branchId, importDate, importId));
        }
        public static List<ContactInfo> SearchDuplicateMo(int collector, int channelId, int importId, int levelId, int sourceTypeId, int branchId,DateTime? importDate, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_SearchDuplicateMo(collector, channelId, importId, levelId, sourceTypeId, branchId, importDate, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> GetForAcceptance(int printId, string pageCode, int levelId, int pageIndex, int pageSize, out int totalRecords)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetForAcceptance(printId, pageCode, levelId, pageIndex, pageSize), out totalRecords);
        }
        public static List<ContactInfo> GetForImportTeam(int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int schoolId, int statusId, int page, int rows, out int totalRecords)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetForImportTeam(branchId, collectorId, channelId, importId, levelId, sourceTypeId, statusId, page, rows), out totalRecords);
        }
        public static List<ContactInfo> GetForFilterTeam(int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int schoolId, int statusId, int page, int rows, out int totalRecords)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_GetForFilterTeam(branchId, collectorId, channelId, importId, levelId, sourceTypeId, statusId, page, rows), out totalRecords);
        }

        public static List<ContactInfo> FilterRecoveryDistributor(int levelId, int sourceTypeId, string statusIds, string statusMapIds, DateTime? fromDate, DateTime? toDate, int branchId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Recovery_Distributor(levelId, sourceTypeId, statusIds, statusMapIds, fromDate, toDate, branchId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterRecoveryAppointment(string users, string levels, int statusMapId, int statusCareId, int day, int branchId, int pageIndex, int pageSize, out int totalRecord)
        {
            if (users == null) users = string.Empty;
            if (levels == null) levels = string.Empty;
            const int statusId = (int)StatusType.HandoverConsultant;

            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Recovery_Appointment(statusId, users, levels, statusMapId, statusCareId, day, branchId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterRecoveryNew(string users, int statusMapId, int statusCareId, int day, int branchId, int pageIndex, int pageSize, out int totalRecord)
        {
            if (users == null) users = string.Empty;
            const int statusId = (int)StatusType.HandoverConsultant;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Recovery_New(statusId, users, statusMapId, statusCareId, day, branchId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterRecovered(string sourceTypes, string levels, int statusMapId, int statusCareId, int branchId, DateTime? recoveryTDate, DateTime? recoveryFDate, int pageIndex, int pageSize, out int totalRecord)
        {
            if (levels == null) levels = string.Empty;
            if (sourceTypes == null) sourceTypes = string.Empty;
            const int statusId = (int)StatusType.HandoverConsultant;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Recovered(statusId, sourceTypes, levels, statusMapId, statusCareId, branchId, recoveryTDate, recoveryFDate, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterSearch(string mobile, string email, int branchId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search(mobile, email, branchId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterCCL2Plus(int branchId, string levelIds, int sourceTypeId, int page, int rows, out int totalRecords)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_CCL2_Plus(branchId, levelIds, sourceTypeId, page, rows), out totalRecords);
        }
        public static int FilterCountHandover(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds)
        {
            return DataProvider.Instance().Contacts_Filter_Count_Handover(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds);
        }
        public static int ContactRecoveryDistributor(string ids, int statusId, DateTime recoveryDate, int createdBy)
        {
            return DataProvider.Instance().Contacts_Recovery_Distributor(ids, statusId, recoveryDate, createdBy);
        }
        public static int ContactRecoveryDistributorAll(int levelId, int sourceTypeId, string statusIds, string statusMapIds, DateTime? fromDate, DateTime? toDate, int branchId, int statusId, DateTime recoveryDate, int createdBy)
        {
            return DataProvider.Instance().Contacts_Recovery_Distributor_All(levelId, sourceTypeId, statusIds, statusMapIds, fromDate, toDate, branchId, statusId, recoveryDate, createdBy);
        }
        public static void ContactReuse(string ids, int statusId, DateTime reuseDate, int sourceTypeId)
        {
            DataProvider.Instance().Contacts_Update_Reuse(ids, statusId, reuseDate, sourceTypeId);
        }
        public static void ContactUpdateDraft(string ids, int userId, int draft, EmployeeType type)
        {
            if (string.IsNullOrEmpty(ids)) return;
            DataProvider.Instance().Contacts_Update_Draft(ids, userId, draft, type);
        }
        public static void ContactUpdateClearDraft(EmployeeType type)
        {
            DataProvider.Instance().Contacts_Update_ClearDraft(type);
        }
        public static int ContactUpdateHandover(int branchId, int levelId, int statusId, int statusMapId, int statusCareId, int productSellId, DateTime handoverDate, DateTime appointmentDate, int createdBy, EmployeeType type)
        {
            return DataProvider.Instance().Contacts_Update_Handover(branchId, levelId, statusId, statusMapId, statusCareId, productSellId, handoverDate, appointmentDate, createdBy, type);
        }
        public static void ContactUpdateUserId(int id, int userId, int branchId, int createdBy)
        {
            DataProvider.Instance().Contacts_Update_UserId(id, userId, branchId, createdBy);
        }

        public static List<ContactInfo> FilterDuplicateTvts(int userId, string statusIds, int branchId, int pageIndex, int pageSize, out int totalRecord)
        {
            if (statusIds == null) statusIds = string.Empty;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_DuplicateTvts(userId, statusIds, branchId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterDraft(int branchId)
        {
            int totalRecord;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Draft(branchId), out totalRecord);
        }
       
        public static int CreateHv(ContactInfo info)
        {
            return DataProvider.Instance().Contacts_Insert_Hv(info.Email, info.Email2, info.LevelId, info.Birthday, info.TypeId, info.Gender, info.Fullname, info.ChannelId, info.BranchId, info.CollectorId, info.StatusId, info.UserConsultantId, info.StatusMapConsultantId, info.StatusCareConsultantId, info.RegisteredDate, info.CreatedDate, info.HandoverConsultantDate, info.AppointmentConsultantDate, info.Mobile1, info.Mobile2, info.CreatedBy);
        }

        
        public static int ContactReuseAll(string statusIds, string sourceTypes, string levels, string educationLevels, int statusMapId, int statusCareId, string schools, string majors, int duplicate, int branchId, int statusId, DateTime? reuseDate, int sourceTypeId, int createdBy)
        {
            return DataProvider.Instance().Contacts_ReuseAll(statusIds, sourceTypes, levels, educationLevels, statusMapId, statusCareId, schools, majors, duplicate, branchId, statusId, reuseDate, sourceTypeId, createdBy);
        }
        public static void ContactUpdateDuplicate(int id, int sourceTypeId, int statusId)
        {
            DataProvider.Instance().Contacts_Update_Duplicate(id, sourceTypeId, statusId);
        }
        public static void ContactUpdateFullName(int id, string fullName)
        {
            DataProvider.Instance().Contacts_Update_FullName(id, fullName);
        }
        public static void ContactUpdateLevel(int id, int levelId, int createdBy)
        {
            DataProvider.Instance().Contacts_Update_Level(id, levelId, createdBy);
        }
        public static void ContactsAcceptL1(ContactInfo entity, string pageCode, int createdBy)
        {

        }
        public static int ContactForwardDistributor(string ids, int targetBranchId, int createdBy)
        {
            return DataProvider.Instance().Contacts_Forward_Distributor(ids, targetBranchId, createdBy);
        }
        public static int ContactForwardAllDistributor(int levelId, string statusIds, int statusMapId, int statusCareId, int branchId, string userIds, DateTime? fromDate, DateTime? toDate, int targetBranchId, int createdBy)
        {
            return DataProvider.Instance().Contacts_ForwardAll_Distributor(levelId, statusIds, statusMapId, statusCareId, branchId, userIds, fromDate, toDate, targetBranchId, createdBy);
        }
        public static List<ContactAppointmentInfo> ContactReportAppointment(int userId, int branchId, DateTime fromDate, DateTime toDate)
        {
            return ObjectHelper.FillCollection<ContactAppointmentInfo>(DataProvider.Instance().Contacts_Report_Appointment(userId, branchId, fromDate, toDate));
        }
        public static int ContactPrintAll(int branchId, DateTime fromDate, DateTime toDate, string channelIds, string channelAmounts, int maxRowPerPage, int createdBy, DateTime createdDate)
        {
            return DataProvider.Instance().Contacts_PrintAll(branchId, fromDate, toDate, channelIds, channelAmounts, maxRowPerPage, createdBy, createdDate);
        }
        public static int ContactHandoverDistributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds, string channelIds, string schoolIds, int total, int userId, int statusIdNext, int statusMapDistributorId, DateTime handoverDistributorDate, int createdBy, DateTime createdDate)
        {
            return DataProvider.Instance().Contacts_Handover_Distributor(branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds, channelIds, schoolIds, total, userId, statusIdNext, statusMapDistributorId, handoverDistributorDate, createdBy, createdDate);
        }
        public static List<ContactInfo> FilterForCollaborator(int branchId, int importId, int channelId, int schoolId, DateTime? startImportDate, DateTime? endImportDate, DateTime? startRegisterDate, DateTime? endRegisterDate, int connectStatus, int todayType, string statusMapIds, int campainId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_For_Collaborator(branchId, importId, channelId, startImportDate, endImportDate, startRegisterDate, endRegisterDate, connectStatus, todayType, statusMapIds, campainId, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterForCollaboratorL1(int branchId, int importId, int channelId, int schoolId, DateTime? startImportDate, DateTime? endImportDate, DateTime? startRegisterDate, DateTime? endRegisterDate, int connectStatus, int todayType, string statusMapIds, int campainId, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_For_CollaboratorL1(branchId, importId, channelId, startImportDate, endImportDate, startRegisterDate, endRegisterDate, connectStatus, todayType, statusMapIds, campainId, pageIndex, pageSize), out totalRecord);
        }
        public static int ContactHandoverToCampain(int total, int campainId, int branchId, int importId, int channelId, int schoolId, DateTime? startImportDate, DateTime? endImportDate, DateTime? startRegisterDate, DateTime? endRegisterDate, int connectStatus, int todayType, string statusMapIds, int createdBy, DateTime createdDate)
        {
            return DataProvider.Instance().Contacts_Handover_ToCampain(total, campainId, branchId, importId, channelId, startImportDate, endImportDate, startRegisterDate, endRegisterDate, connectStatus, todayType, statusMapIds, createdBy, createdDate);
        }

        // Handover
        public static List<ContactInfo> FilterHandover(int amountContacts, int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, EmployeeType employeeType, int statusCareId, int statusMapId)
        {
            int totalRecord;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Handover(amountContacts, branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId), out totalRecord);
        }
        
        public static List<ContactInfo> FilterHandoverForMOL(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, EmployeeType employeeType, int statusCareId, int statusMapId, DateTime? fromRegisterDate, DateTime? toRegisterDate)
        {
            int totalRecord;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Handover_ForMOL(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, employeeType, statusCareId, statusMapId, fromRegisterDate, toRegisterDate), out totalRecord);
        }
        // Today
        public static void ContactsFilterStatictsToday(string userIds, int branchId, DateTime dateTime, out int handoverCount, out int completeInCount, out int completeOutCount, out int notCompleteCount, EmployeeType type)
        {
            var reader = DataProvider.Instance().Contacts_Filter_Staticts_Today(userIds, branchId, dateTime, type);
            if (reader.Read())
            {
                handoverCount = reader["HandoverCount"].ToInt32();
                completeInCount = reader["CompleteInCount"].ToInt32();
                completeOutCount = reader["CompleteOutCount"].ToInt32();
                notCompleteCount = reader["NotCompleteCount"].ToInt32();
                reader.Close();
                return;
            }
            handoverCount = 0;
            completeInCount = 0;
            completeOutCount = 0;
            notCompleteCount = 0;
        }
        public static List<ContactInfo> FilterTodayAll(string userIds, DateTime fromDate, DateTime toDate, string levelIds, int branchId, EmployeeType type, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Today_All(userIds, fromDate, toDate, levelIds, branchId, type, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterTodayNewAndAppointment(string userIds, DateTime fromDate, DateTime toDate, int branchId, EmployeeType type, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Today_NewAndAppointment(userIds, fromDate, toDate, branchId, type, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterTodayAppointment(string userIds, DateTime fromDate, DateTime toDate, string levels, int branchId, EmployeeType type, int pageIndex, int pageSize, out int totalRecord)
        {
            if (levels == null) levels = string.Empty;
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Today_Appointment(userIds, fromDate, toDate, levels, branchId, type, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactInfo> FilterTodayNew(string userIds, DateTime fromDate, DateTime toDate, int branchId, EmployeeType type, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Today_New(userIds, fromDate, toDate, branchId, type, pageIndex, pageSize), out totalRecord);
        }

        // Recovery
        public static List<ContactInfo> FilterRecovery(string userIds, string levelIds, int statusMapId, int statusCareId, int branchId, TodayType todayType, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int sourceType, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Recovery(userIds, levelIds, statusMapId, statusCareId, branchId, todayType, handoverFromDate, handoverToDate, callFromDate, callToDate, sourceType, employeeType, pageIndex, pageSize), out totalRecord);
        }
        public static int ContactRecoveryAll(string userIds, string levelIds, int statusMapId, int statusCareId, int day, int branchId, int sourceType, TodayType todayType, EmployeeType employeeType, int userId, DateTime recoveryDate, int createdBy,DateTime? callToDate,DateTime? callFromDate,DateTime? handoverToDate,DateTime? handoverFromDate)
        {
            return DataProvider.Instance().Contacts_Recovery_All(userIds, levelIds, statusMapId, statusCareId, day, branchId, sourceType, todayType, employeeType, userId, recoveryDate, createdBy, callToDate, callFromDate, handoverToDate, handoverFromDate);
        }
        public static void ContactRecovery(string ids, int userId, DateTime recoveryDate, EmployeeType employeeType, int createdBy)
        {
            DataProvider.Instance().Contacts_Update_Recovery(ids, userId, recoveryDate, employeeType, createdBy);
        }
        // Khi import contact bi duplicate, se chon cac contact duplicate va chuyen ve kho MOL hoặc kho thu hồi 1-6
        public static void ContactRecoveryContainerDuplicate(string ids, int userId, int statusId, DateTime recoveryDate, EmployeeType employeeType, int createdBy)
        {
            DataProvider.Instance().Contacts_Update_RecoveryContainer_Duplicate(ids, userId, statusId, recoveryDate, employeeType, createdBy);
        }

        // Change
        public static int UpdateChangeContainer(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, int containerId, int productSellId, int createdBy)
        {
            return DataProvider.Instance().Contacts_Update_Change_Container(branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, containerId, productSellId, createdBy);
        }
        
        public static List<ContactInfo> FilterContactForChange(int branchId, string levelids, int statusMapId, int statusCareId, string userIds, DateTime fromDate, DateTime toDate, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_ForChange(branchId, levelids, statusMapId, statusCareId, userIds, fromDate, toDate, employeeType, pageIndex, pageSize), out totalRecord);
        }
        public static int ContactForwardAll(int branchId, int statusMapId, int statusCareId, string userIds, DateTime fromDate, DateTime toDate, EmployeeType employeeType, int targetUserId, int targetBranchId, DateTime handoverDate, int createdBy)
        {
            return DataProvider.Instance().Contacts_ForwardAll(branchId, statusMapId, statusCareId, userIds, fromDate, toDate, employeeType, targetUserId, targetBranchId, handoverDate, createdBy);
        }
        public static int ContactForwardAllContainer(DateTime importDate, int importId, int statusId, EmployeeType employeeTypeId)
        {
            return DataProvider.Instance().Contacts_ForwardAllContainer(importDate, importId, statusId, employeeTypeId);
        }
        public static void ContactForward(string ids, EmployeeType employeeType, int targetUserId, int targetBranchId, DateTime handoverDate, int createdBy)
        {
            DataProvider.Instance().Contacts_Forward(ids, employeeType, targetUserId, targetBranchId, handoverDate, createdBy);
        }
        public static void ForwardTVTSContainer(string ids, EmployeeType employeeType, int statusId)
        {
            DataProvider.Instance().Contacts_Forward_TVTSContainer(ids, statusId, employeeType);
        }
        public static void ContactHandoverFast(string ids, EmployeeType employeeType, int targetUserId, int targetBranchId, DateTime handoverDate,int createdBy)
        {
            DataProvider.Instance().Contacts_HandoverContactFast(ids, employeeType, targetUserId, targetBranchId, handoverDate, createdBy);
        }
        // Search
        public static List<ContactInfo> FilterSearchManager(int branchId, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Manager(branchId, name, mobile, email, employeeType, pageIndex, pageSize), out totalRecord);
        }

        public static int FilterSearchCountConsultant(int branchId,string userIds,string name,string mobile,string email,EmployeeType employeeTypeId)
        {
            return DataProvider.Instance().Filter_Search_Count_Consultant(branchId, userIds, name, mobile, email, employeeTypeId);
        }

        public static List<ContactInfo> FilterSearchHandoverContact(int branchId, string name, string mobile,string email, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contact_Filter_Search_Handover(branchId, name, mobile, email, employeeType, pageIndex, pageSize),out totalRecord);
        }
        public static List<ContactInfo> FilterSearchFast(int branchId, string userIds, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Fast(branchId, userIds, name, mobile, email, employeeType, pageIndex, pageSize), out totalRecord);
        }

        //Code tim kiem cho TVTS
        public static List<ContactInfo> FilterSearchFastForConsultant(int branchId, int userId, string name, string mobile, string email, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Fast_For_Consultant(branchId, userId, name, mobile, email, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> FilterSearchFastForCollaborator(int branchId, int userId, string name, string mobile, string email, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Fast_For_Collaborator(branchId, userId, name, mobile, email, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> FilterSearchFastForManagerConsultant(int branchId, int userId, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Fast_For_Manager_Consultant(branchId, userId, name, mobile, email, employeeType, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> FilterSearchAdvance(int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            if (callFromDate.HasValue && !callToDate.HasValue) callToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            if (handoverFromDate.HasValue && !handoverToDate.HasValue) handoverToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Advance(branchId, userIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, employeeType, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> FilterSearchAdvanceManager(int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            if (callFromDate.HasValue && !callToDate.HasValue) callToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            if (handoverFromDate.HasValue && !handoverToDate.HasValue) handoverToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Search_Advance_Manager(branchId, userIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, employeeType, pageIndex, pageSize), out totalRecord);
        }
        // Delete
        public static void Delete(int id, ContactDeletedInfo info, EmployeeType employeeType)
        {
            DataProvider.Instance().Contacts_Delete(id, info.ContactId, info.BranchId, info.UserId, info.Phone, info.Email, info.DeletedBy, info.DeletedTime, info.Json, employeeType);
        }
        public static List<ContactDeletedInfo> FilterDeleted(int branchId, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactDeletedCollection(DataProvider.Instance().Contacts_Filter_Deleted(branchId, employeeType, pageIndex, pageSize), out totalRecord);
        }

        public static List<ContactInfo> GetListContactsId(string strPhones)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacst_GetContactsId(strPhones));        
        }
        public static void Contacts_Delete_ImportFile(string strContactsId)
        {
            DataProvider.Instance().Contacts_Delete_ImportFile(strContactsId);
        }
        // Forward
        public static List<ContactForwardInfo> FilterForward(int branchId,EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactForwardedCollection(DataProvider.Instance().Contacts_Filter_Forward(branchId, employeeType, pageIndex, pageSize), out totalRecord);
        }

        //public static List<Contact>

        // Duplicate
        public static List<ContactInfo> FilterDuplicate(int userId, string statusIds, int branchId, EmployeeType employeeType, DateTime? dateDuplicate, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Duplicate(userId, statusIds, branchId, employeeType, dateDuplicate, pageIndex, pageSize), out totalRecord);
        }

        public static int FilterDuplicateCountConsulant(int userId)
        {
            return DataProvider.Instance().Contact_Filter_Dulicate_Count(userId);
        }

        // Create
        public static int CreateTmp(ContactInfo info)
        {
            return DataProvider.Instance().Contacts_Insert_Tmp(
                info.Code, info.Fullname, info.Address, info.Birthday, info.Gender, info.Email,
                info.Email2, info.Notes, info.RegisteredDate, info.CreatedDate, info.ImportedDate, info.CollectorId,
                info.TypeId, info.ChannelId, info.CampaindTpeId, info.LandingPageId,
                info.TemplateAdsId, info.SearchKeywordId, info.PackageId, info.LevelId, info.BranchId, info.StatusId,
                info.ImportId, info.ContainerId, info.UserImportId, info.UserErrorId, info.Mobile1, info.Mobile2,
                info.Mobile3, info.CreatedBy);
        }
        public static int CreateTvts(ContactInfo info)
        {
            return DataProvider.Instance().Contacts_Insert_Tvts(
                info.Email, info.Email2, info.LevelId, info.Birthday, info.TypeId, info.Gender,
                info.Fullname, info.Address, info.Notes, info.BranchId, info.ProductSellId, info.StatusId, info.UserConsultantId,
                info.StatusMapConsultantId, info.StatusCareConsultantId, info.RegisteredDate, info.CreatedDate,
                info.HandoverConsultantDate, info.AppointmentConsultantDate, info.Mobile1, info.Mobile2, info.CreatedBy, info.CampaindTpeId);
        }
        public static int CreateHotline(ContactInfo info)
        {
            return DataProvider.Instance()
                .Contacts_Insert_Hotline(info.ChannelId,
                info.Email, info.Email2, info.LevelId, info.Birthday, info.TypeId, info.Gender,
                info.Fullname, info.Address, info.Notes, info.BranchId, info.ProductSellId, info.StatusId, info.UserConsultantId,
                info.StatusMapConsultantId, info.StatusCareConsultantId, info.RegisteredDate, info.CreatedDate,
                info.HandoverConsultantDate, info.AppointmentConsultantDate, info.Mobile1, info.Mobile2, info.CreatedBy);
        }

        // Update
        public static void ContactUpdateL1(ContactInfo info)
        {
            DataProvider.Instance().Contacts_Update_L1(info.Id, info.Fullname, info.Birthday, info.Email, info.Email2, info.Address, info.Gender, info.ProductSellId, info.Notes, info.Mobile1, info.Mobile2, info.Mobile3, info.CampaindTpeId);
        }

        // CheckDuplicate
        public static int ContactIsDuplicate(string mobile1, string mobile2, string tel, string email, string email2)
        {
            return DataProvider.Instance().Contacts_IsDuplicate(mobile1, mobile2, tel, email, email2);
        }
        public static int ContactIsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId)
        {
            return DataProvider.Instance().Contacts_IsDuplicate(mobile1, mobile2, tel, email, email2, contactId);
        }

        private static List<ContactInfo> FillContactCollection(IDataReader reader, out int totalRecords)
        {

            List<ContactInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<ContactInfo>(reader, false);

                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }
            }
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }
        private static List<ContactDeletedInfo> FillContactDeletedCollection(IDataReader reader, out int totalRecords)
        {

            List<ContactDeletedInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<ContactDeletedInfo>(reader, false);

                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }

            }
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }

        private static List<ContactForwardInfo> FillContactForwardedCollection(IDataReader reader, out int totalRecords)
        {

            List<ContactForwardInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<ContactForwardInfo>(reader, false);

                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }

            }
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }

        public static ContactInfo GetInfoByMobile(string mobile)
        {
            return ObjectHelper.FillObject<ContactInfo>(DataProvider.Instance().Contacst_GetInfoByMobile(mobile));
        }

        public static List<ContactInfo> FilterDuplicateManager(string statusIds, int branchId, int statusMapId, int statusCareId, string levelIds, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, string userIds, EmployeeType employeeType, DateTime? dateDuplicateN, int page, int rows, out int totalRecord)
        {
            return FillContactCollection(DataProvider.Instance().Contacts_Filter_Duplicate_Manager(statusIds, branchId, statusMapId, statusCareId, levelIds, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, userIds, employeeType, dateDuplicateN, page, rows), out totalRecord);
        }

        public static List<ContactInfo> GetBC50(int branchId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, EmployeeType employeeType)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetBC50(branchId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, employeeType));
        }
        
        public static int UpdateChangeStatusContact(int branchId, string typeIds, string levelIds, string importIds, int statusId, int statusIdMOL, string channelIds, string channelAmounts, int createdBy, DateTime? fromRegisterDate, DateTime? toRegisterDate)
        {
            return DataProvider.Instance().Contacts_Update_Change_StatusContact(branchId, typeIds, levelIds, importIds, statusId, statusIdMOL, channelIds, channelAmounts, createdBy, fromRegisterDate, toRegisterDate);
        }

        public static List<ContactInfo> GetCallHistoryReport(string userids,DateTime? callFDate,DateTime? callTDate)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().Contacts_GetCallHistory(userids, callFDate, callTDate));
        }

        public static void UpdateChangeStatusId(int contactId)
        {
            DataProvider.Instance().Contacts_Update_Change_Status(contactId);
        }

        public static void HandoverContainerContact(string ids, int statusId, int employeeTypeId)
        {
            DataProvider.Instance().Contacts_Update_Change_Container_TVTS(ids, statusId, employeeTypeId);
        }
    }
}
