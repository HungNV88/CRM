using System;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void CallHistories_UpdateCallInfoCM(int CallHistoryId, int ContactId, string AgentCode, string StationId, string MobilePhone, DateTime ResponseTime, DateTime StartTime, DateTime EndTime, int RingTime, string LinkRecord, string CallCenterInfo, string Duration, string ErrorCode, string ErrorDesc, int StatusUpdate, int CallType, DateTime CallTime);
        public abstract void CallHistories_UpdateStatusInterview(int ContactId, int StatusInterviewId, string Notes);
        public abstract void CallHistories_UpdateChangeInterview(int ContactId, int StatusInterviewId, DateTime AppointmentDate, string Notes, int TeacherTypeId, int TimeSlotId);
        public abstract IDataReader CallHistories_GetAll_Call_Error();
        public abstract IDataReader CallHistories_GetInfo(int callHistoryId);
        public abstract IDataReader CallHistories_GetInfo_ByCallId(string callId);
        public abstract IDataReader CallHistories_GetAll_ByContactId(int contactId);
        public abstract IDataReader CallHistories_GetAll_ByContactId(int contactId, int employeeTypeId);
        public abstract IDataReader CallHistories_GetAll_ByContactId_Xml(string xml);
        public abstract IDataReader CallHistories_GetAll_Identify_Tcl(string callId, int userLogType);
        public abstract IDataReader CallHistories_GetAll_Identify(string callId, string stationId, string agentCode);
        public abstract void CallHistories_Update_CallInfo(int callHistoryId, int statusMapId, int contactLevelId, string callCenterInfo, DateTime recallTime);
        public abstract IDataReader CallHistories_Filter_History(string users, DateTime? handoverDate, DateTime? callDate, int statusMapId, int statusCareId, string levelIds, string mobilePhone, EmployeeType employeeType, int pageIndex, int pageSize);
        public abstract IDataReader CallHistories_Filter_History_ForImporter(int userId, DateTime? startDate, DateTime? endDate, int statusMapId, string levels, string educationLevels, string schools, string majors, int userLogType, int pageIndex, int pageSize);
        public abstract int CallHistories_Insert(int contactId, int statusMapId, int contactLevelId, DateTime? callTime, int callTimeLength, string callCenterInfo, DateTime? recallTime, int createdBy, DateTime? createdDate, string messageCode, string agentCode, string stationId, string mobilePhone, DateTime? responseTime, string callId, DateTime? startTime, DateTime? endTime, string duration, string ringTime, string linkRecord, string status, string errorCode, string errorDesc, int levelId, int statusUpdate, int callType, int userLogType);
        public abstract void CallHistories_Update(int callHistoryId, int contactId, int statusMapId, int contactLevelId, DateTime? callTime, int callTimeLength, string callCenterInfo, DateTime? recallTime, int createdBy, DateTime? createdDate, string messageCode, string agentCode, string stationId, string mobilePhone, DateTime? responseTime, string callId, DateTime? startTime, DateTime? endTime, string duration, string ringTime, string linkRecord, string status, string errorCode, string errorDesc, int levelId, int statusUpdate, int callType, int userLogType);
        public abstract void CallHistories_RepairCall(int callHistoryId, string mobilePhone, string messageCode, string agentCode, string stationId, DateTime? responseTime, string callId, DateTime? startTime, DateTime? endTime, string duration, string ringTime, string linkRecord, string status, string errorCode, string errorDesc);
        public abstract IDataReader GetAllCallInfoNotLinkRecords();

        // Ham get toan bo cuoc goi tu ngay den ngay
        public abstract IDataReader GetAllBy_FromDate_ToDate(DateTime fromDate, DateTime toDate);

    }
}

