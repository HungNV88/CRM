using System;
using System.Collections.Generic;
using System.Data;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.CallHistories
{
    public class CallHistoryRepository
    {        
        public static void UpdateCallInfoCM(int CallHistoryId, int ContactId, string AgentCode, string StationId, string MobilePhone, DateTime ResponseTime, DateTime StartTime, DateTime EndTime, int RingTime, string LinkRecord, string CallCenterInfo,string Duration, string ErrorCode, string ErrorDesc,int StatusUpdate, int CallType, DateTime CallTime)
        {
            DataProvider.Instance().CallHistories_UpdateCallInfoCM(CallHistoryId, ContactId, AgentCode, StationId, MobilePhone, ResponseTime, StartTime, EndTime, RingTime, LinkRecord, CallCenterInfo, Duration, ErrorCode, ErrorDesc,StatusUpdate, CallType, CallTime);
        }
        public static void UpdateStatusInterview(int ContactId, int StatusInterviewId, string Notes)
        {
            DataProvider.Instance().CallHistories_UpdateStatusInterview(ContactId, StatusInterviewId, Notes);
        }        
        public static void UpdateChangeInterview(int ContactId, int StatusInterviewId, DateTime AppointmentDate, string Notes, int TeacherTypeId, int TimeSlotId)
        {
            DataProvider.Instance().CallHistories_UpdateChangeInterview(ContactId, StatusInterviewId, AppointmentDate, Notes, TeacherTypeId, TimeSlotId);
        }
        public static int Create(CallHistoryInfo info)
        {
            if (info.MobilePhone.StartsWith("0")) info.MobilePhone = info.MobilePhone.Substring(1);
            return DataProvider.Instance().CallHistories_Insert(info.ContactId, info.StatusCareId, info.StatusMapId, info.CallTime, info.CallTimeLength, info.CallCenterInfo, info.RecallTime, info.CreatedBy, info.CreatedDate, info.MessageCode, info.AgentCode, info.StationId, info.MobilePhone, info.ResponseTime, info.CallId, info.StartTime, info.EndTime, info.Duration, info.RingTime, info.LinkRecord, info.Status, info.ErrorCode, info.ErrorDesc, info.LevelId, info.StatusUpdate, info.CallType, info.UserLogType);
        }
        public static List<CallHistoryInfo> GetAllCallError()
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetAll_Call_Error());
        }
        public static CallHistoryInfo GetInfo(int callHistoryId)
        {
            return ObjectHelper.FillObject<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetInfo(callHistoryId));
        }
        public static List<CallHistoryInfo> GetInfoByCallId(string callId)
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetInfo_ByCallId(callId));
        }

        public static List<CallHistoryInfo> GetAllByContactId(int contactId)
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetAll_ByContactId(contactId));
        }
        public static List<CallHistoryInfo> GetAllByContactId(int contactId, int employeeTypeId)
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetAll_ByContactId(contactId, employeeTypeId));
        }
        public static List<CallHistoryInfo> GetAllByContactId_Xml(string xml)
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetAll_ByContactId_Xml(xml));
        }
        public static CallHistoryInfo GetInfoIdentifyTcl(string callId, int userLogType)
        {
            return ObjectHelper.FillObject<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetAll_Identify_Tcl(callId, userLogType));
        }
        public static CallHistoryInfo GetInfoIdentify(string callId, string stationId, string agentCode)
        {
            return ObjectHelper.FillObject<CallHistoryInfo>(DataProvider.Instance().CallHistories_GetAll_Identify(callId, stationId, agentCode));
        }

        public static List<CallHistoryInfo> FilterHistory(string users, DateTime? handoverDate, DateTime? callDate, int statusMapId, int statusCareId, string levelIds, string mobilePhone, EmployeeType employeeType, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillCollection(DataProvider.Instance().CallHistories_Filter_History(users, handoverDate, callDate, statusMapId, statusCareId, levelIds, mobilePhone, employeeType, pageIndex, pageSize), out totalRecord);
        }
        public static List<CallHistoryInfo> FilterHistoryForImporter(int userId, DateTime? startDate, DateTime? endDate, int statusMapId, string levels, string educationLevels, string schools, string majors, int userLogType, int pageIndex, int pageSize, out int totalRecord)
        {
            if (levels == null) levels = string.Empty;
            if (majors == null) majors = string.Empty;
            if (schools == null) schools = string.Empty;
            if (educationLevels == null) educationLevels = string.Empty;
            return FillCollection(DataProvider.Instance().CallHistories_Filter_History_ForImporter(userId, startDate, endDate, statusMapId, levels, educationLevels, schools, majors, userLogType, pageIndex, pageSize), out totalRecord);
        }
        private static List<CallHistoryInfo> FillCollection(IDataReader reader, out int totalRecords)
        {
            List<CallHistoryInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<CallHistoryInfo>(reader, false);

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

        public static void RepairCall(CallHistoryInfo info)
        {
            if (info.MobilePhone.StartsWith("0")) info.MobilePhone = info.MobilePhone.Substring(1);
            DataProvider.Instance().CallHistories_RepairCall(info.CallHistoryId, info.MobilePhone, info.MessageCode, info.AgentCode, info.StationId, info.ResponseTime, info.CallId, info.StartTime, info.EndTime, info.Duration, info.RingTime, info.LinkRecord, info.Status, info.ErrorCode, info.ErrorDesc);
        }

        //Ham get tat ca cac callinfo cua cuoc goi ko co file ghi am
        public static List<CallHistoryInfo> GetAllCallInfoNotLinkRecords()
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().GetAllCallInfoNotLinkRecords());
        }

        public static List<CallHistoryInfo> GetAll_ByFromDate_ToDate(DateTime fromDate, DateTime toDate)
        {
            return ObjectHelper.FillCollection<CallHistoryInfo>(DataProvider.Instance().GetAllBy_FromDate_ToDate(fromDate, toDate));
        }
    }        
}
