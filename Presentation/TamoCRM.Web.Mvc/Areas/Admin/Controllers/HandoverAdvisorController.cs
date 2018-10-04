using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.StatusMap;
using TamoCRM.Domain.LogsMoney;
using TamoCRM.Services.CasecAccounts;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Phones;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.TestResults;
using TamoCRM.Services.TopicaAccounts;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactLevelInfos;
using TamoCRM.Services.MoneyLogs;
using RestSharp;
using TamoCRM.Domain.Recharge;
using Newtonsoft.Json;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Domain.TopicaAccounts;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Services.LogHandoverAdvisor;
using TamoCRM.Services.FeeMoneyType;
using TamoCRM.Services.SourceTypes;
using System.Configuration;
using RestSharp.Authenticators;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class HandoverAdvisorController : AdminController
    {
        //
        // GET: /Admin/HandoverAdvisor/

        public ActionResult HandoverContact(int id, ContactLevelInfoModel modelCache = null)
        {
            ViewBag.SourceTypes = SourceTypeRepository.GetAll();

            var user = UserContext.GetCurrentUser();

            if (user == null) return RedirectToAction("FilterContactToday", "ContactFilter", new { area = "Admin" });

            var contactInfo = ContactRepository.GetInfo(id);
            if (contactInfo == null) return RedirectToAction("FilterContactToday", "ContactFilter", new { area = "Admin" });

            if (user.GroupConsultantType == (int)GroupConsultantType.Consultant && user.UserID != contactInfo.UserConsultantId)
                return RedirectToAction("FilterContactToday", "ContactFilter", new { area = "Admin" });

            string TransactionReason = "";

            List<LogsMoneyInfo> logMoneyInfos = MoneyLogsRepository.GetAllByContactId(id);      
            foreach(var logMoney in logMoneyInfos)
            {
                TransactionReason += logMoney.NoteChuyenKhoan + "@@@";
            }

            ViewBag.Id = id;
            ViewBag.IsView = user.UserID != contactInfo.UserConsultantId ? 1 : 0;       

            if (user.GroupConsultantType == (int)GroupConsultantType.ManagerConsultant)
                ViewBag.IsView = 0;
            var contactLevelInfo = ContactLevelInfoRepository.GetInfoByContactId(id) ?? new ContactLevelInfo();
            var model = InitModel(contactInfo, contactLevelInfo);

            model.ContactInfo.Consultant = user.UserName;
            model.ContactInfo.UserName = user.FullName; //Lay ten TVTS
            model.ContactLevelInfo.TransactionReason = TransactionReason;
            model.PhoneTVTS = user.Mobile; //điện thoại TVTS
            model.EmailTVTS = user.Email;  //Email TVTS

            if (model.ContactInfo != null)
            {
                if (modelCache != null && modelCache.ContactInfo != null)
                {
                    model.ContactInfo.QualityId = modelCache.ContactInfo.QualityId;
                    model.ContactInfo.CallInfoConsultant = modelCache.ContactInfo.CallInfoConsultant;
                    model.ContactInfo.StatusMapConsultantId = modelCache.ContactInfo.StatusMapConsultantId;
                    model.ContactInfo.StatusCareConsultantId = modelCache.ContactInfo.StatusCareConsultantId;
                }
                else
                {
                    model.ContactInfo.QualityId = 0;
                    model.ContactInfo.StatusMapConsultantId = 0;
                    model.ContactInfo.StatusCareConsultantId = 0;
                    model.ContactInfo.CallInfoConsultant = string.Empty;
                }
            }
            return View(model);
        }

        private ContactLevelInfoModel InitModel(ContactInfo contactInfo, ContactLevelInfo contactLevelInfo)
        {
            var model = new ContactLevelInfoModel
            {
                ContactInfo = contactInfo,
                ContactLevelInfo = contactLevelInfo,
            };
            if (model.ContactLevelInfo != null)
            {
                // AppointmentTime
                if (model.ContactLevelInfo.AppointmentTime != null)
                    model.AppointmentTime = model.ContactLevelInfo.AppointmentTime.Value.ToString("dd/MM/yyyy");
            }

            if (model.ContactInfo != null)
            {
                // Phone
                var listPhone = PhoneRepository.GetByContact(contactInfo.Id);
                if (listPhone != null)
                {
                    model.ContactInfo.Mobile1 = listPhone.Count > 0 ? listPhone[0].PhoneNumber : string.Empty;
                    model.ContactInfo.Mobile2 = listPhone.Count > 1 ? listPhone[1].PhoneNumber : string.Empty;
                    model.ContactInfo.Mobile3 = listPhone.Count > 2 ? listPhone[2].PhoneNumber : string.Empty;
                }

                // Birthday
                if (model.ContactInfo.Birthday != null)
                    model.Birthday = model.ContactInfo.Birthday.Value.ToString("dd/MM/yyyy");
            }
            else model.ContactInfo = new ContactInfo();

            // StatusMaps
            List<StatusMapInfo> statusMaps;
            if (StoreData.ListStatusMap != null && StoreData.ListStatusMap.Count > 0)
            {
                statusMaps = model.ContactInfo == null
                                     ? StoreData.ListStatusMap
                                     : StoreData.ListStatusMap.Where(c => c.LevelId == model.ContactInfo.LevelId).ToList();
            }
            else
            {
                statusMaps = model.ContactInfo == null
                                         ? StatusMapRepository.GetAll()
                                         : StatusMapRepository.GetAllByLevelId(model.ContactInfo.LevelId);
            }
            ViewBag.StatusMaps = statusMaps != null && statusMaps.Count > 0
                                     ? statusMaps.Where(c => c.StatusMapType == (int)EmployeeType.Consultant).ToList()
                                     : new List<StatusMapInfo>();
            // CasecAccountInfo
            var casecAccounts = CasecAccountRepository.GetAllByContactId(contactLevelInfo.ContactId) ?? new List<CasecAccountInfo>();
            model.CasecAccountInfo = casecAccounts.FirstOrDefault(c => c.StatusCasecAccountId == (int)StatusCasecType.Used);
            model.ContactLevelInfo.HasCasecAccount = model.CasecAccountInfo != null;
            model.ContactLevelInfo.HasCasecAccountHidden = model.ContactLevelInfo.HasCasecAccount;

            // TopicaAccountInfo
            var topicaAccounts = TopicaAccountRepository.GetAllByContactId(contactLevelInfo.ContactId) ?? new List<TopicaAccountInfo>();
            model.TopicaAccountInfo = topicaAccounts.FirstOrDefault(c => c.StatusTopicaAccountId == (int)StatusTopicaType.Used);
            model.ContactLevelInfo.HasTopicaAccount = model.TopicaAccountInfo != null;
            model.ContactLevelInfo.HasCasecAccountHidden = model.ContactLevelInfo.HasTopicaAccount;

            // TestResultCasecInfo
            model.TestResultCasecInfo = TestResultRepository.GetResultCasecCurent(contactLevelInfo.ContactId);
            if (model.TestResultCasecInfo != null)
            {
                if (model.TestResultCasecInfo.FullName == null || model.TestResultCasecInfo.FullName == "")
                    model.TestResultCasecInfo.FullName = model.ContactInfo.Fullname;
                model.ContactLevelInfo.HasPointTestCasec = true;
                var casecAccount = casecAccounts.FirstOrDefault(c => c.Id == model.TestResultCasecInfo.CasecAccountId) ?? new CasecAccountInfo();
                model.TestResultCasecInfo.Account = casecAccount.Account;
                model.TestResultCasecInfo.Password = casecAccount.Password;
            }
            else model.ContactLevelInfo.HasPointTestCasec = false;
            model.ContactLevelInfo.HasPointTestCasecHidden = model.ContactLevelInfo.HasPointTestCasec;

            // TestResultTopicaInfo
            model.TestResultTopicaInfo = TestResultRepository.GetResultTopicaCurent(contactLevelInfo.ContactId);
            if (model.TestResultTopicaInfo != null)
            {
                if (model.TestResultTopicaInfo.FullName == null || model.TestResultTopicaInfo.FullName == "")
                    model.TestResultTopicaInfo.FullName = model.ContactInfo.Fullname;
                model.ContactLevelInfo.HasPointTestTopica = true;
                var topicaAccount = topicaAccounts.FirstOrDefault(c => c.Account == model.TestResultTopicaInfo.Account) ?? new TopicaAccountInfo();
                model.TestResultTopicaInfo.Account = topicaAccount.Account;
                model.TestResultTopicaInfo.Password = topicaAccount.Password;
            }
            else model.ContactLevelInfo.HasPointTestTopica = false;
            model.ContactLevelInfo.HasPointTestCasecHidden = model.ContactLevelInfo.HasPointTestTopica;

            // TestResultInterviewInfo
            model.TestResultInterviewInfo = TestResultRepository.GetResultInterviewCurent(contactLevelInfo.ContactId);
            if (model.TestResultInterviewInfo != null)
            {
                if (model.TestResultInterviewInfo.FullName == null || model.TestResultInterviewInfo.FullName == "")
                    model.TestResultInterviewInfo.FullName = model.ContactInfo.Fullname;
                model.ContactLevelInfo.HasPointInterview = true;
            }
            else model.ContactLevelInfo.HasPointInterview = false;
            model.ContactLevelInfo.HasPointInterviewHidden = model.ContactLevelInfo.HasPointInterview;

            // TestResultLinkSb100Info
            model.TestResultLinkSb100Info = TestResultRepository.GetResultLinkSb100Curent(contactLevelInfo.ContactId);

            return model;
        }

        [HttpPost]
        public ActionResult HandoverContact(ContactLevelInfoModel model)
        {
            string linkApiHandoverAdvisor = ConfigurationManager.AppSettings["LinkAPIHandoverAdvisor"].ToString();
            //if (model.ContactLevelInfo.HandoverAdvisor == 1) 
            //{
                string username = "admin";
                string password = "admin";

                var contactid = model.ContactInfo.Id;
                var code = model.ContactInfo.Code;
                var phone = model.ContactInfo.Mobile1;
                var email = model.ContactInfo.Email;
                var fullname = model.ContactInfo.Fullname;
                var gender_id = model.ContactInfo.Gender;
                var note = model.ContactLevelInfo.DacDiemHocVien;

                int iFeeMoneyType = model.ContactLevelInfo.FeeMoneyType;
                var package_want_to_learn = model.ContactLevelInfo.PackageWantToLearn;
                var level_crm = model.ContactInfo.LevelId.ToString();
                var deposit_need = model.ContactLevelInfo.PackagePriceSale;
                var actually_paid = model.ContactLevelInfo.FeeEdu;
                //var technical_test = model.ContactLevelInfo.HasTestTechnical; //da kiem tra ky thuat chua
                var technical_test = 1;
                var tvts_id = model.ContactInfo.Consultant;
               
                var tvts_name = model.ContactInfo.UserName;
                var transaction_reason = model.ContactLevelInfo.TransactionReason;
                var sb100 = model.TestResultLinkSb100Info.LinkSb100 != null? model.TestResultLinkSb100Info.LinkSb100: "";
                var casec_total = model.TestResultCasecInfo.TotalCasec;
                var toiec_total = model.TestResultTopicaInfo.TotalTopica;
                var interview_total = model.TestResultInterviewInfo.TotalInterview;
                var tvts_phone = Request["PhoneTVTS"].ToString();

                var time_want_to_learn = (Request["time_want_to_learn"]);     
                if(!time_want_to_learn.IsStringNullOrEmpty())
                {
                    time_want_to_learn += string.IsNullOrEmpty(model.Time24hWantToLearn) ? " 00:00:00" : " " + model.Time24hWantToLearn;
                }

                var tvts_email = Request["EmailTVTS"].ToString();

                int level_study_id = Int32.Parse(Request["LevelStudyAdvisor"]);
                string level_study;

                string gender;
                if (gender_id == 1)
                {
                    gender = "Nam";
                }
                else
                {
                    gender = "Nữ";
                }


                if (level_study_id == 1)
                {
                    level_study = "basic100";
                }
                else if (level_study_id == 2)
                {
                    level_study = "basic200";
                }
                else if (level_study_id == 3)
                {
                    level_study = "basic300";
                }
                else if (level_study_id == 4)
                {
                    level_study = "inter100";
                }
                else if (level_study_id == 5)
                {
                    level_study = "inter200";
                }
                else if (level_study_id == 6)
                {
                    level_study = "inter300";
                }
                else if (level_study_id == 7)
                {
                    level_study = "advan100";
                }
                else if (level_study_id == 8)
                {
                    level_study = "advan200";
                }
                else if (level_study_id == 9)
                {
                    level_study = "advan300";
                }
                else
                {
                    level_study = "sbasic";
                }

                
                var client = new RestClient(linkApiHandoverAdvisor);
                client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest();
                if(model.ContactLevelInfo.HandoverAdvisor == 1)
                {
                    request = new RestRequest("advisor_api/create_new_student_info", Method.POST);
                }
                else if (model.ContactLevelInfo.HandoverAdvisor == 3)
                {
                    request = new RestRequest("advisor_api/update_new_student_info", Method.POST);
                }

                request.AddHeader("Accept", "application/json");

                request.AddParameter("key", "SSeKfm7RXCJZxnFUleFsPf63o2ymZ93fWuCmvC34");

                request.AddParameter("contact_id", code);
                request.AddParameter("phone", phone);
                request.AddParameter("email", email);
                request.AddParameter("full_name", fullname);
                request.AddParameter("gender", gender);
                request.AddParameter("package_want_to_learn", package_want_to_learn);
                request.AddParameter("lang", "vi");
                request.AddParameter("note", note);
                request.AddParameter("level_crm", "L"+level_crm);
                request.AddParameter("deposit_need", deposit_need);
                request.AddParameter("actually_paid", actually_paid);               
                
                request.AddParameter("technical_test", technical_test);
                request.AddParameter("tvts_id", tvts_id);
                request.AddParameter("tvts_name", tvts_name);
                request.AddParameter("actually_paid", actually_paid);
                request.AddParameter("transaction_reason", transaction_reason);
                request.AddParameter("sb100", sb100);
                request.AddParameter("casec_total", casec_total);
                request.AddParameter("toiec_total", toiec_total);
                request.AddParameter("interview_total", interview_total);
                request.AddParameter("tvts_phone", tvts_phone);
                request.AddParameter("time_want_to_learn", time_want_to_learn);
                request.AddParameter("tvts_email", tvts_email);
                request.AddParameter("level_study", level_study);

                try {
                    request.JsonSerializer.ContentType = "application/json; charset=utf-8";
                    IRestResponse response = client.Execute(request);

                    var content = response.Content;
                    var input = JsonConvert.DeserializeObject<LogTaoHocVien>(content);

                    if (input.state == 1)
                    {
                        ViewBag.Close = 1;
                        ViewBag.Message = "Bàn giao thành công";
                        int value = (int)StatusHandoverAdvisor.Handover;

                        ContactLevelInfoRepository.UpdateHandoverAdvisorStatusWithContactId(contactid, value);
                        ContactLevelInfoRepository.UpdateLevelStudyAdvisor(contactid, level_study_id);

                        DateTime dt_timewanttolearn = DateTime.ParseExact(time_want_to_learn, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        ContactLevelInfoRepository.Update_DateWantToLearn(contactid, dt_timewanttolearn);
                        //Viet them ham cap nhat da kiem tra test ky thuat
                        LogHandoverAdvisorRepository.Insert_LogHandoverAdvisor(code, "", value);

                        return HandoverContact(model.ContactInfo.Id, model);
                    }
                    else
                    {
                        ViewBag.Message = "Bàn giao không thành công: " + input.msg;
                        return HandoverContact(model.ContactInfo.Id, model);
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.Message = "Đã có lỗi xảy ra, vui lòng thực hiện lại!";
                    return HandoverContact(model.ContactInfo.Id, model);
                }

            return HandoverContact(model.ContactInfo.Id, model);
        }
    }
}
