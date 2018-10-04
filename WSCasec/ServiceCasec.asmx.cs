using Newtonsoft.Json;
using System;
using System.Web.Services;
using TamoCRM.Core;
using TamoCRM.Domain.Logs;
using TamoCRM.Services.CallHistories;
using TamoCRM.Services.TestResults;

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
        public Result CheckInputUpdateCasecMark(UpdateCasecMark input)
        {
            var result = new Result { Code = 0 };

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            if (input.contactId < 0)
            {
                result.Code = 1;
                result.Description = "ContactID bị NULL";
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
        public Result CheckInputUpdateLinkSB100(UpdateLinkSB100 input)
        {
            var result = new Result { Code = 0 };

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            if (input.contactId < 0)
            {
                result.Code = 1;
                result.Description = "ContactID bị NULL";
                return result;
            }
            if (input.SB100 == null)
            {
                result.Code = 1;
                result.Description = "Link SB100 bị NULL";
                return result;
            }


            return result;
        }
        public Result CheckInputUpdateInterviewMark(UpdateInterviewMark input)
        {
            var result = new Result { Code = 0 };

            if (input == null)
            {
                result.Code = 1;
                result.Description = "Thông tin đầu vào không đúng";
                return result;
            }
            if (input.contactId < 0)
            {
                result.Code = 1;
                result.Description = "ContactID bị NULL";
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
            var result = new Result { Code = 0 };

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
            if (input.AgentCode == null)
            {
                result.Code = 1;
                result.Description = "AgentCode bị NULL";
                return result;
            }
            if (input.LinkRecord == null)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infomation"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateCasecMark(string infomation)
        {
            //bắt sự kiện ngoại lệ như input NULL, contactID null hoặc trống, không có thông tin điểm

            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateCasecMark,
            };


            var input = JsonConvert.DeserializeObject<UpdateCasecMark>(infomation);

            var result = CheckInputUpdateCasecMark(input);
            if (result.Code == 0)
            {
                var info = TestResultRepository.TestHasMark(input.contactId);
                if (info.ContactId == 0)
                {
                    TestResultRepository.InsertCasecMark(input.contactId, input.tuVung, input.nguPhap, input.ngheHieu, input.chinhTa, input.toeic, input.levelCasec);
                }
                else
                {
                    TestResultRepository.UpdateCasecMark(input.contactId, input.tuVung, input.nguPhap, input.ngheHieu, input.chinhTa, input.toeic, input.levelCasec);
                }
                result.Code = 0;
            }
            var output = JsonConvert.SerializeObject(result);
            return output;

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
            var input = JsonConvert.DeserializeObject<UpdateInterviewMark>(infomation);
            var result = CheckInputUpdateInterviewMark(input);
            if (result.Code == 0)
            {
                var info = TestResultRepository.TestHasMark(input.contactId);
                if (info.ContactId == 0)
                {
                    TestResultRepository.InsertInterviewMark(input.contactId, input.smooth, input.vocabulary, input.grammar, input.rythm, input.speaking, info.LevelSpeakingId);
                }
                else
                {
                    TestResultRepository.UpdateInterviewMark(input.contactId, input.smooth, input.vocabulary, input.grammar, input.rythm, input.speaking, info.LevelSpeakingId);
                }
                result.Code = 0;
            }
            var output = JsonConvert.SerializeObject(result);
            return output;
        }

        /// <summary>
        /// Cập nhật vào bảng TestResult
        /// </summary>
        /// <param name="infomation"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateLinkSB100(string infomation)
        {

            //bắt sự kiện ngoại lệ như input NULL, không có/NULL/Sai định dạng link SB100
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateLinkSB100,
            };
            var input = JsonConvert.DeserializeObject<UpdateLinkSB100>(infomation);
            var result = CheckInputUpdateLinkSB100(input);
            if (result.Code == 0)
            {
                TestResultRepository.UpdateLinkSB100(input.contactId, input.SB100);
                result.Code = 0;
            }
            var output = JsonConvert.SerializeObject(result);
            return output;
        }

        [WebMethod]
        public string UpdateCallInfoCM(string infomation)
        {
            //bắt sự kiện ngoại lệ như input NULL, không có thông tin
            // Logs
            var log = new TmpLogServiceInfo
            {
                Time = DateTime.Now,
                Description = infomation,
                CallType = (int)CallType.UpdateInterviewInfoCM,
            };
            var input = JsonConvert.DeserializeObject<UpdateCasecCallInfo>(infomation);
            var result = CheckInputUpdateCasecCallInfo(input);
            if (result.Code == 0)
            {
                CallHistoryRepository.UpdateCallInfoCM(input.CallHistoryId, input.ContactId, input.AgentCode, input.StationId, input.MobilePhone, input.ResponseTime, input.StartTime, input.EndTime, input.RingTime, input.LinkRecord, input.CallCenterInfo, input.Duration, input.ErrorCode, input.ErrorDesc, input.StatusUpDate, input.CallType);
                result.Code = 0;
            }
            var output = JsonConvert.SerializeObject(result);
            return output;
        }
    }
}
