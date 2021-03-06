using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int CallHistories_Insert(int contactId, int statusMapId, int contactLevelId, DateTime? callTime, int callTimeLength, string callCenterInfo, DateTime? recallTime, int createdBy, DateTime? createdDate, string messageCode, string agentCode, string stationId, string mobilePhone, DateTime? responseTime, string callId, DateTime? startTime, DateTime? endTime, string duration, string ringTime, string linkRecord, string status, string errorCode, string errorDesc, int levelId, int statusUpdate, int callType, int userLogType)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("CallHistories_Insert_Clone"), contactId, statusMapId, contactLevelId, callTime, callTimeLength, callCenterInfo, recallTime, createdBy, createdDate, messageCode, agentCode, stationId, mobilePhone, responseTime, callId, startTime, endTime, duration, ringTime, linkRecord, status, errorCode, errorDesc, levelId, statusUpdate, callType, userLogType);
        }

        public override void CallHistories_Update(int callHistoryId, int contactId, int statusMapId, int contactLevelId, DateTime? callTime, int callTimeLength, string callCenterInfo, DateTime? recallTime, int createdBy, DateTime? createdDate, string messageCode, string agentCode, string stationId, string mobilePhone, DateTime? responseTime, string callId, DateTime? startTime, DateTime? endTime, string duration, string ringTime, string linkRecord, string status, string errorCode, string errorDesc, int levelId, int statusUpdate, int callType, int userLogType)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("CallHistories_Update"), callHistoryId, contactId, statusMapId, contactLevelId, callTime, callTimeLength, callCenterInfo, recallTime, createdBy, createdDate, messageCode, agentCode, stationId, mobilePhone, responseTime, callId, startTime, endTime, duration, ringTime, linkRecord, status, errorCode, errorDesc, levelId, statusUpdate, callType, userLogType);
        }

        public override void CallHistories_RepairCall(int callHistoryId, string mobilePhone, string messageCode, string agentCode, string stationId, DateTime? responseTime, string callId, DateTime? startTime, DateTime? endTime, string duration, string ringTime, string linkRecord, string status, string errorCode, string errorDesc)
        {
            SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("CallHistories_RepairCall"), callHistoryId, mobilePhone, messageCode, agentCode, stationId, responseTime, callId, startTime, endTime, duration, ringTime, linkRecord, status, errorCode, errorDesc);
        }

        public override void CallHistories_UpdateCallInfoCM(int CallHistoryId, int ContactId, string AgentCode, string StationId, string MobilePhone, DateTime ResponseTime, DateTime StartTime, DateTime EndTime, int  RingTime, string LinkRecord, string CallCenterInfo, string Duration, string ErrorCode, string ErrorDesc, int StatusUpdate, int CallType, DateTime CallTime)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("TestResult_UpdateCasecCallInfo"), CallHistoryId, ContactId, AgentCode, StationId, MobilePhone, ResponseTime, StartTime, EndTime, RingTime, LinkRecord, CallCenterInfo, Duration, ErrorCode, ErrorDesc, StatusUpdate, CallType, CallTime);
        }

        public override void CallHistories_UpdateStatusInterview(int ContactId, int StatusInterviewId, string Notes)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Appointmentinterview_UpdateStatusInterview"), ContactId, StatusInterviewId, Notes);
        }
        public override void CallHistories_UpdateChangeInterview(int ContactId, int StatusInterviewId, DateTime AppointmentDate, string Notes, int TeacherTypeId, int TimeSlotId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Appointmentinterview_UpdateChangeInterview"), ContactId, StatusInterviewId, AppointmentDate, Notes, TeacherTypeId, TimeSlotId);
        }

        public override IDataReader CallHistories_GetInfo(int callHistoryId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetInfo"), callHistoryId);
        }

        public override IDataReader CallHistories_GetInfo_ByCallId(string callId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetInfo_ByCallId"), callId);
        }

        public override IDataReader CallHistories_GetAll_ByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAll_ByContactId"), contactId);
        }

        public override IDataReader CallHistories_GetAll_ByContactId(int contactId, int employeeTypeId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAll_ByContactId_and_EmployeeTypeId"), contactId, employeeTypeId);
        }

        public override IDataReader CallHistories_GetAll_ByContactId_Xml(string xml)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAll_ByContactId_Xml"), xml);
        }

        public override IDataReader CallHistories_GetAll_Identify_Tcl(string callId, int userLogType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAll_Identify_Tcl"), callId, userLogType);
        }

        public override IDataReader CallHistories_GetAll_Identify(string callId, string stationId, string agentCode)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetInfo_Identify"), callId, stationId, agentCode);
        }

        public override IDataReader CallHistories_GetAll_Call_Error()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAll_Call_Error"));
        }

        public override void CallHistories_Update_CallInfo(int callHistoryId, int statusMapId, int contactLevelId, string callCenterInfo, DateTime recallTime)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CallHistories_Update_CallInfo"), callHistoryId, statusMapId, contactLevelId, callCenterInfo, recallTime);
        }

        public override IDataReader CallHistories_Filter_History(string userIds, DateTime? handoverDate, DateTime? callDate, int statusMapId, int statusCareId, string levelIds,string mobilePhone, EmployeeType employeeType, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_Filter_History"), userIds, handoverDate, callDate, statusMapId, statusCareId, levelIds, mobilePhone, (int)employeeType, pageIndex, pageSize);
        }

        public override IDataReader CallHistories_Filter_History_ForImporter(int userId, DateTime? startDate, DateTime? endDate, int statusMapId, string levels, string educationLevels, string schools, string majors, int userLogType, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_Filter_History_ForImporter"), userId, startDate, endDate, statusMapId, levels, educationLevels, schools, majors, userLogType, pageIndex, pageSize);
        }
        
        public override IDataReader GetAllCallInfoNotLinkRecords()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAllNotLinkRecord"));
        }

        public override IDataReader GetAllBy_FromDate_ToDate(DateTime fromDate, DateTime toDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("CallHistories_GetAll_ByFromDate_ToDate"), fromDate, toDate);
        }
    }
}

