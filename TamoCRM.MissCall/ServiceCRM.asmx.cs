using System;
using System.Text.RegularExpressions;
using System.Web.Services;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.Logs;
using TamoCRM.Domain.MissedCall;
using TamoCRM.Services.CallHistories;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Logs;
using TamoCRM.Services.MissedCall;
using TamoCRM.Web.Framework;

namespace TamoCRM.MissCall
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceCRM : WebService
    {
        [WebMethod]
        public ResultMissCall MissCall(string infomation)
        {
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.MissCall,
            };

            //" <dien_thoai></dien_thoai> <thoi_gian></thoi_gian> <call_id_truoc> </call_id_truoc> <agent_code></agent_code>. ";
            if (string.IsNullOrEmpty(infomation))
            {
                // Logs
                log.Status = 1;
                log.Description = "Thông tin cuộc gọi nhỡ rỗng";
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.NullOrEmpty,
                    Description = "Thông tin cuộc gọi nhỡ rỗng",
                };
            }

            var rg = new Regex("<dien_thoai>(?<phone>.*?)</dien_thoai>.*?" +
                               "<thoi_gian>(?<time>.*?)</thoi_gian>.*?" +
                               "<call_id_truoc>(?<call_id>.*?)</call_id_truoc>.*?" +
                               "<agent_code>(?<agent_code>.*?)</agent_code>");
            var mt = rg.Match(infomation);
            if (!mt.Success)
            {
                // Logs
                log.Status = 1;
                log.Description = "Thông tin cuộc gọi không đúng định dạng: " + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.NotFormat,
                    Description = "Thông tin cuộc gọi không đúng định dạng",
                };
            }

            var phone = mt.Groups["phone"].Value;
            if (string.IsNullOrEmpty(phone))
            {
                // Logs
                log.Status = 1;
                log.Description = "Số điện thoại rỗng:" + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Description = "Số điện thoại rỗng",
                    Code = (int)ErrorServiceType.NullOrEmpty,
                };
            }

            try
            {
                phone = phone.Trim();
                if (phone.StartsWith("0")) phone = phone.Substring(1);
                var contactEntity = ContactRepository.GetByMobile(phone);
                var entity = new MissedCallInfo
                {
                    PhoneNumber = phone,
                    CreatedTime = DateTime.Now,
                    OldCallId = mt.Groups["call_id"].Value,
                    Status = contactEntity == null ? -1 : 1,
                    AgentCode = mt.Groups["agent_code"].Value,
                    ContactId = contactEntity == null ? 0 : contactEntity.Id,
                    UserId = contactEntity == null ? 0 : contactEntity.UserConsultantId,
                    MissedCallTime = mt.Groups["time"].Value.ToDateTime("yyyyMMddHHmmss") ?? DateTime.Now,
                };
                entity.Id = MissedCallRepository.Create(entity);

                // Logs
                log.Status = 0;
                log.Description = infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.Success,
                    Description = "Cập nhật cuộc gọi nhỡ thành công",
                };
            }
            catch (Exception ex)
            {
                // Logs
                log.Status = 0;
                log.Description = ex + " --- input:" + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.Exception,
                    Description = "Cập nhật cuộc gọi nhỡ không thành công, lỗi hệ thống",
                };
            }
        }

        //[WebMethod]
        //public ResultMissCall MissCallDetail(string phone, string time, string callId, string agentCode)
        //{
        //    var xml = "<dien_thoai>{0}</dien_thoai><thoi_gian>{1}</thoi_gian><call_id_truoc>{2}</call_id_truoc><agent_code>{3}</agent_code>";
        //    xml = string.Format(xml, phone, time, callId, agentCode);
        //    return MissCall(xml);
        //}

        [WebMethod]
        public ResultMissCall Incoming(string infomation)
        {
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.Incoming,
            };

            if (string.IsNullOrEmpty(infomation))
            {
                // Logs
                log.Status = 1;
                log.Description = "Thông tin cuộc gọi đến rỗng";
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.NullOrEmpty,
                    Description = "Thông tin cuộc gọi đến rỗng",
                };
            }

            infomation = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><CallInfor>" + infomation + "</CallInfor>";
            var callInfo = ObjectHelper.CreateObject<CallInfor>(infomation);
            if (callInfo == null)
            {
                // Logs
                log.Status = 1;
                log.Description = "Thông tin cuộc gọi không đúng định dạng: " + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.NotFormat,
                    Description = "Thông tin cuộc gọi không đúng định dạng",
                };
            }

            try
            {
                callInfo.mobile_phone = callInfo.mobile_phone.Trim();
                if (callInfo.mobile_phone.StartsWith("0")) callInfo.mobile_phone = callInfo.mobile_phone.Substring(1);
                var contactInfo = ContactRepository.GetByMobile(callInfo.mobile_phone);
                var entity = new CallHistoryInfo
                                 {
                                     // Info call
                                     CallTimeLength = 0,
                                     CallTime = DateTime.Now,
                                     Status = callInfo.status,
                                     CallId = callInfo.call_id,
                                     CreatedDate = DateTime.Now,
                                     Duration = callInfo.duration,
                                     RingTime = callInfo.ringtime,
                                     CallCenterInfo = string.Empty,
                                     StationId = callInfo.station_id,
                                     AgentCode = callInfo.agent_code,
                                     ErrorCode = callInfo.error_code,
                                     ErrorDesc = callInfo.error_desc,
                                     CallType = (int)CallType.Incoming,
                                     MessageCode = callInfo.message_code,
                                     MobilePhone = callInfo.mobile_phone,
                                     UserLogType = (int)EmployeeType.Consultant,
                                     StatusUpdate = contactInfo == null ? -1 : 2,
                                     LinkRecord = callInfo.link_down_record.IsStringNullOrEmpty()
                                                      ? string.Empty
                                                      : callInfo.link_down_record.Replace("/var/spool/asterisk/monitor", StoreData.LinkRecordCRM(contactInfo == null ? 0 : contactInfo.BranchId)),
                                     EndTime = callInfo.end_time.ToDateTime("yyyyMMddHHmmss"),
                                     StartTime = callInfo.start_time.ToDateTime("yyyyMMddHHmmss"),
                                     ResponseTime = callInfo.datetime_response.ToDateTime("yyyyMMddHHmmss"),

                                     // Info contact
                                     ContactId = contactInfo == null ? 0 : contactInfo.Id,
                                     LevelId = contactInfo == null ? 0 : contactInfo.LevelId,
                                     CreatedBy = contactInfo == null ? 0 : contactInfo.UserConsultantId,
                                     StatusMapId = contactInfo == null ? 0 : contactInfo.StatusMapConsultantId,
                                     StatusCareId = contactInfo == null ? 0 : contactInfo.StatusCareConsultantId,
                                     RecallTime = contactInfo == null ? null : contactInfo.AppointmentConsultantDate,
                                 };
                CallHistoryRepository.Create(entity);

                // Logs
                log.Status = 0;
                log.Description = infomation + " ==> BranchId: " + (contactInfo == null ? 0 : contactInfo.BranchId);
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                           {
                               Code = (int)ErrorServiceType.Success,
                               Description = "Cập nhật cuộc gọi đến thành công",
                           };
            }
            catch (Exception ex)
            {
                // Logs
                log.Status = 0;
                log.Description = ex + " ---- input" + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new ResultMissCall
                {
                    Code = (int)ErrorServiceType.Exception,
                    Description = "Cập nhật cuộc gọi đến không thành công, lỗi hệ thống",
                };
            }
        }

        [WebMethod]
        public Result TCLCallInfoUpdate(string infomation)
        {
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.IncomingUpdate,
            };

            if (string.IsNullOrEmpty(infomation))
            {
                // Logs
                log.Status = 1;
                log.Description = "Thông tin cuộc gọi đến rỗng";
                TmpLogServiceRepository.Create(log);

                // Return
                return new Result
                {
                    Code = (int)ErrorServiceType.NullOrEmpty,
                    Description = "Thông tin cuộc gọi đến rỗng",
                };
            }

            infomation = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><CallInforUpdate>" + infomation + "</CallInforUpdate>";
            var callInfo = ObjectHelper.CreateObject<CallInforUpdate>(infomation);
            if (callInfo == null)
            {
                // Logs
                log.Status = 1;
                log.Description = "Thông tin cuộc gọi không đúng định dạng: " + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new Result
                {
                    Code = (int)ErrorServiceType.NotFormat,
                    Description = "Thông tin cuộc gọi không đúng định dạng",
                };
            }

            try
            {
                if (callInfo.station_id.IsStringNullOrEmpty())
                {
                    var arr = callInfo.agent_code.Split('/');
                    if (arr.Length > 1) callInfo.station_id = arr[1];
                }
                callInfo.mobile_phone = callInfo.mobile_phone.Trim();
                if (callInfo.mobile_phone.StartsWith("0")) callInfo.mobile_phone = callInfo.mobile_phone.Substring(1);

                var contactInfo = ContactRepository.GetByMobile(callInfo.mobile_phone);
                var callHitoryInfo = CallHistoryRepository.GetInfoIdentifyTcl(callInfo.call_id, (int)EmployeeType.Collaborator) ??
                                     new CallHistoryInfo();

                callHitoryInfo.CallTimeLength = 0;
                callHitoryInfo.CallTime = DateTime.Now;
                callHitoryInfo.Status = callInfo.status;
                callHitoryInfo.CallId = callInfo.call_id;
                callHitoryInfo.CreatedDate = DateTime.Now;
                callHitoryInfo.Duration = callInfo.duration;
                callHitoryInfo.RingTime = callInfo.ringtime;
                callHitoryInfo.CallCenterInfo = string.Empty;
                callHitoryInfo.StationId = callInfo.station_id;
                callHitoryInfo.AgentCode = callInfo.agent_code;
                callHitoryInfo.ErrorCode = callInfo.error_code;
                callHitoryInfo.ErrorDesc = callInfo.error_desc;
                callHitoryInfo.MessageCode = callInfo.message_code;
                callHitoryInfo.MobilePhone = callInfo.mobile_phone;
                callHitoryInfo.CallType = (int)CallType.IncomingUpdate;
                callHitoryInfo.StatusUpdate = contactInfo == null ? -1 : 2;
                callHitoryInfo.UserLogType = (int)EmployeeType.Collaborator;
                callHitoryInfo.LinkRecord = callInfo.link_down_record.IsStringNullOrEmpty()
                                        ? string.Empty
                                        : callInfo.link_down_record.Replace("/var/spool/asterisk/monitor/5678/", StoreData.LinkRecordTCL(contactInfo == null ? 0 : contactInfo.BranchId));
                callHitoryInfo.EndTime = callInfo.end_time.ToDateTime("yyyyMMddHHmmss");
                callHitoryInfo.StartTime = callInfo.start_time.ToDateTime("yyyyMMddHHmmss");
                callHitoryInfo.ResponseTime = callInfo.datetime_response.ToDateTime("yyyyMMddHHmmss");
                if (callHitoryInfo.ContactId.IsIntegerNull()) callHitoryInfo.ContactId = contactInfo == null ? 0 : contactInfo.Id;
                if (callHitoryInfo.LevelId.IsIntegerNull()) callHitoryInfo.LevelId = contactInfo == null ? 0 : contactInfo.LevelId;
                if (callHitoryInfo.CreatedBy.IsIntegerNull()) callHitoryInfo.CreatedBy = contactInfo == null ? 0 : contactInfo.UserConsultantId;
                if (callHitoryInfo.RecallTime == null) callHitoryInfo.RecallTime = contactInfo == null ? null : contactInfo.AppointmentConsultantDate;
                if (callHitoryInfo.StatusMapId.IsIntegerNull()) callHitoryInfo.StatusMapId = contactInfo == null ? 0 : contactInfo.StatusMapConsultantId;
                if (callHitoryInfo.StatusCareId.IsIntegerNull()) callHitoryInfo.StatusCareId = contactInfo == null ? 0 : contactInfo.StatusCareConsultantId;
                if (callHitoryInfo.CallCenterInfo.IsStringNullOrEmpty()) callHitoryInfo.CallCenterInfo = contactInfo == null ? string.Empty : contactInfo.CallInfoConsultant;
                CallHistoryRepository.Create(callHitoryInfo);

                // Logs
                log.Status = 0;
                log.Description = infomation + " ==> BranchId: " + (contactInfo == null ? 0 : contactInfo.BranchId);
                TmpLogServiceRepository.Create(log);

                // Return
                return new Result
                {
                    Code = (int)ErrorServiceType.Success,
                    Description = "Cập nhật cuộc gọi đến thành công",
                };
            }
            catch (Exception ex)
            {
                // Logs
                log.Status = 0;
                log.Description = ex + " ---- input" + infomation;
                TmpLogServiceRepository.Create(log);

                // Return
                return new Result
                {
                    Code = (int)ErrorServiceType.Exception,
                    Description = "Cập nhật cuộc gọi đến không thành công, lỗi hệ thống",
                };
            }
        }

        private static void UpdateLog(CallType type, string description)
        {
            try
            {
                var log = new TmpLogServiceInfo
                              {
                                  Time = DateTime.Now,
                                  CallType = (int)type,
                                  Description = description,
                              };
                TmpLogServiceRepository.Create(log);
            }
            catch
            {

            }
        }
    }
}