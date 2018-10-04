using Newtonsoft.Json;
using System;
using System.Web.Services;
using TamoCRM.Core;
using TamoCRM.Domain.UserRole;
using TamoCRM.Domain.Logs;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.Logs;
using TamoCRM.Services.TestResults;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.UserRole;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Domain.TestResults;
using TamoCRM.Persitence;
using System.Collections.Generic;
using TamoCRM.Services.LogHandoverAdvisor;
using StackExchange.Redis;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Services.AppointmentInterview;
using TamoCRM.Domain.AppointmentInterviews;

namespace WSCasec
{
    /// <summary>
    /// Summary description for ServiceCasec
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class ServiceCasec : WebService
    {
        ConnectionMultiplexer Redis;
        public Result CheckInputUpdateCasecMark(UpdateCasecMark input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            var infoContact = ContactRepository.GetInfo(input.contactId);
            if (input.contactId == null || infoContact == null)
            {
                result.Code = 1;
                result.Description = "Contact có Id không tồn tại trong hệ thống";
                return result;
            }
            if (input.chinhTa < 0 || input.ngheHieu < 0 || input.nguPhap < 0 || input.tuVung < 0 || input.toeic < 0)
            {
                result.Code = 1;
                result.Description = "Có điểm bị trống";
                return result;
            }

            return result;
        }

        public Result CheckInputUpdateTopicaMark(UpdateTopicaMark input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            
            if (input.tuVung < 0 || input.dienDat < 0 || input.ngheHieu < 0 || input.chinhTa < 0)
            {
                result.Code = 1;
                result.Description = "Có điểm bị trống";
                return result;
            }

            return result;
        }
        public Result CheckInputUpdateLinkSB100(UpdateLinkSB100 input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            var infoContact = ContactRepository.GetInfo(input.contactId);
            if (input.contactId == null || infoContact == null)
            {
                result.Code = 1;
                result.Description = "Contact có Id không tồn tại trong hệ thống";
                return result;
            }
            if (String.IsNullOrEmpty(input.SB100))
            {
                result.Code = 1;
                result.Description = "Link SB100 bị NULL hoặc Empty";
                return result;
            }

            return result;
        }
        public Result CheckInputUpdateLinkSB100Topica(UpdateLinkSB100Topica input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            var infoContact = ContactRepository.GetInfo(input.contactId);
            if (input.contactId == null || infoContact == null)
            {
                result.Code = 1;
                result.Description = "Contact có Id không tồn tại trong hệ thống";
                return result;
            }
            if (String.IsNullOrEmpty(input.SB100Topica))
            {
                result.Code = 1;
                result.Description = "Link SB100 bị NULL hoặc Empty";
                return result;
            }

            return result;
        }
        public Result CheckInputUpdateInterviewMark(UpdateInterviewMark input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            var infoContact = ContactRepository.GetInfo(input.contactId);
            if (input.contactId == null || infoContact == null)
            {

                result.Code = 1;
                result.Description = "Contact có Id không tồn tại trong hệ thống";
                return result;
            }
            if (input.grammar < 0 || input.rythm < 0 || input.smooth < 0 || input.speaking < 0 || input.vocabulary < 0)
            {
                result.Code = 1;
                result.Description = "Có điểm bị trống";
                return result;
            }

            return result;
        }
        public Result CheckInputUpdateCasecCallInfo(UpdateCasecCallInfo input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            var infoContact = ContactRepository.GetInfo(input.ContactId);
            if (input.ContactId==null || infoContact == null)
            {
                result.Code = 1;
                result.Description = "Contact có Id không tồn tại trong hệ thống";
                return result;
            }
            if (input.AgentCode == null)
            {
                result.Code = 1;
                result.Description = "AgentCode bị NULL";
                return result;
            }
            if (input.LinkRecord==null)
            {
                result.Code = 1;
                result.Description = "Link Record bị NULL";
                return result;
            }
            if (input.MobilePhone == null)
            {
                result.Code = 1;
                result.Description = "Mobile bị NULL";
                return result;
            }
            if (input.StationId == null)
            {
                result.Code = 1;
                result.Description = "StationId bị NULL";
                return result;
            }
            return result;
        }
        public Result CheckInputUpdateStatusInterview(UpdateStatusInterview input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            if (input.ContactId < 0)
            {
                result.Code = 1;
                result.Description = "ContactID bị NULL";
                return result;
            }
            if (input.StatusInterviewId < 0)
            {
                result.Code = 1;
                result.Description = "trạng thái không xác định";
                return result;
            }
            return result;
        }
        public Result CheckInputChangeInterview(ChangeInterview input)
        {
            var result = new Result();
            result.Code = 0;

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            if (input.ContactId < 0)
            {
                result.Code = 1;
                result.Description = "ContactID bị NULL";
                return result;
            }
            if (input.StatusInterviewId < 0)
            {
                result.Code = 1;
                result.Description = "trạng thái không xác định";
                return result;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infomation"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateCasecMark(string infomation)
        {
            // Moi them 1 truong levelCasec
            //bắt sự kiện ngoại lệ như input NULL, contactID null hoặc trống, không có thông tin điểm

            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateCasecMark,
            };
            try
            {       
                var input = JsonConvert.DeserializeObject<UpdateCasecMark>(infomation);

                var result = CheckInputUpdateCasecMark(input);
                if (result.Code == 0)
                {
                    TestResultRepository.InsertCasecMark(input.contactId, input.casecAccount, input.tuVung, input.nguPhap, input.ngheHieu, input.chinhTa, input.toeic, input.levelCasec, input.tongDiem, input.ngayThi);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật điểm không thành công: " + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }         

        }
        [WebMethod]
        public string UpdateTopicaMark(string infomation)
        {
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateTopicaMark,
            };
            try
            {
                var input = JsonConvert.DeserializeObject<UpdateTopicaMark>(infomation);

                var f = input.tuVung + input.dienDat + input.ngheHieu + input.chinhTa;
                int levelCasec = 0;
                if (f >= 0 && f <= 219) levelCasec = 1;//basic 100                     
                else if (f >= 220 && f <= 239) levelCasec = 3;//basic 200
                else if (f >= 330 && f <= 449) levelCasec = 4;//basic 300
                else if (f >= 450 && f <= 549) levelCasec = 32;//inter 100
                else if (f >= 550 && f <= 654) levelCasec = 33;//inter 200
                else if (f >= 655 && f <= 759) levelCasec = 34;//inter 300
                else if (f >= 760 && f <= 849) levelCasec = 35;//adv 100
                else if (f >= 850 && f <= 909) levelCasec = 36;//adv 200
                else if (f >= 909 && f <= 1000) levelCasec = 37;//adv 300

                var result = CheckInputUpdateTopicaMark(input);

                if (result.Code == 0)
                {
                    TestResultRepository.InsertTopicaMark(input.userName, input.tuVung, input.dienDat, input.ngheHieu, input.chinhTa, levelCasec);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống " + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }

        [WebMethod]
        public string UpdateInterviewMark(string infomation)
        {
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateInterviewMark,
            };
            try
            {            
                var input = JsonConvert.DeserializeObject<UpdateInterviewMark>(infomation);
                var result = CheckInputUpdateInterviewMark(input);
                if (result.Code == 0)
                {
                    TestResultRepository.InsertInterviewMark(input.contactId, input.smooth, input.vocabulary, input.grammar, input.rythm, input.speaking, input.levelSpeaking, input.Notes, input.agent_user);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật điểm không thành công" + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }

        /// <summary>
        /// Cập nhật vào bảng TestResult
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="SB100"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateLinkSB100(string infomation)
        {
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateLinkSB100,
            };
            try
            {
                //bắt sự kiện ngoại lệ như input NULL, không có/NULL/Sai định dạng link SB100
                // Logs
                
                var input = JsonConvert.DeserializeObject<UpdateLinkSB100>(infomation);
                var result = CheckInputUpdateLinkSB100(input);
                if (result.Code == 0)
                {
                    TestResultRepository.UpdateLinkSB100(input.contactId, input.SB100);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật link không thành công" + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }
        [WebMethod]
        public string UpdateLinkSb100Topica(string infomation)
        {
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateLinkSB100Topica,
            };
            try
            {
                var input = JsonConvert.DeserializeObject<UpdateLinkSB100Topica>(infomation);
                var result = CheckInputUpdateLinkSB100Topica(input);
                if (result.Code == 0)
                {
                    TestResultRepository.UpdateLinkSB100Topica(input.contactId, input.SB100Topica);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật link không thành công" + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }
        [WebMethod]
        public string UpdateCallInfoCM(string infomation)
        {
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateCallInfoCM
            };
            try
            {
                //bắt sự kiện ngoại lệ như input NULL, không có thông tin
                
                var input = JsonConvert.DeserializeObject<UpdateCasecCallInfo>(infomation);
                var result = CheckInputUpdateCasecCallInfo(input);
                if (result.Code == 0)
                {
                    CallHistoryRepository.UpdateCallInfoCM(input.CallHistoryId, input.ContactId, input.AgentCode, input.StationId, input.MobilePhone, input.ResponseTime, input.StartTime, input.EndTime, input.RingTime, input.LinkRecord, input.CallCenterInfo, input.Duration, input.ErrorCode, input.ErrorDesc, input.StatusUpDate, input.CallType, input.CallTime);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật thông tin cuộc gọi không thành công" + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }

        [WebMethod]
        public string UpdateStatusInterview(string infomation)
        {
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateStatusInterview,
            };
            try
            {                
                var input = JsonConvert.DeserializeObject<UpdateStatusInterview>(infomation);
                var result = CheckInputUpdateStatusInterview(input);
                if (result.Code == 0)
                {
                    CallHistoryRepository.UpdateStatusInterview(input.ContactId, input.StatusInterviewId, input.Notes);
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật trạng thái phỏng vấn không thành công" + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }


        [WebMethod]
        public string ChangeInterview(string infomation)
        {
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateChangeInterview,
            };
            try
            {
                var input = JsonConvert.DeserializeObject<ChangeInterview>(infomation);
                var result = CheckInputChangeInterview(input);
                if (result.Code == 0)
                {
                    CallHistoryRepository.UpdateChangeInterview(input.ContactId, input.StatusInterviewId, input.AppointmentDate, input.Notes, input.TeacherTypeId, input.TimeSlotId);
                    //var entity = new AppointmentInterviewInfo
                    //                   {
                    //                      ContactId = input.ContactId,
                                          

                    //                   }
                    //AppointmentInterviewRepository.Create();
                    result.Code = 0;
                }
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Hệ thống hiện tại bị lỗi, cập nhật thay đổi trạng thái phỏng vấn không thành công" + infomation;
                var output = JsonConvert.SerializeObject(result);
                log.Description = result.Description + "_" + infomation;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return output;
            }
        }

        [WebMethod]
        public string GetInfoConsultant()
        {
            List<UserInfoTVST> result = new List<UserInfoTVST>();
            result = UserRepository.GetAll_TVTS();
            var output = JsonConvert.SerializeObject(result);
            return output;
        }

        [WebMethod]
        public bool SaveInfoHandover(string code, string note, int status)
        {
            try
            {
                LogHandoverAdvisorRepository.Insert_LogHandoverAdvisor(code, note, status);
                ContactLevelInfoRepository.UpdateHandoverAdvisorStatus(code,status);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public int IsDuplicateAutoSale(string mobile, string email) 
        {
            Redis = ConnectionMultiplexer.Connect("localhost");
            int retVal;
            if (!string.IsNullOrEmpty(mobile))
            {
                retVal = ConvertHelper.ToInt32(Redis.GetDatabase().StringGet(mobile));
                if (retVal != 0)
                {
                    Redis.Close();
                    return retVal;
                }
            }

            if (!string.IsNullOrEmpty(email))
            {
                retVal = ConvertHelper.ToInt32(Redis.GetDatabase().StringGet(email));
                if (retVal != 0)
                {
                    Redis.Close();
                    return retVal;
                }
            }
            Redis.Close();
            return 0;
        }

        [WebMethod]
        public Result checkDulicateCRMfromAutoSale(string mobile, string email) //Webservice để hệ thống autosale gọi check trùng contacts với CRM Native
        {
            Result result = new Result();
            try
            {
                ContactDulicateAutosale ctsInfo = new ContactDulicateAutosale();
                int duplicateId;
                mobile = Util.CleanAlphabetAndFirstZero(mobile);
                duplicateId = IsDuplicateAutoSale(mobile, email);
                if (duplicateId == 0)
                    duplicateId = ContactRepository.ContactIsDuplicate(mobile, string.Empty, string.Empty, email, string.Empty);
                if (duplicateId != 0)
                {
                    var c = ContactRepository.GetInfoForAutoSale(duplicateId);

                    ctsInfo.FullName = c.Fullname;
                    ctsInfo.Email = c.Email;
                    ctsInfo.Mobile = c.PhoneNumber;
                    ctsInfo.Level = c.LevelId;
                    ctsInfo.StatusCare = c.StatusCare;
                    ctsInfo.StatusMap = c.StatusMap;
                    ctsInfo.Consultant = c.UserName; //Tên TVTS
                    ctsInfo.Mobile = c.PhoneNumber;
                    ctsInfo.CallConsulantDate = c.CallConsultantDate;
                    ctsInfo.StatusCareId = c.StatusCareConsultantId;
                    
                    string output = JsonConvert.SerializeObject(ctsInfo);
                    result.Code = 1;
                    result.Description = output;
                    return result;
                }
                else
                {
                    result.Code = 2;
                    result.Description = "Không trùng!";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Code = 0;
                result.Description = "Lỗi Exception: " + ex.Message;
                return result;
            }
        }
    }
}
