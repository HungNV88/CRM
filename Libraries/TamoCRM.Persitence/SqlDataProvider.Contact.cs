using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;
using System.Data.Common;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader Contacts_GetBC41(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetL8"), from, to);
        }

        public override IDataReader Contacts_GetBC42(int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, EmployeeType employeeType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_TVTSXuatContact"), branchId,  userIds,  statusMapId,  statusCareId,  levels, handoverFromDate,handoverToDate, callFromDate, callToDate,  qualityId,  productSellId);
        }

        public override IDataReader Contacts_GetBC44(int statusId,int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, int sourceType, EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Consultant: 
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_ExportContactAdvance"), statusId, branchId, userIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, sourceType);
                    
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_QLTVTSXuatContact_AddSourceType_Collaborator"), statusId, branchId, userIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, sourceType);
            }
            return null;
        }

        public override IDataReader Contacts_GetBC45(DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? registerFromDate, DateTime? registerToDate, int channelId, int templateAdsId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("pr_RPT_BAO_CAO_THONG_KE_MO_CRM30"), handoverFromDate, handoverToDate, registerFromDate, registerToDate, channelId, templateAdsId);
        }

        public override IDataReader Contacts_GetBC46(DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? registerFromDate, DateTime? registerToDate, int channelId, int statuscontact)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("pr_RPT_BAO_CAO_THONG_KE_MO_CRM30_by_channel_01"), handoverFromDate, handoverToDate, registerFromDate, registerToDate, channelId, statuscontact);
        }

        public override IDataReader Contacts_GetBC47(string name, string phone, string email, string userids, string listedprice, int sourcetype, DateTime? datewanttolearn, DateTime? handoverfromadvisor, DateTime? handovertoadvisor)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetHandoverAdvisorBC47"), name, phone, email, userids, listedprice, sourcetype, datewanttolearn, handoverfromadvisor, handovertoadvisor);
        }
        public override IDataReader Contacts_GetBC40(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetHotLine"), from, to);
        }
        public override IDataReader Contacts_GetChitiettralai(DateTime from)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetChitiettralai"), from);
        }

        public override IDataReader Contacts_GetForBC02(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetForL5BL2AndL8L2"), from, to);
        }
        public override IDataReader Contacts_GetBC03(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetMoAcceptance"), from, to);
        }

        public override IDataReader Contacts_GetBC20(DateTime hanoverDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetByHanoverDateConsultant"), hanoverDate);
        }

        public override IDataReader Contacts_GetBC43(DateTime? fromRegister, DateTime? toRegister, DateTime? fromHandover, DateTime? toHandover, string userIds, int statusType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("ContactGet_TicReport"), fromRegister, toRegister, fromHandover, toHandover, userIds, statusType);
        }
        public override IDataReader Contacts_GetBC06(DateTime from, DateTime to)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAllAcceptance"), from, to);
        }

        public override IDataReader Contacts_GetByMobile(string mobile)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetByMobile"), mobile);
        }

        public override IDataReader Contacts_GetInfoFeeEduByMobile(string mobile)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetInfoFeeEduByMobile"), mobile);
        }

        public override int Contacts_CountByChannel(int branchId, int sourceTypeId, int levelId, int channelId, DateTime from, DateTime to)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_CountByChannel"), branchId, sourceTypeId, levelId, channelId, from, to);
        }
        public override int Contacts_Count(string sourceTypeIds, string channelIds, string schoolIds, int levelId, int statusId, DateTime from, DateTime to)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Count"), sourceTypeIds, channelIds, schoolIds, levelId, statusId, from, to);
        }
        public override void Contacts_UpdateAcceptance(int contactId, int acceptStatus, int contactStatusId, int statusMapId, int levelId, int createdBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Acceptance"), contactId, acceptStatus, contactStatusId, statusMapId, levelId, createdBy);
        }

        public override int Contacts_Insert(string code, string fullname, DateTime? birth, string email, string address, int gender, int typeId, int channelId, int levelId, int collectorId, int branchId, int statusId, int statusMapId, DateTime? registeredDate, string notes, int? importId = null)
        {

            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Insert"), code, fullname, birth, email, address, gender, typeId, channelId, levelId, collectorId, branchId, statusId, statusMapId, registeredDate, notes, importId);
        }

        public override void Contacts_Update(int id, string code, string fullname, string address, DateTime? birthday, int gender, string email, string email2, string notes, DateTime? registeredDate, DateTime? createdDate, DateTime? importedDate, DateTime? handoverCollaboratorDate, DateTime? recoveryCollaboratorDate, DateTime? appointmentCollaboratorDate, DateTime? callCollaboratorDate, DateTime? handoverConsultantDate, DateTime? recoveryConsultantDate, DateTime? appointmentConsultantDate, DateTime? callConsultantDate, int collectorId, int typeId, int channelId, int campaindTpeId, int landingPageId, int templateAdsId, int searchKeywordId, int packageId, int levelId, int branchId, int statusId, int importId, int containerId, int productSellId, int productSoldId, int statusMapCollaboratorId, int statusCareCollaboratorId, int statusMapConsultantId, int statusCareConsultantId, int userImportId, int userErrorId, int userHandoverCollaboratorId, int userCollaboratorId, int userRecoveryCollaboratorId, int userHandoverConsultantId, int userConsultantId, int userRecoveryConsultantId, int draftCollaborator, int draftConsultant, string callInfoCollaborator, string callInfoConsultant, int handoverHistoryCollaboratorId, int handoverHistoryConsultantId, int callCount)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update"), id, code, fullname, address, birthday, gender, email, email2, notes, registeredDate, createdDate, importedDate, handoverCollaboratorDate, recoveryCollaboratorDate, appointmentCollaboratorDate, callCollaboratorDate, handoverConsultantDate, recoveryConsultantDate, appointmentConsultantDate, callConsultantDate, collectorId, typeId, channelId, campaindTpeId, landingPageId, templateAdsId, searchKeywordId, packageId, levelId, branchId, statusId, importId, containerId, productSellId, productSoldId, statusMapCollaboratorId, statusCareCollaboratorId, statusMapConsultantId, statusCareConsultantId, userImportId, userErrorId, userHandoverCollaboratorId, userCollaboratorId, userRecoveryCollaboratorId, userHandoverConsultantId, userConsultantId, userRecoveryConsultantId, draftCollaborator, draftConsultant, callInfoCollaborator, callInfoConsultant, handoverHistoryCollaboratorId, handoverHistoryConsultantId, callCount);
        }

        public override IDataReader Contacts_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetInfo"), id);
        }

        public override IDataReader Contacts_GetInfo_OldDatabase(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetInfo_OldDatabase"), id);
        }

        public override IDataReader Contacts_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll"));
        }

        public override IDataReader Contacts_GetAll_ForRedis()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_ForRedis"));
        }

        public override IDataReader Contacts_GetAllHandoverAdvisor(int userid, string name, string phone, string email, int statusHandoverAdvisor, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_Handover_Advisor"),userid,name,phone,email,statusHandoverAdvisor, pageIndex, pageSize);
        }
        public override IDataReader Contacts_GetAllSuccessHandoverAdvisor(int userid, string name, string phone, string email, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_Handover_Success_Advisor"), userid, name, phone, email, pageIndex, pageSize);
        }

        public override IDataReader Contacts_GetAllHandoverManagerAdvisor(int branchId, string name, string phone, string email, int statusHandoverAdvisor, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_Handover_Advisor_Manager"), branchId, name, phone, email, statusHandoverAdvisor, pageIndex, pageSize);
        }

        public override IDataReader Contacts_GetAllSuccessHandoverManagerAdvisor(int branchId, string name, string phone, string email, string userids, string listedprice, int sourcetype, DateTime? datewanttolearn, DateTime? handoverFadvisor, DateTime? handoverTadvisor, int packageFeeEdu, int appointmentType, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_Handover_Success_Advisor_Manager"), branchId, name, phone, email, userids, listedprice, sourcetype, datewanttolearn, handoverFadvisor, handoverTadvisor, packageFeeEdu, appointmentType, pageIndex, pageSize);
        }
        public override IDataReader Contacts_GetAllSuccessHandoverManagerAdvisor(int branchId, string name, string phone, string email, string userids, string listedprice, int sourcetype, DateTime? datewanttolearn, DateTime? handoverFadvisor, DateTime? handoverTadvisor, int packageFeeEdu, int appointmentType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_Handover_Success_Advisor_Manager_WithOutPage"), branchId, name, phone, email, userids, listedprice, sourcetype, datewanttolearn, handoverFadvisor, handoverTadvisor, packageFeeEdu, appointmentType);
        }

        public override IDataReader Contacts_GetAllSuccessHandoverManagerAdvisorL7(int branchId, string name, string phone, string email, string userids, string listedprice, int sourcetype, DateTime? datewanttolearn, DateTime? handoverFadvisor, DateTime? handoverTadvisor, int packageFeeEdu, int appointmentType, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetAll_Handover_Success_Advisor_ManagerL7"), branchId, name, phone, email, userids, listedprice, sourcetype, datewanttolearn, handoverFadvisor, handoverTadvisor, packageFeeEdu, appointmentType, pageIndex, pageSize);
        }
        public override IDataReader Contacts_GetForAcceptance(int printId, string pageCode, int levelId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetForAcceptance"), printId, pageCode, levelId, pageIndex, pageSize);
        }
        public override IDataReader Contacts_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Search"), keyword, pageIndex, pageSize);
        }
        public override IDataReader Contacts_Search(int collector, int channelId, int importId, int levelId, int sourceTypeId, int statusId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_SearchByCollectorChannelImport"), collector, channelId, importId, levelId, sourceTypeId, statusId, pageIndex, pageSize);
        }
        public override IDataReader Contacts_SearchDuplicateMo(int collector, int channelId, int importId, int levelId, int sourceTypeId, int branchId,DateTime? importDate, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_SearchDuplicateMo"), branchId, collector, channelId, importId, levelId, sourceTypeId, importDate, pageIndex, pageSize);
        }
        public override IDataReader Contacts_SearchDuplicateMo(int branchId, DateTime? importDate,int importId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_SearchDuplicateMo_Copy"), branchId, importDate, importId, pageIndex, pageSize);
        }
        public override IDataReader Contacts_SearchDuplicateMo(int branchId, DateTime? importDate, int importId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_SearchDuplicateMo_GetExcel"), branchId, importDate, importId);
        }
        public override IDataReader Contacts_SearchDistributedContacts(int collector, int channelId, int importId, int levelId, int sourceTypeId, int statusId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_SearchDistributedContacts"), collector, channelId, importId, levelId, sourceTypeId, statusId, pageIndex, pageSize);
        }
        public override IDataReader Contacts_Filter_Recovered(int statusId, string sourceTypes, string levels, int statusMapId, int statusCareId, int branchId, DateTime? recoveryFromDate, DateTime? recoveryToDate, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Recovered_Test"), statusId, sourceTypes, levels, statusMapId, statusCareId, branchId, recoveryFromDate, recoveryToDate, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Reuse(int statusId, int branchId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Reuse"), statusId, branchId, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Search(string mobile, string email, int branchId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search"), mobile, email, branchId, pageIndex, pageSize);
        }

        public override int Contacts_Filter_Count_Handover(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Count_Handover"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds);
        }

        public override int Contacts_Recovery_Distributor(string ids, int statusId, DateTime recoveryDate, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Recovery_Distributor"), ids, statusId, recoveryDate, createdBy);
        }

        public override int Contacts_Recovery_Distributor_All(int levelId, int sourceTypeId, string statusIds, string statusMapIds, DateTime? fromDate, DateTime? toDate, int branchId, int statusId, DateTime recoveryDate, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Recovery_Distributor_All"), levelId, sourceTypeId, statusIds, statusMapIds, fromDate, toDate, branchId, statusId, recoveryDate, createdBy);
        }

        public override int Contacts_ReuseAll(string statusIds, string sourceTypes, string levels, string educationLevels, int statusMapId, int statusCareId, string schools, string majors, int duplicate, int branchId, int statusId, DateTime? reuseDate, int sourceTypeId, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_ReuseAll"), statusIds, sourceTypes, levels, educationLevels, statusMapId, statusCareId, schools, majors, duplicate, branchId, statusId, reuseDate, sourceTypeId, createdBy);
        }

        public override int Contacts_Forward_Distributor(string ids, int branchId, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Forward_Distributor"), ids, branchId, createdBy);
        }

        public override int Contacts_ForwardAll_Distributor(int levelId, string statusIds, int statusMapId, int statusCareId, int branchId, string userIds, DateTime? fromDate, DateTime? toDate, int targetBranchId, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_ForwardAll_Distributor"), levelId, statusIds, statusMapId, statusCareId, branchId, userIds, fromDate, toDate, targetBranchId, createdBy);
        }


        public override void Contacts_Update_ClearDraft(EmployeeType type)
        {
            switch (type)
            {
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Clear_DraftCollaborator"));
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Clear_DraftConsultant"));
                    break;
            }
        }

        public override void Contacts_Update_Reuse(string ids, int statusId, DateTime reuseDate, int sourceTypeId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Reuse"), ids, statusId, reuseDate, sourceTypeId);
        }


        public override void Contacts_Update_Draft(string ids, int userId, int draft, EmployeeType type)
        {

            switch (type)
            {
                case EmployeeType.Collector:
                    break;
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_DraftCollaborator"), ids, userId, draft);
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_DraftConsultant"), ids, userId, draft);
                    break;
            }
        }

        public override void Contacts_Update_StatusMap(int contactId, int statusMapId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_StatusMap"), contactId, statusMapId);
        }

        public override int Contacts_Update_Handover(int branchId, int levelId, int statusId, int statusMapId, int statusCareId, int productSellId, DateTime handoverDate, DateTime appointmentDate, int createdBy, EmployeeType type)
        {
            switch (type)
            {
                case EmployeeType.Collaborator:
                    return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Update_HandoverCollaborator"), branchId, levelId, statusId, statusMapId, statusCareId, productSellId, handoverDate, appointmentDate, createdBy);
                case EmployeeType.Consultant:
                    return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Update_HandoverConsultant"), branchId, levelId, statusId, statusMapId, statusCareId, productSellId, handoverDate, appointmentDate, createdBy);
            }
            return 0;
        }

        public override void Contacts_Update_UserId(int id, int userId, int branchId, int createdBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, "Contacts_Update_UserId", id, userId, branchId, createdBy);
        }

        public override IDataReader Contacts_Filter_DuplicateTvts(int userId, string statusIds, int branchId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_DuplicateTvts"), userId, statusIds, branchId, pageIndex, pageSize);
        }
        public override IDataReader Contacts_Filter_Draft(int branchId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Draft"), branchId);
        }

        public override void Contacts_Update_Duplicate(int id, int sourceTypeId, int statusId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Duplicate"), id, sourceTypeId, statusId);
        }

        public override void Contacts_Update_FullName(int id, string fullName)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_FullName"), id, fullName);
        }

        public override void Contacts_Update_Level(int id, int levelId, int createdBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Level"), id, levelId, createdBy);
        }

        public override void Contacts_Accept_L1(int id, int statusMapId, int statusCareId, int statusMapDistributorId, DateTime acceptDateL1, string pageCode, int createdBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Accept_L1"), id, statusMapId, statusCareId, statusMapDistributorId, acceptDateL1, pageCode, createdBy);
        }

        public override IDataReader Contacts_Report_Appointment(int userId, int branchId, DateTime fromDate, DateTime toDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Report_Appointment"), userId, branchId, fromDate, toDate);
        }

        public override int Contacts_PrintAll(int branchId, DateTime fromDate, DateTime toDate, string channelIds, string channelAmounts, int maxRowPerPage, int createdBy, DateTime createdDate)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_PrintAll"), branchId, fromDate, toDate, channelIds, channelAmounts, maxRowPerPage, createdBy, createdDate);
        }

        public override int Contacts_Handover_Distributor(int branchId, int sourceTypeId, int statusId, DateTime fromDate, DateTime toDate, int typeCc, string statusMapIds, string channelIds, string schoolIds, int total, int userId, int statusIdNext, int statusMapDistributorId, DateTime handoverDistributorDate, int createdBy, DateTime createdDate)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Handover_Distributor"), branchId, sourceTypeId, statusId, fromDate, toDate, typeCc, statusMapIds, channelIds, schoolIds, total, userId, statusIdNext, statusMapDistributorId, handoverDistributorDate, createdBy, createdDate);
        }

        public override IDataReader Contacts_Filter_For_Collaborator(int branchId, int importId, int channelId, DateTime? startImportDate, DateTime? endImportDate, DateTime? startRegisterDate, DateTime? endRegisterDate, int connectStatus, int todayType, string statusMapIds, int campainId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_For_Collaborator"), branchId, importId, channelId, startImportDate, endImportDate, startRegisterDate, endRegisterDate, connectStatus, todayType, statusMapIds, campainId, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_For_CollaboratorL1(int branchId, int importId, int channelId, DateTime? startImportDate, DateTime? endImportDate, DateTime? startRegisterDate, DateTime? endRegisterDate, int connectStatus, int todayType, string statusMapIds, int campainId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_For_CollaboratorL1"), branchId, importId, channelId, startImportDate, endImportDate, startRegisterDate, endRegisterDate, connectStatus, todayType, statusMapIds, campainId, pageIndex, pageSize);
        }

        public override int Contacts_Handover_ToCampain(int total, int campainId, int branchId, int importId, int channelId, DateTime? startImportDate, DateTime? endImportDate, DateTime? startRegisterDate, DateTime? endRegisterDate, int connectStatus, int todayType, string statusMapIds, int createdBy, DateTime createdDate)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Handover_ToCampain"), total, campainId, branchId, importId, channelId, startImportDate, endImportDate, startRegisterDate, endRegisterDate, connectStatus, todayType, statusMapIds, createdBy);
        }

        public override IDataReader Contacts_Filter_Recovery_New(int statusId, string users, int statusMapId, int statusCareId, int day, int branchId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Recovery_New"), statusId.ToString(), users, statusMapId, statusCareId, day, branchId, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Recovery_Appointment(int statusId, string users, string levels, int statusMapId, int statusCareId, int day, int branchId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Recovery_Appointment"), statusId.ToString(), users, levels, statusMapId, statusCareId, day, branchId, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Recovery_Distributor(int levelId, int sourceTypeId, string statusIds, string statusMapIds, DateTime? fromDate, DateTime? toDate, int branchId, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Recovery_Distributor"), levelId, sourceTypeId, statusIds, statusMapIds, fromDate, toDate, branchId, pageIndex, pageSize);
        }

        public override void Contacts_Delete_ImportFile(string strContactIds)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Delete_ImportFile"), strContactIds);
        }

        public override IDataReader Contacst_GetContactsId(string strPhones)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetContactIDs"), strPhones);
        }

        public override int Contacts_Insert_Hv(string email, string email2, int levelId, DateTime? birth, int typeId, int gender, string fullname, int channelId, int branchId, int collectorId, int statusId, int userId, int statusMapId, int statusCareId, DateTime? registeredDate, DateTime? createdDate, DateTime? handoverDate, DateTime? appointmentDate, string mobile1, string mobile2, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Insert_Hv"), email, email2, levelId, birth, typeId, gender, fullname, channelId, branchId, collectorId, statusId, userId, statusMapId, statusCareId, registeredDate, createdDate, handoverDate, appointmentDate, mobile1, mobile2, createdBy);
        }

        public override IDataReader Contacts_GetForFilterTeam(int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int statusId, int page, int rows)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetForFilterTeam"), branchId, collectorId, channelId, importId, levelId, sourceTypeId, statusId, page, rows);
        }
        public override IDataReader Contacts_GetForImportTeam(int branchId, int collectorId, int channelId, int importId, int levelId, int sourceTypeId, int statusId, int page, int rows)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetForImportTeam"), branchId, collectorId, channelId, importId, levelId, sourceTypeId, statusId, page, rows);
        }
        public override IDataReader Contacts_Filter_CCL2_Plus(int branchId, string levelIds, int sourceTypeId, int page, int rows)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_CCL2_Plus"), branchId, levelIds, sourceTypeId, page, rows);
        }

        // Handover
        public override IDataReader Contacts_Filter_Handover(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, EmployeeType employeeType, int statusCareId, int statusMapId)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Handover_Collaborator"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, statusCareId, statusMapId);
                    }
                case EmployeeType.Consultant:
                    {
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Handover_Consultant"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, statusCareId, statusMapId);
                    }
                case EmployeeType.All:
                    {
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Handover"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts);
                    }
            }
            return null;
        }
        
        public override IDataReader Contacts_Filter_Handover_ForMOL(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, EmployeeType employeeType, int statusCareId, int statusMapId, DateTime? fromRegisterDate, DateTime? toRegisterDate)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Handover_Collaborator"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, statusCareId, statusMapId);
                    }
                case EmployeeType.Consultant:
                    {
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Handover_Consultant_ForMOL"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, statusCareId, statusMapId, fromRegisterDate, toRegisterDate);
                    }
                case EmployeeType.All:
                    {
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Handover"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts);
                    }
            }
            return null;
        }

        public override void Contacts_HandoverContactFast(string ids, EmployeeType employeeType, int targetUserId, int targetBranchId, DateTime handoverDate, int createdBy)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_HandoverFast_Collaborator"), ids, targetUserId, targetBranchId, handoverDate, createdBy);
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_HandoverFast_Consultant"), ids, targetUserId, targetBranchId, handoverDate, createdBy);
                    break;
            }
        }

        // Recovery
        public override int Contacts_Recovery_All(string userIds, string levelIds, int statusMapId, int statusCareId, int day, int branchId, int sourceType, TodayType todayType, EmployeeType employeeType, int userId, DateTime recoveryDate, int createdBy,DateTime? callToDate,DateTime? callFromDate,DateTime? handoverToDate,DateTime? handoverFromDate)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    return (int)SqlHelper.ExecuteScalar(ConnectionString,
                                GetFullyQualifiedName("Contacts_Update_Recovery_All_Collaborator"),
                                (int) StatusType.HandoverCollaborator, userIds, levelIds, statusMapId, statusCareId, day,
                                branchId, sourceType, (int) todayType, userId, (int) StatusType.RecoveryCollaborator, recoveryDate,
                                createdBy, callToDate, callFromDate, handoverToDate, handoverFromDate);
                case EmployeeType.Consultant:
                    return (int)SqlHelper.ExecuteScalar(ConnectionString,
                                GetFullyQualifiedName("Contacts_Update_Recovery_All_Consultant"),
                                (int) StatusType.HandoverConsultant, userIds, levelIds, statusMapId, statusCareId, day,
                                branchId, sourceType, (int) todayType, userId, (int) StatusType.RecoveryConsultant, recoveryDate,
                                createdBy, callToDate, callFromDate, handoverToDate, handoverFromDate);
            }
            return 0;
        }
        public override IDataReader Contacts_Filter_Recovery(string userIds, string levelIds, int statusMapId, int statusCareId, int branchId, TodayType todayType, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int sourceType, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Recovery_Collaborator"), (int)StatusType.HandoverCollaborator, userIds, levelIds, statusMapId, statusCareId, branchId, (int)todayType, handoverFromDate, handoverToDate, callFromDate, callToDate, sourceType, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Recovery_Consultant"), (int)StatusType.HandoverConsultant, userIds, levelIds, statusMapId, statusCareId, branchId, (int)todayType, handoverFromDate, handoverToDate, callFromDate, callToDate, sourceType, pageIndex, pageSize);
            }
            return null;
        }
        public override void Contacts_Update_Recovery(string ids, int userId, DateTime recoveryDate, EmployeeType employeeType, int createdBy)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Recovery_Collaborator"), (int)StatusType.RecoveryCollaborator, ids, userId, recoveryDate, createdBy);
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_Recovery_Consultant"), (int)StatusType.RecoveryConsultant, ids, userId, recoveryDate, createdBy);
                    break;
            }
        }

        // Khi import contact bi duplicate, se chon cac contact duplicate va chuyen ve kho MOL
        public override void Contacts_Update_RecoveryContainer_Duplicate(string ids, int userId, DateTime recoveryDate, EmployeeType employeeType, int createdBy)
        {
            switch (employeeType)
            {
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_RecoveryContainer_Duplicate_Consultant"), (int)StatusType.ContainerMOL, ids, userId, recoveryDate, createdBy);
                    break;
            }
        }

        // Change
        public override int Contacts_Update_Change_Container(int branchId, string typeIds, string levelIds, string importIds, string statusIds, string containerIds, string channelIds, string channelAmounts, int containerId, int productSellId, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Update_Change_Container"), branchId, typeIds, levelIds, importIds, statusIds, containerIds, channelIds, channelAmounts, containerId, productSellId, createdBy);
        }
        public override int Contacts_ForwardAll(int branchId, int statusMapId, int statusCareId, string userIds, DateTime fromDate, DateTime toDate, EmployeeType employeeType, int targetUserId, int targetBranchId, DateTime handoverDate, int createdBy)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        var statusIds = (int)StatusType.HandoverCollaborator + "," + (int)StatusType.RecoveryCollaborator;
                        return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Forward_All_Collaborator"), statusIds, statusMapId, statusCareId, branchId, userIds, fromDate, toDate, targetUserId, targetBranchId, handoverDate, createdBy);
                    }
                case EmployeeType.Consultant:
                    {
                        var statusIds = (int)StatusType.HandoverConsultant + "," + (int)StatusType.RecoveryConsultant;
                        return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Forward_All_Consultant"), statusIds, statusMapId, statusCareId, branchId, userIds, fromDate, toDate, targetUserId, targetBranchId, handoverDate, createdBy);
                    }
            }
            return 0;
        }
        public override int Contacts_ForwardAllContainer(DateTime importDate, int importId, int statusId, EmployeeType employeeType)
        {
            switch(employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Forward_All_Collaborator_Container"), importDate, importId, statusId);
                    }
                case EmployeeType.Consultant:
                    {
                        return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Forward_All_Consultant_Container"), importDate, importId, statusId);
                    }
            }
            return 0;
        }
        public override IDataReader Contacts_Filter_ForChange(int branchId, string levelids, int statusMapId, int statusCareId, string userIds, DateTime? fromDate, DateTime? toDate, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        var statusIds = (int)StatusType.HandoverCollaborator + "," + (int)StatusType.RecoveryCollaborator;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_ForChange_Collaborator"), statusIds, statusMapId, statusCareId, branchId, userIds, fromDate, toDate, pageIndex, pageSize);
                    }
                case EmployeeType.Consultant:
                    {
                        var statusIds = (int)StatusType.HandoverConsultant + "," + (int)StatusType.RecoveryConsultant;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_ForChange_Consultant"), levelids, statusIds, statusMapId, statusCareId, branchId, userIds, fromDate, toDate, pageIndex, pageSize);
                    }
            }
            return null;
        }
        public override void Contacts_Forward(string ids, EmployeeType employeeType, int targetUserId, int targetBranchId, DateTime handoverDate, int createdBy)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Forward_Collaborator"), ids, targetUserId, targetBranchId, handoverDate, createdBy);
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Forward_Consultant"), ids, targetUserId, targetBranchId, handoverDate, createdBy);
                    break;
            }
        }
        public override void Contacts_Forward_TVTSContainer(string ids, int statusId, EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Forward_Collaborator_Container"), ids, statusId);
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Forward_Consultant_Container"), ids, statusId);
                    break;
                case EmployeeType.ManagerContact:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Forward_Consultant_Container"), ids, statusId);
                    break;
            }
        }

        // Search
        public override IDataReader Contacts_Filter_Search_Manager(int branchId, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        var statusIds = (int)StatusType.HandoverCollaborator + "," + (int)StatusType.RecoveryCollaborator;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Manager_Collaborator"), branchId, statusIds, name, mobile, email, pageIndex, pageSize);
                    }
                case EmployeeType.Consultant:
                    {
                        var statusIds = (int)StatusType.HandoverConsultant;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Manager_Consultant"), branchId, statusIds, name, mobile, email, pageIndex, pageSize);
                    }
            }
            return null;
        }

        public override int Filter_Search_Count_Consultant(int branchId,string userIds, string name, string mobile, string email, EmployeeType employeeType)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString,GetFullyQualifiedName("Filter_Search_Count_Consultant"), branchId, userIds, name, mobile, email);
        }
        //Tim kiem nhanh de phan cho TVTS
        public override IDataReader Contact_Filter_Search_Handover(int branchId, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            var statusIds = (int)StatusType.New + "," + (int)StatusType.RecoveryConsultant + "," + (int)StatusType.AcceptCollaborator + "," + (int)StatusType.RecoveryCollaborator + "," + (int)StatusType.NewCollaborator + "," + (int)StatusType.DuplicateContact + "," + (int)StatusType.ContainerMOL;
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contact_Filter_Search_Handover"), branchId, statusIds, name, mobile, email, pageIndex, pageSize);
        }
        public override IDataReader Contacts_Filter_Search_Fast(int branchId, string userIds, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                {
                    const int statusIds = (int) StatusType.HandoverCollaborator;
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Fast_Collaborator"), branchId, userIds, statusIds, name, mobile, email, pageIndex, pageSize);
                }
                case EmployeeType.Consultant:
                {
                    const int statusIds = (int) StatusType.HandoverConsultant;
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Fast_Consultant_Turning"), branchId, userIds, statusIds, name, mobile, email, pageIndex, pageSize);
                }
                case EmployeeType.ManagerContact:
                {
                    userIds = string.Empty;
                    var statusIds = string.Empty;
                    //var statusIds = (int)StatusType.New;
                    branchId = 0; //thêm vào do yêu c?u qu?n lý contact du?c tìm ki?m trên toàn h? th?ng
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Fast_Consultant"), branchId, userIds, statusIds, name, mobile, email, pageIndex, pageSize);
                }
            }
            return null;
        }

        //Code lai chuc nang tim kiem contact cho TVTS
        public override IDataReader Contacts_Filter_Search_Fast_For_Consultant(int branchId, int userId, string name, string mobile, string email, int pageIndex, int pageSize)
        {
            const int statusId = (int)StatusType.HandoverConsultant;
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Fast_For_Consultant"), branchId, userId, statusId, name, mobile, email, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Search_Fast_For_Collaborator(int branchId, int userId, string name, string mobile, string email, int pageIndex, int pageSize)
        {
            const int statusId = (int)StatusType.HandoverCollaborator;
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Fast_For_Collaborator"), branchId, userId, statusId, name, mobile, email, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Search_Fast_For_Manager_Consultant(int branchId, int userId, string name, string mobile, string email, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            const int statusId = 0;
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Fast_For_Manager_Consultant"), branchId, userId, statusId, name, mobile, email, pageIndex, pageSize);
        }

        public override IDataReader Contacts_Filter_Search_Advance(int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        const int statusIds = (int)StatusType.HandoverCollaborator;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Advance_Collaborator"), branchId, userIds, statusIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, pageIndex, pageSize);
                    }
                case EmployeeType.Consultant:
                    {
                        const int statusIds = (int)StatusType.HandoverConsultant;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Advance_Consultant"), branchId, userIds, statusIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, pageIndex, pageSize);
                    }
            }
            return null;
        }

        public override IDataReader Contacts_Filter_Search_Advance_Manager(int branchId, string userIds, int statusMapId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    {
                        const int statusIds = (int)StatusType.HandoverCollaborator;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Advance_Collaborator"), branchId, userIds, statusIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, pageIndex, pageSize);
                    }
                case EmployeeType.Consultant:
                    {
                        const int statusIds = (int)StatusType.HandoverConsultant;
                        return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Search_Advance_Consultant_Mannager"), branchId, userIds, statusIds, statusMapId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, pageIndex, pageSize);
                    } 
            }
            return null;
        }
       
        // Delete
        public override void Contacts_Delete(int id, int contactId, int branchId, int userId, string phone, string email, int deletedBy, DateTime? deletedTime, string json, EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Delete_Collaborator"), id, contactId, branchId, userId, phone, email, deletedBy, deletedTime, json);
                    break;
                case EmployeeType.Consultant:
                    SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Delete_Consultant"), id, contactId, branchId, userId, phone, email, deletedBy, deletedTime, json);
                    break;
            }
        }
        public override IDataReader Contacts_Filter_Deleted(int branchId, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Deleted"), branchId, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Deleted"), branchId, pageIndex, pageSize);
            }
            return null;
        }
        
        //Forward
        public override IDataReader Contacts_Filter_Forward(int branchId, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Forward"), branchId, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Forward"), branchId, pageIndex, pageSize);
            }
            return null;
        }
        // Duplicate
        public override IDataReader Contacts_Filter_Duplicate(int userId, string statusIds, int branchId, EmployeeType employeeType, DateTime? dateDuplicate, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Duplicate"), userId, statusIds, branchId, (int)employeeType, dateDuplicate, pageIndex, pageSize);
        }

        //Duplicate Manager
        public override IDataReader Contacts_Filter_Duplicate_Manager(string statusIds, int branchId, int statusMapId, int statusCareId, string levelIds, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, int qualityId, int productSellId, string userIds, EmployeeType employeeType, DateTime? dateDuplicateN, int page, int rows)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Duplicate_Manager"), statusIds, branchId, statusMapId, statusCareId, levelIds, handoverFromDate, handoverToDate, callFromDate, callToDate, qualityId, productSellId, userIds, dateDuplicateN, page, rows);
        }

        public override int Contact_Filter_Dulicate_Count(int userId)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contact_Filter_Dulicate_Count"), userId);
        }

        // Create
        public override int Contacts_Insert_Tvts(string email, string email2, int levelId, DateTime? birth, int typeId, int gender, string fullname, string address, string notes, int branchId, int productSellId, int statusId, int userId, int statusMapId, int statusCareId, DateTime? registeredDate, DateTime? createdDate, DateTime? handoverDate, DateTime? appointmentDate, string mobile1, string mobile2, int createdBy, int campaindTpe)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Insert_Tvts"),
                email, email2, levelId, birth, typeId, gender, fullname, address, notes, branchId, productSellId, statusId,
                userId, statusMapId, statusCareId, registeredDate, createdDate, handoverDate, appointmentDate,
                mobile1, mobile2, createdBy, campaindTpe);
        }
        public override int Contacts_Insert_Hotline(int channelId, string email, string email2, int levelId, DateTime? birth, int typeId, int gender, string fullname, string address, string notes, int branchId, int productSellId, int statusId, int userId, int statusMapId, int statusCareId, DateTime? registeredDate, DateTime? createdDate, DateTime? handoverDate, DateTime? appointmentDate, string mobile1, string mobile2, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Insert_Hotline"),
                 channelId, email, email2, levelId, birth, typeId, gender, fullname, address, notes, branchId, productSellId, statusId,
                 userId, statusMapId, statusCareId, registeredDate, createdDate, handoverDate, appointmentDate,
                 mobile1, mobile2, createdBy);
        }
        public override int Contacts_Insert_Tmp(string code, string fullName, string address, DateTime? birthday, int gender, string email,
            string email2, string notes, DateTime? registeredDate, DateTime? createdDate, DateTime? importDate, int collectorId,
            int typeId, int channelId, int campaindTpeId, int landingPageId, int templateAdsId, int searchKeywordId,
            int packageId, int levelId, int branchId, int statusId, int importId, int containerId, int userImportId,
            int userErrorId, string mobile1, string mobile2, string mobile3, int createdBy)
        {
            return (int) SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contacts_Insert_Tmp"), code,
                fullName, address, birthday, gender, email, email2, notes, registeredDate, createdDate, importDate,
                collectorId, typeId, channelId, campaindTpeId, landingPageId, templateAdsId, searchKeywordId,
                packageId, levelId, branchId, statusId, importId, containerId, userImportId,
                userErrorId, mobile1, mobile2, mobile3, createdBy);
        }

        // Today
        public override IDataReader Contacts_Filter_Staticts_Today(string userIds, int branchId, DateTime dateTime, EmployeeType type)
        {
            switch (type)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Staticts_Today_Collaborator"), userIds, branchId, (int)StatusType.HandoverCollaborator, dateTime);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Staticts_Today_Consultant_Clone"), userIds, branchId, (int)StatusType.HandoverConsultant, dateTime);
            }
            return null;
        }
        public override IDataReader Contacts_Filter_Today_All(string userIds, DateTime fromDate, DateTime toDate, string levelIds,int branchId, EmployeeType type, int pageIndex, int pageSize)
        {
            switch (type)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_All_Collaborator"), userIds, fromDate, toDate, levelIds, branchId, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_All_Consultant"), userIds, fromDate, toDate, branchId, pageIndex, pageSize);
            }
            return null;
        }
        public override IDataReader Contacts_Filter_Today_NewAndAppointment(string userIds, DateTime fromDate, DateTime toDate, int branchId, EmployeeType type, int pageIndex, int pageSize)
        {
            switch (type)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_NewAndAppointment_Collaborator"), userIds, fromDate, toDate, branchId, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_NewAndAppointment_Consultant"), userIds, fromDate, toDate, branchId, pageIndex, pageSize);
            }
            return null;
        }
        public override IDataReader Contacts_Filter_Today_Appointment(string userIds, DateTime fromDate, DateTime toDate, string levels, int branchId, EmployeeType type, int pageIndex, int pageSize)
        {

            switch (type)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_Appointment_Collaborator"), userIds, fromDate, toDate, levels, branchId, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_Appointment_Consultant"), userIds, fromDate, toDate, levels, branchId, pageIndex, pageSize);
                case (EmployeeType)7 :
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_All_By_Level_Collaborator"), userIds, levels, branchId, pageIndex, pageSize); 
                default:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_All_By_Level_Consultant"), userIds, levels, branchId, pageIndex, pageSize);
            }
            return null;
        }
        public override IDataReader Contacts_Filter_Today_New(string userIds, DateTime fromDate, DateTime toDate, int branchId, EmployeeType type, int pageIndex, int pageSize)
        {

            switch (type)
            {
                case EmployeeType.Collaborator:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_New_Collaborator"), userIds, fromDate, toDate, branchId, pageIndex, pageSize);
                case EmployeeType.Consultant:
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_Filter_Today_New_Consultant"), userIds, fromDate, toDate, branchId, pageIndex, pageSize);
            }
            return null;
        }

        // Update
        public override void Contacts_Update_L1(int id, string fullname, DateTime? birthday, string email, string email2, string address, int gender, int productSellId, string notes, string mobile1, string mobile2, string mobile3)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Contacts_Update_L1"), id, fullname, birthday, email, email2, address, gender, productSellId, notes, mobile1, mobile2, mobile3);
        }

        // Duplicate
        public override int Contacts_IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Contacts_CheckDuplicate", mobile1, mobile2, tel, email, email2);
        }
        public override int Contacts_IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Contacts_CheckDuplicate_ByContactId", mobile1, mobile2, tel, email, email2, contactId);
        }
        public override IDataReader Contacts_GetInfoForAutoSale(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetInfoForAutoSale"), id);
        }
        public override IDataReader Contacst_GetInfoByMobile(string strPhones)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetInfoByMobile"), strPhones);
        }

        public override IDataReader Contacts_GetBC50(int branchId, int statusCareId, string levels, DateTime? handoverFromDate, DateTime? handoverToDate, DateTime? callFromDate, DateTime? callToDate, EmployeeType employeeType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contacts_GetCC3Report"), branchId, statusCareId, levels, handoverFromDate, handoverToDate, callFromDate, callToDate);
        }

        public override int Contacts_Update_Change_StatusContact(int branchId, string typeIds, string levelIds, string importIds, int statusId, int statusIdMOL, string channelIds, string channelAmounts, int createdBy, DateTime? fromRegisterDate, DateTime? toRegisterDate)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contact_Update_Change_StatusContact"), branchId, typeIds, levelIds, importIds, statusId, statusIdMOL, channelIds, channelAmounts, createdBy, fromRegisterDate, toRegisterDate);
        }

        public override IDataReader Contacts_GetCallHistory(string userids, DateTime? callFDate, DateTime? callTDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Contact_GetCallHistory"), userids, callFDate, callTDate);
        }

        public override void Contacts_Update_Change_Status(int contactId)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contact_Update_Change_Status"), contactId);
        }

        public override void Contacts_Update_Change_Container_TVTS(string ids, int statusId, int employeeTypeId)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Contact_Update_Change_Container_TVTS"), ids, statusId);
        }
    }
}

