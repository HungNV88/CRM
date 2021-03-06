using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TamoCRM.Call;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.Logs;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.Logs;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Mvc.Areas.Admin.Models.CallHistories;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Error;
using TamoCRM.Web.Framework.Controllers;
using static TamoCRM.Services.Logs.TmpLogServiceRepository;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Domain.LogDashBoard;
using TamoCRM.Services.Report;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.CallHistories
{
    public class CallHistoryController : CustomApiController
    {
        [HttpGet]
        public int RepairCall()
        {
            var list = CallHistoryRepository.GetAllCallError() ?? new List<CallHistoryInfo>();
            return list.IsNullOrEmpty() ? 0 : RepairCall(list);
        }

        [HttpGet]
        public CallHistoryInfo Get(int id)
        {
            return CallHistoryRepository.GetInfo(id);
        }

        [HttpGet]
        public CallHistoryListModel GetCallError()
        {
            var listCallHistories = CallHistoryRepository.GetAllCallError() ?? new List<CallHistoryInfo>();
            var model = new CallHistoryListModel
            {
                Rows = listCallHistories,
            };
            return model;
        }

        [HttpGet]
        public CallHistoryInfo Call(int id, string mobile)
        {
            #region "Log Checkpoint - Begin Goi Den CallCenter 28/10/2016"
            int SessionLog = TmpJobReportRepository.GetSessionLog();
            DateTime TimeBegin = DateTime.Now;

            var logbegin = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "BEGIN_CALL_CONTACT_TVTS",
                ContactId = id,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            LogDashBoardRepository.CreateLogDashBoard(logbegin);
            #endregion

            if (string.IsNullOrEmpty(mobile)) return null;
            var user = UserRepository.GetCurrentUserInfo();
            

            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                CallType = (int)CallType.Incoming,
            };

            try
            {

                //var item = HelpUtils.Call(mobile, user.StationId, user.UserName, StoreData.CallCenterSoapBinding);
                
                var item = HelpUtils.CallNew(mobile, user.StationId, user.UserName, StoreData.LinkCallCenter);

                #region "Log Checkpoint - End Goi Den CallCenter 28/10/2016"
                DateTime TimeEnd = DateTime.Now;
                var logend = new LogDashBoard
                {
                    Date = DateTime.Now.Date,
                    Time = TimeEnd.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                    Name = "END_CALL_CONTACT_TVTS",
                    ContactId = id,
                    CreatedBy = UserContext.GetCurrentUser().UserID,
                    Runtime = "0",
                    Session = SessionLog,
                    CallId = item.call_id
                };
                LogDashBoardRepository.CreateLogDashBoard(logend);
                #endregion

                var entity = new CallHistoryInfo
                {
                    ContactId = id,
                    StatusMapId = 0,
                    StatusUpdate = 1,
                    StatusCareId = 0,
                    CallHistoryId = 0,
                    RecallTime = null,
                    CallTimeLength = 0,
                    Status = item.status,
                    CallId = item.call_id,
                    CreatedBy = user.UserID,
                    CallTime = DateTime.Now,
                    RingTime = item.ringtime,
                    Duration = item.duration,
                    CreatedDate = DateTime.Now,
                    AgentCode = item.agent_code,
                    ErrorCode = item.error_code,
                    ErrorDesc = item.error_desc,
                    StationId = item.station_id,
                    CallCenterInfo = string.Empty,
                    MessageCode = item.message_code,
                    MobilePhone = item.mobile_phone,
                    LinkRecord = item.link_down_record,
                    EndTime = item.end_time.ToDateTime("yyyyMMddHHmmss"),
                    StartTime = item.start_time.ToDateTime("yyyyMMddHHmmss"),
                    ResponseTime = item.datetime_response.ToDateTime("yyyyMMddHHmmss"),
                    LogCallCenter = item.log_callcenter

                };
                entity.CallHistoryId = CallHistoryRepository.Create(entity);

                // Logs
                log.Description = entity.LogCallCenter;
                log.Status = 1; // ko lỗi
                TmpLogServiceRepository.Create(log);

                // Return
                return entity;
            }
            catch (Exception ex)
            {
                // Logs
                log.Status = 0; //lỗi
                log.Description = "CallHistories/Call, Mobile: " + mobile + ": " + ex.Message.ToString();
                TmpLogServiceRepository.Create(log);

                // Return
                return null;
            }
        }

        [HttpGet]
        public CallHistoryListModel GetByContactId(int contactId, int userLogType)
        {
            var list = CallHistoryRepository.GetAllByContactId(contactId) ?? new List<CallHistoryInfo>();

            // Repear call
            var task = new Task(() => RepairCall(list.Where(c => !c.CallId.IsStringNullOrEmpty())
                                                     .Where(c => c.Duration.IsStringNullOrEmpty())));
            //var task = new Task(() => RepairCall(list));

            task.Start();

            list = list.Where(c => c.StatusMapId > 0).ToList();
            list = userLogType == (int)EmployeeType.Consultant
                       ? list.Where(c => c.UserLogType.IsIntegerNull() || c.UserLogType == userLogType).ToList()
                       : list.Where(c => c.UserLogType == userLogType).ToList();
            var model = new CallHistoryListModel
            {
                Rows = list,
            };
            var second = model.Rows.ToList().Sum(c => ConvertHelper.ToInt32(c.Duration));
            var strTime = second >= 60 ? (second / 60) + " phút, " + (second % 60) + " giây" : second + " giây";
            model.UserData = new List<string>
                                 {
                                     "Tổng số lượng gọi: " + model.Rows.Count(c => !string.IsNullOrEmpty(c.CallId)) + " cuộc",
                                     "Tổng thời gian gọi: " +  strTime,
                                 };
            return model;
        }

       

        [HttpGet]
        public CallHistoryListModel GetAllByContactId(int contactId, int employeeTypeId)
        {
            var list = CallHistoryRepository.GetAllByContactId(contactId, employeeTypeId) ?? new List<CallHistoryInfo>();

            // Repear call
            var task = new Task(() => RepairCall(list.Where(c => !c.CallId.IsStringNullOrEmpty())
                                                     .Where(c => c.Duration.IsStringNullOrEmpty())));
            //var task = new Task(() => RepairCall(list));
            task.Start();

            //list = list.Where(c => c.StatusMapId > 0).ToList();
            list = list.ToList();
            var model = new CallHistoryListModel
            {
                Rows = list,
            };
            var second = model.Rows.ToList().Sum(c => ConvertHelper.ToInt32(c.Duration));
            var strTime = second >= 60 ? (second / 60) + " phút, " + (second % 60) + " giây" : second + " giây";
            model.UserData = new List<string>
                                 {
                                     "Tổng số lượng gọi: " + model.Rows.Count(c => !string.IsNullOrEmpty(c.CallId)) + " cuộc",
                                     "Tổng thời gian gọi: " +  strTime,
                                 };
            return model;
        }

        [HttpGet]
        public CallHistoryListModel FilterHistoryForImporter(int userId, string callDate, string callDateEnd, string startTime, string endTime, int statusMapId, string levels, string educationLevels, string schools, string majors, int page, int rows)
        {
            DateTime? startDate = null, endDate = null;
            if (endTime.IsStringNullOrEmpty()) endTime = "23:59:59";
            if (startTime.IsStringNullOrEmpty()) startTime = "00:00:00";
            var dateCall = callDate.IsStringNullOrEmpty() ? null : callDate.ToDateTime();
            var dateCallEnd = callDateEnd.IsStringNullOrEmpty() ? null : callDateEnd.ToDateTime();
            if (dateCall != null) startDate = (dateCall.Value.ToString("dd/MM/yyyy") + " " + startTime).ToDateTime("dd/MM/yyyy HH:mm:ss");
            if (dateCallEnd != null) endDate = (dateCallEnd.Value.ToString("dd/MM/yyyy") + " " + endTime).ToDateTime("dd/MM/yyyy HH:mm:ss");

            int totalRecords;
            const int userLogType = (int)EmployeeType.Collaborator;
            var model = new CallHistoryListModel
            {
                Rows = CallHistoryRepository.FilterHistoryForImporter(userId, startDate, endDate, statusMapId, levels, educationLevels, schools, majors, userLogType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = new List<string>
                                 {
                                     totalRecords.ToString(),
                                 },
                Records = rows,
                Page = page,
            };
            return model;
        }

        [HttpGet]
        public CallHistoryListModel FilterHistory(int groupId, int userId, string handoverDate, string callDate, int statusMapId, int statusCareId, string levelIds, string mobilePhone, int employeeTypeId, int page, int rows)
        {
            string users;
            if (userId == 0)
            {
                if (groupId == 0) users = string.Empty;
                else users = StoreData.ListUser
                        .Where(c => c.GroupId == groupId)
                        .Select(c => c.UserID.ToString())
                        .Distinct().Aggregate((total, curent) => total + "," + curent);
            }
            else users = userId.ToString();
            var employeeType = (EmployeeType)employeeTypeId;
            var dateCall = callDate.IsStringNullOrEmpty() ? null : callDate.ToDateTime();
            var dateHandover = handoverDate.IsStringNullOrEmpty() ? null : handoverDate.ToDateTime();

            int totalRecords;
            var model = new CallHistoryListModel
            {
                Rows = CallHistoryRepository.FilterHistory(users, dateHandover, dateCall, statusMapId, statusCareId, levelIds, mobilePhone, employeeType, page, rows, out totalRecords),
                Total = (totalRecords / rows) + 1,
                UserData = new List<string>
                                 {
                                     totalRecords.ToString(),
                                 },
                Records = rows,
                Page = page,
            };
            return model;
        }

        private static int RepairCall(IEnumerable<CallHistoryInfo> list)
        {
            var count = 0;
            foreach (var entity in list)
            {
                // Repair call
                try
                {
                    if (entity.CallId != "KO_CO") {
                        StoreData.WsUpdateCallHistoryInfo(entity);
                        count += 1;
                    } 
                }
                catch
                {

                }
            }
            return count;
        }
        
        [HttpPost]
        public string GetAllByDateFromTo(FormDataCollection form)//(string dateTimeFrom, string dateTimeTo)
        {
            try
            {
                var dateFrom = TimeStampConvert.UnixTimeStampToDateTime(Double.Parse(form.Get("dateTimeFrom")));
                var dateTo = TimeStampConvert.UnixTimeStampToDateTime(Double.Parse(form.Get("dateTimeTo")));
                var result = CallHistoryRepository.GetAll_ByFromDate_ToDate(dateFrom, dateTo);

                foreach(var item in result)
                {
                    if((item.CreatedDate).HasValue)
                    {
                        item.CreatedDate_TimeStamp = TimeStampConvert.ConvertToUnixTime((DateTime)item.CreatedDate);
                    }
                    
                    if((item.StartTime).HasValue)
                    {
                        item.StartTime_TimeStamp = TimeStampConvert.ConvertToUnixTime((DateTime)item.StartTime);
                    }
                    
                    if((item.EndTime).HasValue)
                    {
                        item.EndTime_TimeStamp = TimeStampConvert.ConvertToUnixTime((DateTime)item.EndTime);
                    }
                }

                var output = JsonConvert.SerializeObject(result);

                return output;
            }
            catch(Exception ex)
            {
                Error error = new Error();
                error.Code = 0;
                error.Description = ex.Message.ToString();
                var outputError = JsonConvert.SerializeObject(error);
                return outputError;
            }
        }

    }
}

