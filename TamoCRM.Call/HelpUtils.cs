using System;
using System.ServiceModel;
using Newtonsoft.Json;
using TamoCRM.Call.WsAutoDialService;
using TamoCRM.Call.WsOfCasec;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Logs;
using TamoCRM.Services.Logs;
using TamoCRM.Domain.AppointmentInterviews;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.UserRole;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Phones;
using TamoCRM.Services.AppointmentInterview;
using TamoCRM.Domain.TopicaAccounts;
using TamoCRM.Services.TestResults;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Net;
using System.IO;

namespace TamoCRM.Call
{
    public class HelpUtils
    {
        /// <summary>
        /// Hàm này thực hiện việc call contact; trả về thông tin cuộc gọi: call_id, error code
        /// </summary>
        /// <param name="mobile">Gọi đến số điện thoại nào</param>
        /// <param name="stationId">Dùng mã máy nào để gọi</param>
        /// <param name="agencode">Agencode gọi. Thường dùng là username của người gọi</param>
        /// <param name="link">Địa chỉ webservice của callcenter (có thể có hoặc không)</param>
        /// <returns>Đối tượng lưu thông tin cuộc gọi vừa tạo. 
        /// Call_Id đc dùng để lấy lại thông tin sau khi gọi xong. 
        /// Kiểm tra error_code để biết cuộc gọi có lỗi hay ko?(=1 là lỗi)</returns>
        public static CallInfor Call(string mobile, string stationId, string agencode, string link = default(string))
        {
            const string v_str_message_code = "003";
            var v_dat_datetime_request = DateTime.Now.ToLongTimeString();
            if (!mobile.StartsWith("0")) mobile = "0" + mobile;
            var v_str_input_string = "<agent_code>" + agencode + "</agent_code>"
                                        + "<mobile_phone>" + mobile + "</mobile_phone>"
                                        + "<station_id>" + stationId + "</station_id>"
                                        + "<datetime_request>" + v_dat_datetime_request + "</datetime_request>"
                                        + "<message_code>" + v_str_message_code + "</message_code>";

            var ws = link.IsStringNullOrEmpty()
                ? new WsCallCenterService.CallCenterClient("CallCenter")
                : new WsCallCenterService.CallCenterClient("CallCenter", new EndpointAddress(link));
            var v_str_result = ws.submit(v_str_input_string);
            var v_obj_infor = JsonConvert.DeserializeObject<CLichSuCuocGoi>(v_str_result);

            v_obj_infor.data.log_callcenter = v_str_result.ToString(); //Them ngay 24/06/2016 luu lai log goi callcenter de hien thi tren trang Edit.cshtml

            return v_obj_infor.data;
        }

        public static CallInfor CallNew(string mobile, string stationId, string agencode, string link = default(string))
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string read = string.Empty;
            string readStr = string.Empty;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(link + "rabdial?message_code=003&station_id=" + stationId + "&mobile_phone=" + mobile + "&agent_code=" + agencode);
                request.KeepAlive = true;
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                read = reader.ReadToEnd();
                
            }
            catch { }
            finally
            {
                if (response != null)
                    response.Dispose();
                if (request != null)
                    request.Abort();
            }
            if (read != null && !string.Empty.Equals(read))
            {
                var v_obj_infor = JsonConvert.DeserializeObject<CallInfor>(read);
                return v_obj_infor;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hàm này dùng để get thông tin về cuộc gọi
        /// </summary>
        /// <param name="callId">Call id dùng để get thông tin cuộc gọi</param>
        /// <param name="mobile">Số điện thọai vừa gọi</param>
        /// <param name="stationId">station id vừa call</param>
        /// <param name="agencode">Agen code thường để là username</param>
        /// <param name="link">Địa chỉ webservice của callcenter (có thể có hoặc không)</param>
        /// <returns>Đối tượng lưu toàn bộ thông tin cuộc gọi.
        /// Kiểm tra error_code để biết cuộc gọi có lỗi hay ko? (=1 là lỗi)</returns>
        public static CallInfor GetCall(string callId, string mobile, string stationId, string agencode, string link = default(string))
        {
            const string v_str_message_code = "004";
            var v_dat_datetime_request = DateTime.Now.ToLongTimeString();
            if (!mobile.StartsWith("0")) mobile = "0" + mobile;

            var v_str_input_string = "<agent_code>" + agencode + "</agent_code>"
                                        + "<mobile_phone>" + mobile + "</mobile_phone>"
                                        + "<station_id>" + stationId + "</station_id>"
                                        + "<datetime_request>" + v_dat_datetime_request + "</datetime_request>"
                                        + "<message_code>" + v_str_message_code + "</message_code>"
                                        + "<call_id>" + callId + "</call_id>";

            var ws = link.IsStringNullOrEmpty()
           ? new WsCallCenterService.CallCenterClient("CallCenter")
           : new WsCallCenterService.CallCenterClient("CallCenter", new EndpointAddress(link));

            //var v_str_result = ws.submit(v_str_input_string);
            //var v_obj_infor = JsonConvert.DeserializeObject<CLichSuCuocGoi>(v_str_result);

            try
            {
                var v_str_result = ws.submit(v_str_input_string);
                var v_obj_infor = JsonConvert.DeserializeObject<CLichSuCuocGoi>(v_str_result);
                //dat log
                var log = new TmpLogServiceInfo
                {
                    Time = DateTime.Now,
                    Description = "---Input: " + v_str_input_string + "---Output: " + v_str_result.ToString(),
                    CallType = (int)CallType.CheckGetCall,
                };
                log.Status = 0;
                TmpLogServiceRepository.Create(log);
                return v_obj_infor.data;
            }
            catch (Exception ex)
            {
                var log = new TmpLogServiceInfo
                {
                    Time = DateTime.Now,
                    Description = ex.ToString(),
                    CallType = (int)CallType.CheckGetCall,
                };

                log.Status = 1;
                TmpLogServiceRepository.Create(log);

                return null;
            }
        }

        public static CallInfor GetCallNew(string callId, string mobile, string stationId, string agencode, string link = default(string))
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string read = string.Empty;
            string readStr = string.Empty;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(link + "getrecording?callid=" + callId);
                request.KeepAlive = true;
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                read = reader.ReadToEnd();

            }
            catch { }
            finally
            {
                if (response != null)
                    response.Dispose();
                if (request != null)
                    request.Abort();
            }
            if (read != null && !string.Empty.Equals(read))
            {
                var v_obj_infor = JsonConvert.DeserializeObject<CallInfor>(read);
                return v_obj_infor;
            }
            return null;


            //try
            //{
            //    var v_str_result = ws.submit(v_str_input_string);
            //    var v_obj_infor = JsonConvert.DeserializeObject<CLichSuCuocGoi>(v_str_result);
            //    //dat log
            //    var log = new TmpLogServiceInfo
            //    {
            //        Time = DateTime.Now,
            //        Description = "---Input: " + v_str_input_string + "---Output: " + v_str_result.ToString(),
            //        CallType = (int)CallType.CheckGetCall,
            //    };
            //    log.Status = 0;
            //    TmpLogServiceRepository.Create(log);
            //    return v_obj_infor.data;
            //}
            //catch (Exception ex)
            //{
            //    var log = new TmpLogServiceInfo
            //    {
            //        Time = DateTime.Now,
            //        Description = ex.ToString(),
            //        CallType = (int)CallType.CheckGetCall,
            //    };

            //    log.Status = 1;
            //    TmpLogServiceRepository.Create(log);

            //    return null;
            //}
        }
        /// <summary>
        /// Hàm này dùng để get thông tin về cuộc gọi
        /// </summary>
        public static CallInfor GetCallTcl(string callId, string mobile, string stationId, string agenCode, int campainId, string link = default(string))
        {
            const string messageCode = "007";
            var datetimeRequest = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
            var xml = "<message_code>" + messageCode + "</message_code>"
                          + "<campaign_id>" + campainId + "</campaign_id>"
                          + "<station_id>" + stationId + "</station_id>"
                          + "<datetime_request>" + datetimeRequest + "</datetime_request>"
                          + "<call_id>" + callId + "</call_id>";

            try
            {
                var ws = link.IsStringNullOrEmpty()
                    ? new TPCAutoDialClient()
                    : new TPCAutoDialClient("TPCAutoDial", new EndpointAddress(link));
                var result = ws.processMsg(xml);
                var log = new TmpLogServiceInfo
                {
                    Time = DateTime.Now,
                    Description = xml + "_" + result,
                    CallType = (int)CallType.GetCallInfo,
                };
                TmpLogServiceRepository.Create(log);

                var entity = JsonConvert.DeserializeObject<CLichSuCuocGoi>(result);
                return entity.data.error_code == "1" ? null : entity.data;
            }
            catch (Exception ex)
            {
                var log = new TmpLogServiceInfo
                {
                    Time = DateTime.Now,
                    Description = xml + ex,
                    CallType = (int)CallType.GetCallInfo,
                };
                TmpLogServiceRepository.Create(log);
                return null;
            }
        }

        /// <summary>
        /// Hàm này dùng để cập nhật lịch hẹn
        /// </summary>
        public static bool UpdateAppointment(int campainId, int contactId, string mobile, DateTime calledDate, string callId, string stationId, string agentCode, string linkEditContact, out string message, string link = default(string))
        {
            if (!mobile.StartsWith("0")) mobile = "0" + mobile;
            message = string.Empty;
            var xml = "<message_code>008</message_code>" +
                      "<Phone>" + mobile + "</Phone>" +
                      "<CallId>" + callId + "</CallId>" +
                      "<CampainId>" + campainId + "</CampainId>" +
                      "<ContactId>" + contactId + "</ContactId>" +
                      "<AgentCode>" + agentCode + "</AgentCode>" +
                      "<StationId>" + stationId + "</StationId>" +
                      "<Url>" + string.Format(linkEditContact, contactId) + "</Url>" +
                      "<CallDate>" + calledDate.ToString("ddMMyyyyHHmmss") + "</CallDate>" +
                      "<CreateDate>" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "</CreateDate>";

            try
            {
                // Call Service
                var result = (link.IsStringNullOrEmpty()
                    ? new TPCAutoDialClient()
                    : new TPCAutoDialClient("TPCAutoDial", new EndpointAddress(link))).processMsg(xml);
                var code = result.GetElement("<error_code>(?<text>.*?)</error_code>");
                message = result.GetElement("<error_desc>(?<text>.*?)</error_desc>");

                // Log
                var log = new TmpLogServiceInfo
                {
                    Time = DateTime.Now,
                    CallType = (int)CallType.UpdateAppointment,
                    Description = (int)ErrorServiceType.Success + "_" + xml + "_" + result,
                };
                TmpLogServiceRepository.Create(log);

                // Return
                return code.ToInt32() == 0;
            }
            catch (Exception ex)
            {
                var log = new TmpLogServiceInfo
                {
                    Time = DateTime.Now,
                    Description = ex + "_" + xml,
                    CallType = (int)CallType.UpdateAppointment,
                };
                TmpLogServiceRepository.Create(log);
                return false;
            }
        }


        /// <summary>
        /// Hàm này dùng để lấy thông tin cuộc gọi phỏng vấn
        /// </summary>
        /// <param name="CallHistoryId"></param>
        /// <param name="ContactId"></param>
        /// <param name="AgentCode"></param>
        /// <param name="StationId"></param>
        /// <param name="MobilePhone"></param>
        /// <param name="ResponseTime"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="RingTime"></param>
        /// <param name="LinkRecord"></param>
        /// <returns></returns>
        public static CasecCallInfor GetCallCasec(int CallHistoryId, int ContactId, int AgentCode, int StationId, string MobilePhone, DateTime ResponseTime, DateTime StartTime, DateTime EndTime, DateTime RingTime, string LinkRecord)
        {
            const string v_str_message_code = "010";
            var v_dat_datetime_request = DateTime.Now.ToLongTimeString();
            if (!MobilePhone.StartsWith("0")) MobilePhone = "0" + MobilePhone;

            var v_str_input_string = "<agent_code>" + AgentCode + "</agent_code>"
                                        + "<mobile_phone>" + MobilePhone + "</mobile_phone>"
                                        + "<station_id>" + StationId + "</station_id>"
                                        + "<datetime_request>" + v_dat_datetime_request + "</datetime_request>"
                                        + "<message_code>" + v_str_message_code + "</message_code>"
                                        + "<call_id>" + CallHistoryId + "</call_id>"
                                        + "<ContactId>" + ContactId + "</ContactId>"
                                        + "<LinkRecord>" + LinkRecord + "</LinkRecord>";
            var link = default(string);
            var ws = link.IsStringNullOrEmpty()
                ? new WsCallCenterService.CallCenterClient("CallCenter")
                : new WsCallCenterService.CallCenterClient("CallCenter", new EndpointAddress(link));
            var v_str_result = ws.submit(v_str_input_string);
            var v_obj_infor = JsonConvert.DeserializeObject<CLichSuCuocGoi>(v_str_result);

            return v_obj_infor.dataCasec;
        }

        public Result SendCasecAccount(CasecAccountInfo info)
        {
            var log = new TmpLogServiceInfo();
            log.Status = 0;
            log.CallType = (int)CallType.SendCasecAccount;
            log.Time = DateTime.Now;
            try
            {
                var model = new CasecAccount();
                model.contactId = info.ContactId;
                model.casecAccount = info.Account;
                model.passWord = info.Password;
                //
                var infoContact = ContactRepository.GetInfo(info.ContactId);
                model.tenHV = infoContact.Fullname;

                var ws = new WsCRMSoapClient("WsCRMSoap");
                var input = JsonConvert.SerializeObject(model);
                var output = ws.UpdateAcountCasec(input);

                var result = JsonConvert.DeserializeObject<Result>(output);

                log.Description = result.Description + "_" + input;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Không gửi được thông tin tài khoản cấp cho Chuyên Môn";
                log.Description = result.Description;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
        }

        public Result SendTopicaAccount(TopicaAccountInfo info)
        {
            return null;
        }

        public Result SendInterviewInfo(AppointmentInterviewInfo info, bool isVip)
        {
            var log = new TmpLogServiceInfo();
            log.Status = 0;
            log.CallType = (int)CallType.SendInterviewInfo;
            log.Time = DateTime.Now;
            try
            {
                //gọi link của CongNN truyền vào các tham số contactId, Account, Password trên    
                var model = new InterviewInfo();
                model.hocVienID = info.ContactId;
                var infoContact = ContactRepository.GetInfo(info.ContactId);
                model.hoTenHocVien = infoContact.Fullname;

                model.ngayHenPV = (DateTime)info.AppointmentDate;
                model.khungGioID = info.TimeSlotId;
                model.ghiChuLichHen = info.Notes;
                var phones = PhoneRepository.GetByContact(info.ContactId);
                foreach (var phone in phones)
                {
                    if (phone.IsPrimary == 1)
                    {
                        model.dienThoaiHV = phone.PhoneNumber;
                        break;
                    }
                }
                //model.dienThoaiHV = infoContact.Mobile1;
                model.kieuGV = info.TeacherTypeId;
                model.levelTester = info.LevelTesterId;
                //model.UserId = infoContact.UserConsultantId;
                var infoUser = UserRepository.GetInfo(info.UserId);
                model.AccountTVTS = infoUser.UserName;


                var ws = new WsCRMSoapClient("WsCRMSoap");
                var input = JsonConvert.SerializeObject(model);

                var output = isVip == true ? ws.UpdateTTPhongVanForVip(input) : ws.UpdateTTPhongVan(input);

                var result = JsonConvert.DeserializeObject<Result>(output);

                log.Description = result.Description + "_" + input;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Có lỗi xảy ra trong quá trình đặt lịch phỏng vấn. Error: " + ex.Message;
                log.Description = result.Description;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
        }
        public Result SendChangeInterviewInfo(AppointmentInterviewInfo info)
        {
            var log = new TmpLogServiceInfo();
            log.Status = 0;
            log.CallType = (int)CallType.SendChangeInterviewInfo;
            log.Time = DateTime.Now;
            try
            {
                var model = new ChangeInterviewInfo();
                model.hocVienID = info.ContactId;
                var infoContact = ContactRepository.GetInfo(info.ContactId);
                model.hoTenHocVien = infoContact.Fullname;

                model.ngayHenPV = (DateTime)info.AppointmentDate;
                model.khungGioID = info.TimeSlotId;
                model.ghiChuLichHen = info.Notes;
                var phones = PhoneRepository.GetByContact(info.ContactId);
                foreach (var phone in phones)
                {
                    if (phone.IsPrimary == 1)
                    {
                        model.dienThoaiHV = phone.PhoneNumber;
                        break;
                    }
                }
                //model.dienThoaiHV = infoContact.Mobile1;
                model.kieuGV = info.TeacherTypeId;
                model.levelTester = info.LevelTesterId;
                //model.UserId = infoContact.UserConsultantId;
                var infoUser = UserRepository.GetInfo(info.UserId);
                model.AccountTVTS = infoUser.UserName;


                var ws = new WsCRMSoapClient("WsCRMSoap");
                var input = JsonConvert.SerializeObject(model);
                var output = ws.ChangeInterviewInfo(input);

                var result = JsonConvert.DeserializeObject<Result>(output);

                log.Description = result.Description + "_" + input;
                log.Status = result.Code;

                TmpLogServiceRepository.Create(log);
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Có lỗi xảy ra trong quá trình đặt lịch phỏng vấn. Error: " + ex.Message;
                log.Description = result.Description;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
        }
        public Result SendCancelInterview(int ContactId)
        {
            var log = new TmpLogServiceInfo();
            log.Status = 0;
            log.CallType = (int)CallType.SendCancelInterview;
            log.Time = DateTime.Now;
            try
            {
                var model = new CancelInterview();
                model.hocVienID = ContactId;

                var ws = new WsCRMSoapClient("WsCRMSoap");
                var input = JsonConvert.SerializeObject(model);
                var output = ws.CancelInterview(input); // Sửa tên Hàm của CongNN viết 

                var result = JsonConvert.DeserializeObject<Result>(output);

                log.Description = result.Description + "_" + input;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Không gửi được hủy phỏng vấn cho chuyên môn";
                log.Description = result.Description;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
        }

        public Result SendRequestSB100(int ContactId, int feePackageType) // tuy chon, thoa thich
        {
            var log = new TmpLogServiceInfo();
            log.Status = 0;
            log.CallType = (int)CallType.SendRequestSB100;
            log.Time = DateTime.Now;
            try
            {
                //
                var infoContact = ContactRepository.GetInfo(ContactId);
                var model = new SB100();
                model.hocVienId = ContactId;
                model.hoTenHocVien = infoContact.Fullname;
                var phones = PhoneRepository.GetByContact(ContactId);
                foreach (var phone in phones)
                {
                    if (phone.IsPrimary == 1)
                    {
                        model.dienThoai = phone.PhoneNumber;
                        break;
                    }
                }
                var infoLevel = ContactLevelInfoRepository.GetInfoByContactId(ContactId);
                model.kieuHocPhiId = feePackageType; // infoLevel.FeePackageType;
                var infoUser = UserRepository.GetInfo(infoContact.UserConsultantId);
                model.TVTS = infoUser.UserName;

                var ws = new WsCRMSoapClient("WsCRMSoap");
                var input = JsonConvert.SerializeObject(model);
                var output = ws.UpdateSB100(input);

                var result = JsonConvert.DeserializeObject<Result>(output);

                log.Description = result.Description + "_" + input;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Không gửi được thông tin tài khoản cấp cho Chuyên Môn";
                log.Description = result.Description;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
        }

        public Result SendRequestSB100Topica(int ContactId, int feePackageType) // tuy chon, thoa thich
        {
            var log = new TmpLogServiceInfo();
            log.Status = 0;
            log.CallType = (int)CallType.SendRequestSB100Topica;
            log.Time = DateTime.Now;
            try
            {
                var TVTS = UserRepository.GetCurrentUserInfo();
                var infoContact = ContactRepository.GetInfo(ContactId);
                var infoResult = TestResultRepository.GetResultTopicaCurent(ContactId);
                var model = new SB100Topica();
                model.hocVienId = ContactId;
                model.ngayTest = infoResult.TestDate;
                model.S1 = infoResult.S1;
                model.S2 = infoResult.S2;
                model.S3 = infoResult.S3;
                model.S4 = infoResult.S4;
                var phones = PhoneRepository.GetByContact(ContactId);
                foreach (var phone in phones)
                {
                    if (phone.IsPrimary == 1)
                    {
                        model.mobile = phone.PhoneNumber;
                        break;
                    }
                }
                model.tenHocVien = infoContact.Fullname;
                model.TVTS = TVTS.UserName;

                var infoLevel = ContactLevelInfoRepository.GetInfoByContactId(ContactId);
                model.kieuHocPhiId = feePackageType; // infoLevel.FeePackageType;
                var infoUser = UserRepository.GetInfo(infoContact.UserConsultantId);


                var ws = new WsCRMSoapClient("WsCRMSoap");
                var input = JsonConvert.SerializeObject(model);
                var output = ws.UpdateSB100_topica(input);

                var result = JsonConvert.DeserializeObject<Result>(output);

                log.Description = result.Description + "_" + input;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Code = 1;
                result.Description = "Không gửi được thông tin tài khoản cấp cho Chuyên Môn";
                log.Description = result.Description;
                log.Status = result.Code;
                TmpLogServiceRepository.Create(log);
                return result;
            }
        }
    }
}
