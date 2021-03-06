using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Domain.ContactLevelInfos;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.MissedCall;
using TamoCRM.Domain.StatusMap;
using TamoCRM.Domain.Logs;
using TamoCRM.Services.AppointmentInterview;
using TamoCRM.Services.CasecAccounts;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Phones;
using TamoCRM.Services.StatusMap;
using TamoCRM.Services.TestResults;
using TamoCRM.Services.UserRole;
using TamoCRM.Services.FeeMoneyType;
using TamoCRM.Services.LevelTester;
using TamoCRM.Services.Logs;
using TamoCRM.Services.CC3ContactReport;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactLevelInfos;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Services.Catalogs;
using TamoCRM.Domain.TopicaAccounts;
using TamoCRM.Services.TopicaAccounts;
using System.Net.Http.Formatting;
using TamoCRM.Domain.LogsMoney;
using TamoCRM.Services.MoneyLogs;
using RestSharp;
using Newtonsoft.Json;
using TamoCRM.Domain.Recharge;
using TamoCRM.Services.Packge;
using TamoCRM.Services.PricePackageListed;
using System.Configuration;
using TamoCRM.Services.Product;
using TamoCRM.Services.TimeSlot;
using TamoCRM.Services.EstimateOwner;
using TamoCRM.Services.Country;
using TamoCRM.Domain.TestResults;
using TamoCRM.Services.Banks;
using System.Globalization;
using TamoCRM.Services.PaymentType;
using TamoCRM.Services.ContactDuplicates;
using TamoCRM.Services.Quality;
using TamoCRM.Domain.CC3Report;
using TamoCRM.Services.Logs;
using TamoCRM.Domain.LogDashBoard;
using TamoCRM.Services.Report;
using RestSharp.Authenticators;
using System.Diagnostics;
using static TamoCRM.Services.Logs.TmpLogServiceRepository;
using TamoCRM.Services.Sources;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ContactLevelInfoController : AdminController
    {
        bool checkHasAccPayment = false; //kiem tra xem da tao tai khoan tren payment chua 
        int history_Id; //Lay id phiên nạp tiền sang advisor 
        public ActionResult Edit(int id, ContactLevelInfoModel modelCache = null)
        {
            #region "Log Checkpoint - Begin Load Form cham soc hoc vien 28/10/2016"
            int SessionLog = TmpJobReportRepository.GetSessionLog();
            ViewBag.SessionLog = SessionLog;
            DateTime TimeBegin = DateTime.Now;
            ViewBag.TimeBegin = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff");

            var logbegin = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeBegin.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "BEGIN_LOAD_CHAM_SOC_CONTACT_TVTS",
                ContactId = id,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            LogDashBoardRepository.CreateLogDashBoard(logbegin);
            #endregion

            var user = UserContext.GetCurrentUser();
            if (user == null) return RedirectToAction("FilterContactToday", "ContactFilter", new { area = "Admin" });

            var contactInfo = ContactRepository.GetInfo(id);
            if (contactInfo == null) return RedirectToAction("FilterContactToday", "ContactFilter", new { area = "Admin" });

            if (user.GroupConsultantType == (int)GroupConsultantType.Consultant && user.UserID != contactInfo.UserConsultantId)
                return RedirectToAction("FilterContactToday", "ContactFilter", new { area = "Admin" });
           

            ViewBag.Id = id;
            ViewBag.IsView = user.UserID != contactInfo.UserConsultantId ? 1 : 0;

            ViewBag.Products = StoreData.ListProduct;
            ViewBag.TimeSlots = StoreData.ListTimeSlot;
            ViewBag.LevelTester = LevelTesterRepository.GetAll();
            ViewBag.StatusCares = StoreData.ListStatusCare;

            if (user.GroupConsultantType == (int)GroupConsultantType.ManagerConsultant)
                ViewBag.IsView = 0;

            var contactLevelInfo = ContactLevelInfoRepository.GetInfoByContactId(id) ?? new ContactLevelInfo();
            var model = InitModel(contactInfo, contactLevelInfo);

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

            #region "Log Checkpoint - End Load Form cham soc hoc vien 28/10/2016"
            DateTime TimeEnd = DateTime.Now;
            var logend = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = TimeEnd.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = "END_CONTROLLER_LOAD_CHAM_SOC_CONTACT_TVTS",
                ContactId = id,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                Runtime = "0",
                Session = SessionLog,
            };

            if (ViewBag.Close == null)
            {
                LogDashBoardRepository.CreateLogDashBoard(logend);
            }
            #endregion

            return View(model);
            
        }
        protected bool NapTienHocVien(string code, string name, string phone, string mail, string country, string bank, string typeappointment, string estimateowner, string package, string value, string reason, bool has_acc_payment, string userConsulant, double packagePriceSale, int levelCt)
        {
            try
            {
                string username = "crm";
                string password = "crm.topica.";
                string linkApiPayment = ConfigurationManager.AppSettings["LinkAPIPayment"].ToString();
                var client = new RestClient(linkApiPayment);

                client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request_create = new RestRequest("User/Add", Method.POST);
                request_create.AddHeader("Accept", "application/json");

                request_create.AddParameter("key", "6WxDCFTjlgjH6L6YLA03LNW1JbJbCZGCLHa0DIXT");
                request_create.AddParameter("ContactId", code);
                request_create.AddParameter("UserName", name);
                request_create.AddParameter("UserPhone", phone);
                request_create.AddParameter("UserEmail", mail);

                request_create.JsonSerializer.ContentType = "application/json; charset=utf-8";

                IRestResponse response_create = client.Execute(request_create);

                var content_create = response_create.Content;
                var input = JsonConvert.DeserializeObject<RechargeInfo>(content_create);

                if (input.status == "True")
                {
                    checkHasAccPayment = true;
                }

                //Tạo tài khoản trên payment, trường hợp đã tạo rồi thì kiểm tra ContactId đã tồn tại và đã tạo acc bên payment hay chưa (biến has_acc_payment)
                if (input.status == "True" || (input.status == "False" && input.status_code == "CONTACTID_ALREADY_EXIST" && has_acc_payment == true))
                {
                    var request = new RestRequest("Balance/Deposit", Method.POST);
                    request.AddHeader("Accept", "application/json");

                    request.AddParameter("key", "6WxDCFTjlgjH6L6YLA03LNW1JbJbCZGCLHa0DIXT");
                    request.AddParameter("ContactId", code);
                    request.AddParameter("UserName", name);
                    request.AddParameter("UserPhone", phone);
                    request.AddParameter("UserEmail", mail);
                    request.AddParameter("Value", value); //Số tiền nộp
                    request.AddParameter("Reason", reason);
                    request.AddParameter("TransactionBy", typeappointment);
                    request.AddParameter("EstimateOwner", estimateowner);
                    request.AddParameter("TransactionOwner", "CRM");
                    request.AddParameter("Package", package);
                    request.AddParameter("Country", country);  //Việt Nam, Thái Lan    
                    request.AddParameter("FromBanking", bank);
                    request.AddParameter("AutoTransfer", true);
                    request.AddParameter("UserCreate", userConsulant);
                    request.AddParameter("SaleCost", packagePriceSale);
                    request.AddParameter("Level", levelCt);

                    request.JsonSerializer.ContentType = "application/json; charset=utf-8";

                    try
                    {
                        IRestResponse response = client.Execute(request);
                        var response_addcashier = response.Content;
                        var input_addcashier = JsonConvert.DeserializeObject<LogNapTien>(response_addcashier);
                        history_Id = input_addcashier.transaction_id;

                        return input_addcashier.status == "True" ? true : false;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Lỗi : " + ex.Message.ToString();
                        return false;
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Lỗi : " + ex.Message.ToString();
                return false;
            }
        }

        //Lấy danh sách gói học
        protected string GetNamePackageLearn(string package_product, string package_type, string package_cost, string package_unit)
        {
            try
            {
                string username = "admin";
                string password = "admin";
                string linkApiGetPackageLearn = ConfigurationManager.AppSettings["LinkApiMarketGetPackageLearn"].ToString();

                var client = new RestClient(linkApiGetPackageLearn);
                client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request_create = new RestRequest("packageAPI/crmpackage", Method.GET);
                request_create.AddHeader("Accept", "application/json");

                request_create.AddParameter("key", "vcIXD3cK8LchElyTWPq96fIOOwkfnK");
                request_create.AddParameter("package_product", package_product);
                request_create.AddParameter("package_type", package_type);
                request_create.AddParameter("package_cost", package_cost);
                request_create.AddParameter("package_unit", package_unit);

                request_create.JsonSerializer.ContentType = "application/json; charset=utf-8";

                IRestResponse response_create = client.Execute(request_create);
                var content_create = response_create.Content;
                var input = JsonConvert.DeserializeObject<RechargeInfo>(content_create);

                return input.data;
            }
            catch(Exception ex)
            {
                TmpLogServiceInfo log = new TmpLogServiceInfo();
                log.Time = DateTime.Now;
                log.CallType = CallType.ExceptionGetNamePackageLearn.ToInt32();
                log.Status = 0;
                log.Description = ex.Message;
                TmpLogServiceRepository.Create(log);         
                return "error";
            }
        }

        [HttpPost]
        public ActionResult Edit(ContactLevelInfoModel model)
        {
            try
            {
                //Để tất cả contact ở trạng thái chưa bàn giao sang bên Advisor
                int levelContact = model.ContactInfo.LevelId;
                if (levelContact == 1) //Contact khi ở trạng thái level 1 đầu tiên sẽ update statushandoveradvisor là 1
                {
                    model.ContactLevelInfo.HandoverAdvisor = (int)StatusHandoverAdvisor.NotHandover;
                }

                var userTVTS = UserContext.GetCurrentUser();
                bool statusCallApi = false; //Check trang thai goi thanh cong API nap tien hay ko
                double PricePackage;
                bool hasAccPayment = false;
                string PackageWantToLearn = "";

                int ContactId = model.ContactInfo.Id;
                string Code = model.ContactInfo.Code;
                string UserName = model.ContactInfo.Fullname;
                string UserPhone = model.ContactInfo.Mobile1;
                string UserEmail = model.ContactInfo.Email;
                double packagePriceSale = model.ContactLevelInfo.PackagePriceSale;            
                
                string Value = Request["TienNop"].ToString(); //So tien nop
                string Reason = (model.ContactLevelInfo.FeeEduNotes); // Noi dung chuyen khoan
                int TypeAppointment = Convert.ToInt32(Request["KieuThanhToan"].ToString());
                int iBank = TypeAppointment == 3 ? Convert.ToInt32(Request["NganHang"].ToString()) : 0;

                PricePackage = model.ContactLevelInfo.PackagePriceSale;
                int iEstimateOwner = Convert.ToInt32(Request["ChuDuToan"].ToString());
                int iDia_Phuong = model.ContactInfo.NationId;
                DateTime? DateCollection = Request["NgayThucThu"].ToDateTime();

                string sBank;
                sBank = iBank == 0 ? "" : BankRepository.GetBankWithId(iBank);

                string KieuThanhToan;
                KieuThanhToan = PaymentTypeRepository.GetValuePaymentTypeWithId(TypeAppointment);

                string ChuDuToan;
                ChuDuToan = EstimateOwnerRepository.GetValueWithId(iEstimateOwner);

                string sDiaPhuong;
                sDiaPhuong = CountryRespository.GetValueWithId(iDia_Phuong);
                
                //Check cac statusmap len L7 va L8 (Nạp tiền)
                int sttmap = model.ContactInfo.StatusMapConsultantId;
                int userTestNapTien = model.ContactInfo.UserConsultantId;

                int[] intArrayUser = {202, 219, 205, 337, 265, 317, 212, 216, 239, 241, 242, 243, 244, 264, 266, 332, 335, 356, 362, 379, 391, 415, 423, 442, 445, 446, 471, 472, 485, 486, 500, 501, 504, 505, 506, 441, 380};
                var check_user_tvts = intArrayUser.Contains(userTestNapTien);

                int[] intArrayStatusMap = { 29, 47, 65, 74, 77, 80, 142, 30, 48, 66, 75, 78 };
                var check_statusmap = intArrayStatusMap.Contains(sttmap);

                if ((check_user_tvts) & (check_statusmap & levelContact != 8))
                {

                    //-------------Lay thong tin goi hoc
                    string s_package_product = model.ContactInfo.ProductSaleId == 1 ? "TAAM" : "TENUP"; //TAAM chinh la Native (TVTS goi la Native)
                    string s_package_type = model.ContactLevelInfo.FeePackageTypeL7L8 == 28 ? "TT" : "TC";  // table PackageFeeEdu

                    if (model.ContactLevelInfo.FeePackageTypeL7L8 == 29)
                    {
                        model.ContactLevelInfo.LearnNumberDay = 0; //Gói tùy chọn thì ko có ngày học
                    }

                    int i_package_cost;
                    i_package_cost = model.ContactLevelInfo.PricePackageListedId;

                    hasAccPayment = model.ContactLevelInfo.HasAccPayment;

                    string s_package_cost = i_package_cost.ToString();
                    string s_package_unit = Request["DonViTienTe"] == "1" ? "VND" : "Baht";

                    PackageWantToLearn = GetNamePackageLearn(s_package_product, s_package_type, s_package_cost, s_package_unit);

                    if (PackageWantToLearn == "" || PackageWantToLearn == null)
                    {
                        string goiHoc = s_package_type == "TC" ? "tùy chọn" : "thỏa thích";
                        ViewBag.Message = "Không tìm thấy gói học nào có giá " + s_package_cost + " và kiểu học phí " + goiHoc + ". Vui lòng check lại thông tin";
                        return Edit(model.ContactInfo.Id, model);
                    }
                    else if (PackageWantToLearn == "error")
                    {
                        ViewBag.Message = "Gửi API lấy gói học bị lỗi. Vui lòng báo kỹ thuật.";
                        return Edit(model.ContactInfo.Id, model);
                    }
                    else
                    {

                    }
                    //==================== End lay goi hoc ============
                    int iFeeMoneyType = model.ContactLevelInfo.FeeMoneyType;

                    model.ContactLevelInfo.PackageWantToLearn = PackageWantToLearn;
                    int TienNop = Value.ToInt32();

                    model.ContactLevelInfo.FeeEdu = model.ContactLevelInfo.FeeEdu + TienNop; //Cộng thêm tiền vào trường số tiền đã nộp
                    model.ContactLevelInfo.FeeEduYet = model.ContactLevelInfo.PackagePriceSale - model.ContactLevelInfo.FeeEdu;
                    model.ContactLevelInfo.NoteDealsReason = model.ContactLevelInfo.FeeEduDealsReason == 5 ? Request["NoteGiaBanThap"] : "";
                    model.ContactLevelInfo.NumberDealsGroup = model.ContactLevelInfo.FeeEduDealsReason == 4 ? Request["UuDaiNhom"].ToInt32() : 0;

                    if (UserEmail.IsStringNullOrEmpty())
                    {
                        ViewBag.Message = "Email không được để trống";
                        return Edit(model.ContactInfo.Id, model);
                    }
                    double TienDaDong = model.ContactLevelInfo.FeeEdu;
                }

                model.ContactInfo.Mobile1 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile1);
                model.ContactInfo.Mobile2 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile2);
                model.ContactInfo.Mobile3 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile3);
                var duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, model.ContactInfo.Mobile2, string.Empty, string.Empty, string.Empty, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, model.ContactInfo.Mobile3, string.Empty, string.Empty, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, string.Empty, model.ContactInfo.Email, string.Empty, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, string.Empty, string.Empty, model.ContactInfo.Email2, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = ContactRepository.ContactIsDuplicate(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2, model.ContactInfo.Id);
                if (duplicateId > 0 && duplicateId != model.ContactInfo.Id)
                {
                    var contactInfoDb = ContactRepository.GetInfo(duplicateId);
                    if (contactInfoDb == null)
                        ViewBag.Message = "Cập nhật contact bị lỗi do trùng với contact khác trong hệ thống";
                    else
                    {
                        var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == contactInfoDb.UserConsultantId) ??
                                       UserRepository.GetInfo(contactInfoDb.UserConsultantId);

                        ViewBag.Message = user == null
                                              ? "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfoDb.Id + " - " + contactInfoDb.Fullname + " - Level " + contactInfoDb.LevelId + " - TVTS: chưa có ai chăm sóc"
                                              : "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfoDb.Id + " - " + contactInfoDb.Fullname + " - Level " + contactInfoDb.LevelId + " - TVTS: " + user.FullName;
                    }
                    return Edit(model.ContactInfo.Id, model);
                }

                // Get statusmap
                var statusMapInfo = GetStatusMap(model, EmployeeType.Consultant);
                if (statusMapInfo == null)
                {
                    ViewBag.Message = "Cập nhật contact bị lỗi do không tìm thấy trạng thái chăm sóc";
                    return Edit(model.ContactInfo.Id, model);
                }

                // Delete Redis
                StoreData.DeleteRedis(model.ContactInfo.Id);

                // Update
                var entity = new ContactAllInfo
                {
                    Mobile1 = model.ContactInfo.Mobile1,
                    Mobile2 = model.ContactInfo.Mobile2,
                    Mobile3 = model.ContactInfo.Mobile3,
                    ContactInfo = model.ContactInfo,
                    ContactLevelInfo = model.ContactLevelInfo,
                };
                UpdateContactLevelInfo(entity, model, EmployeeType.Consultant);
                UpdateCallHistoryInfo(entity, model, statusMapInfo, EmployeeType.Consultant);
                UpdateContactInfo(entity, model, statusMapInfo, EmployeeType.Consultant);
                UpdateMissedCallInfo(entity);
                ContactDuplicateRepository.UpdateIsNotify(model.ContactInfo.Id);

                #region "Start Checkpoint"
                CheckPointApi checkPointApi = new CheckPointApi();
                var watch = new Stopwatch();
                watch.Start();
                checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "ContactUpdate", "Start", 0);
                #endregion

                var contactId = ContactAllRepository.Update(entity, EmployeeType.Consultant);

                #region "End CheckPoint"
                watch.Stop();
                checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "ContactUpdate", "End", watch.ElapsedMilliseconds);
                #endregion

                //CheckPointApi.createCheckPoint(UserContext.GetCurrentUser().UserName, "ContactUpdate", "End");
                if (contactId > 0)
                {
                    ViewBag.Close = 1;
                    ViewBag.Message = "Cập nhật contact thành công";

                    // Redis
                    StoreData.LoadRedis(entity.ContactInfo.Id);

                    //Nạp tiền
                    if ((check_user_tvts) & (check_statusmap))
                    {
                        try
                        {
                            statusCallApi = NapTienHocVien(Code, UserName, UserPhone, UserEmail, sDiaPhuong, sBank, KieuThanhToan, ChuDuToan, PackageWantToLearn, Value, Reason, hasAccPayment, userTVTS.UserName, packagePriceSale, statusMapInfo.LevelIdNext);

                            if (checkHasAccPayment == true)
                            {
                                model.ContactLevelInfo.HasAccPayment = true;
                            }

                            ContactLevelInfoRepository.Update_HasAccPayment(entity.ContactInfo.Id);

                            if (statusCallApi == false && hasAccPayment == false)
                            {
                                ViewBag.Message = "Lỗi: Contact ID đã tồn tại bên advisor, hasAccPayment is false";
                                return Edit(model.ContactInfo.Id, model);
                            }

                            if (statusCallApi == false)
                            {
                                ViewBag.Message = "Nạp tiền ko thành công, hệ thống nạp tiền bị lỗi. Vui lòng thử nạp tiền lại 1 lần nữa";
                                return Edit(model.ContactInfo.Id, model);
                            }
                            //Lưu vào bảng logmoney
                            var logmoney = new LogsMoneyInfo
                            {
                                TienHVNop = Value.ToInt32(),
                                TienBanGoi = PricePackage,
                                NoteChuyenKhoan = Reason,
                                NguoiTao = userTVTS.UserName,
                                NgayThucThu = DateCollection,
                                KieuThanhToan = TypeAppointment,
                                DiaPhuong = iDia_Phuong,
                                CreateDate = DateTime.Now,
                                ContactId = ContactId,
                                Code = Code,
                                ChuDuToan = iEstimateOwner,
                                TrangThai = statusCallApi,
                                HistoryId = history_Id
                            };
                            MoneyLogsRepository.Create(logmoney);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "Lỗi : " + ex.Message.ToString();
                            return Edit(model.ContactInfo.Id, model);
                        }
                        
                    }
                }
                else
                {
                    ViewBag.Close = 0;
                    ViewBag.Message = "Cập nhật contact bị lỗi, vui lòng thử lại sau";

                    // Redis
                    StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2, model.ContactInfo.Id);
                }

                // Repair call
                try
                {
                    StoreData.WsUpdateCallHistoryInfo(entity.CallHistoryInfo.CallHistoryId);
                }
                catch
                {

                }
                // Return
                return Edit(model.ContactInfo.Id, model);
            }
            catch
            {
                ViewBag.Close = 0;
                ViewBag.Message = "Cập nhật contact bị lỗi, vui lòng thử lại sau";

                // Redis
                try
                {
                    StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2,
                        model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2,
                        model.ContactInfo.Id);
                }
                catch
                {

                }
                return Edit(model.ContactInfo.Id, model);
            }
        }

        public ActionResult EditCollaborator(int id)
        {
            var user = UserContext.GetCurrentUser();
            if (user == null) return RedirectToAction("FilterContactTodayCollaborator", "ContactFilter", new { area = "Admin" });

            var contactInfo = ContactRepository.GetInfo(id);
            if (contactInfo == null) return RedirectToAction("FilterContactTodayCollaborator", "ContactFilter", new { area = "Admin" });

            if (user.GroupCollaboratorType == (int)GroupCollaboratorType.Collaborator && user.UserID != contactInfo.UserCollaboratorId)
                return RedirectToAction("FilterContactTodayCollaborator", "ContactFilter", new { area = "Admin" });

            ViewBag.Id = id;
            ViewBag.LevelTester = LevelTesterRepository.GetAll();
            ViewBag.Quality = QualityRepository.GetAll();
            ViewBag.IsView = user.UserID != contactInfo.UserCollaboratorId ? 1 : 0;
            if (user.GroupCollaboratorType == (int)GroupCollaboratorType.ManagerCollaborator)
                ViewBag.IsView = 0;

            var contactLevelInfo = ContactLevelInfoRepository.GetInfoByContactId(id) ?? new ContactLevelInfo();
            var model = InitModel(contactInfo, contactLevelInfo);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCollaborator(ContactLevelInfoModel model)
        {
            try
            {
                int userCollaboratorId = UserContext.GetCurrentUser().UserID;

                model.ContactInfo.Mobile1 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile1);
                model.ContactInfo.Mobile2 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile2);
                model.ContactInfo.Mobile3 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile3);
                var duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, model.ContactInfo.Mobile2, string.Empty, string.Empty, string.Empty, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, model.ContactInfo.Mobile3, string.Empty, string.Empty, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, string.Empty, model.ContactInfo.Email, string.Empty, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(string.Empty, string.Empty, string.Empty, string.Empty, model.ContactInfo.Email2, model.ContactInfo.Id);
                if (duplicateId == 0) duplicateId = ContactRepository.ContactIsDuplicate(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2);
                if (duplicateId > 0 && duplicateId != model.ContactInfo.Id)
                {
                    var contactInfoDb = ContactRepository.GetInfo(duplicateId);
                    if (contactInfoDb == null)
                        ViewBag.Message = "Cập nhật contact bị lỗi do trùng với contact khác trong hệ thống";
                    else
                    {
                        var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == contactInfoDb.UserCollaboratorId) ??
                                       UserRepository.GetInfo(contactInfoDb.UserCollaboratorId);
                        if (user == null) user = StoreData.ListUser.FirstOrDefault(c => c.UserID == contactInfoDb.UserConsultantId) ??
                                                        UserRepository.GetInfo(contactInfoDb.UserConsultantId); 
                        ViewBag.Message = user == null
                                              ? "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfoDb.Id + " - " + contactInfoDb.Fullname + " - Level " + contactInfoDb.LevelId + "- TVTS/Lọc: chưa có ai chăm sóc"
                                              : "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfoDb.Id + " - " + contactInfoDb.Fullname + " - Level " + contactInfoDb.LevelId + "- TVTS/Lọc: " + user.FullName;
                    }
                    return EditCollaborator(model.ContactInfo.Id);
                }

                // Get statusmap
                var statusMapInfo = GetStatusMap(model, EmployeeType.Collaborator);
                if (statusMapInfo == null)
                {
                    ViewBag.Message = "Cập nhật contact bị lỗi do không tìm thấy trạng thái chăm sóc";
                    return EditCollaborator(model.ContactInfo.Id);
                }

                // Delete Redis
                StoreData.DeleteRedis(model.ContactInfo.Id);

                // Update
                var entity = new ContactAllInfo
                {
                    ContactInfo = model.ContactInfo,
                    Mobile1 = model.ContactInfo.Mobile1,
                    Mobile2 = model.ContactInfo.Mobile2,
                    Mobile3 = model.ContactInfo.Mobile3,
                    ContactLevelInfo = model.ContactLevelInfo,
                };
                UpdateContactLevelInfo(entity, model, EmployeeType.Collaborator);
                UpdateCallHistoryInfo(entity, model, statusMapInfo, EmployeeType.Collaborator);
                UpdateContactInfo(entity, model, statusMapInfo, EmployeeType.Collaborator);
                UpdateMissedCallInfo(entity);

                //Ham update trang thai IsNotify contact tu 1 thanh 0 trong bang ContactDuplicate
                ContactDuplicateRepository.UpdateIsNotify(model.ContactInfo.Id); 

                //Import vao table CC3ContactReport luu danh sach cac contact duoc CTV Loc cham soc len CC3 23/3/2016
                if (statusMapInfo.StatusIdNext == (int)StatusType.AcceptCollaborator)
                {
                    CC3ReportInfo cc3Model = new CC3ReportInfo
                    {
                        FullName = entity.ContactInfo.Fullname,
                        PhoneNumber = entity.Mobile1,
                        Email = entity.ContactInfo.Email,
                        Level = entity.ContactInfo.LevelId,
                        CallInfoCollaborator = entity.ContactInfo.CallInfoCollaborator,
                        HandoverCollaboratorDate = entity.ContactInfo.HandoverCollaboratorDate,
                        CallCollaboratorDate = entity.ContactInfo.CallCollaboratorDate,
                        StatusCareCollaboratorId = entity.ContactInfo.StatusCareCollaboratorId,
                        UserCollaboratorId = userCollaboratorId,
                        CallCount = entity.ContactInfo.CallCount
                    };
                    CC3ContactReportRepository.Create(cc3Model);
                }

                var contactId = ContactAllRepository.Update(entity, EmployeeType.Collaborator);
                if (contactId > 0)
                {
                    ViewBag.Close = 1;
                    ViewBag.Message = "Cập nhật contact thành công";

                    // Redis
                    StoreData.LoadRedis(entity.ContactInfo.Id);
                }
                else
                {
                    ViewBag.Close = 0;
                    ViewBag.Message = "Cập nhật contact bị lỗi, vui lòng thử lại sau";

                    // Redis
                    StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2, model.ContactInfo.Id);
                }

                // Repair call
                try
                {
                    StoreData.WsUpdateCallHistoryInfo(entity.CallHistoryInfo.CallHistoryId);
                }
                catch
                {

                }

                // Return
                return EditCollaborator(model.ContactInfo.Id);
            }
            catch
            {
                ViewBag.Close = 0;
                ViewBag.Message = "Cập nhật contact bị lỗi, vui lòng thử lại sau";

                // Redis
                StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2, model.ContactInfo.Id);
                return EditCollaborator(model.ContactInfo.Id);
            }
        }

        #region Update All
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
                    if (model.TestResultCasecInfo.FullName.IsStringNullOrEmpty())
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
                    if (model.TestResultTopicaInfo.FullName.IsStringNullOrEmpty())
                        model.TestResultTopicaInfo.FullName = model.ContactInfo.Fullname;
                    model.ContactLevelInfo.HasPointTestTopica = true;
                    var topicaAccount = topicaAccounts.FirstOrDefault(c => c.Account == model.TestResultTopicaInfo.Account) ?? new TopicaAccountInfo();
                    model.TestResultTopicaInfo.Account = topicaAccount.Account;
                    model.TestResultTopicaInfo.Password = topicaAccount.Password;

                    model.TestResultTopicaInfo.TestDate = model.TestResultTopicaInfo.TestDate.ToDateTime("d");

                }
                else
                {
                    model.ContactLevelInfo.HasPointTestTopica = false;

                    model.TestResultTopicaInfo = new TestResultTopicaInfo();
                    model.TestResultTopicaInfo.S1 = 0;
                    model.TestResultTopicaInfo.S2 = 0;
                    model.TestResultTopicaInfo.S3 = 0;
                    model.TestResultTopicaInfo.S4 = 0;
                }
                model.ContactLevelInfo.HasPointTestCasecHidden = model.ContactLevelInfo.HasPointTestTopica;

                // TestResultInterviewInfo
                model.TestResultInterviewInfo = TestResultRepository.GetResultInterviewCurent(contactLevelInfo.ContactId);
                if (model.TestResultInterviewInfo != null)
                {
                    if (model.TestResultInterviewInfo.FullName.IsStringNullOrEmpty())
                        model.TestResultInterviewInfo.FullName = model.ContactInfo.Fullname;
                    model.ContactLevelInfo.HasPointInterview = true;
                }
                else model.ContactLevelInfo.HasPointInterview = false;
                model.ContactLevelInfo.HasPointInterviewHidden = model.ContactLevelInfo.HasPointInterview;

                // TestResultLinkSb100Info
                model.TestResultLinkSb100Info = TestResultRepository.GetResultLinkSb100Curent(contactLevelInfo.ContactId);
                if (model.TestResultLinkSb100Info != null)
                {
                    if (model.TestResultLinkSb100Info.FullName.IsStringNullOrEmpty())
                        model.TestResultLinkSb100Info.FullName = model.ContactInfo.Fullname;
                    model.ContactLevelInfo.HasLinkSb100 = true;
                }
                else model.ContactLevelInfo.HasLinkSb100 = false;
                model.ContactLevelInfo.HasLinkSb100Hidden = model.ContactLevelInfo.HasLinkSb100;

                // AppointmentInterviewInfo
                model.AppointmentInterviewInfo = AppointmentInterviewRepository.GetInfo(contactLevelInfo.ContactId);
                model.ContactLevelInfo.HasAppointmentInterview = model.AppointmentInterviewInfo != null;
                model.ContactLevelInfo.HasAppointmentInterviewHidden = model.ContactLevelInfo.HasAppointmentInterview;

                // SB100
                model.ContactLevelInfo.HasSetSb100Hidden = model.ContactLevelInfo.HasSetSb100;

                //PricePackageListedId
               
            }

            if (model.ContactInfo != null)
            {
                //Source
                model.ContactInfo.SourceName = SourceRepository.GetInfo(model.ContactInfo.TypeId).Name;
                // Phone
                var listPhone = PhoneRepository.GetByContact(contactInfo.Id);
                if (listPhone != null)
                {
                    //model.ContactInfo.Mobile1 = listPhone.Count > 0 ? listPhone[0].PhoneNumber : string.Empty;
                    var phoneInfor1 = listPhone.Find(item => item.PhoneType.Equals("1"));
                    var phoneInfor2 = listPhone.Find(item => item.PhoneType.Equals("2"));
                    var phoneInfor3 = listPhone.Find(item => item.PhoneType.Equals("3"));
                    model.ContactInfo.Mobile1 = phoneInfor1 != null ? phoneInfor1.PhoneNumber : string.Empty;
                    model.ContactInfo.Mobile2 = phoneInfor2 != null ? phoneInfor2.PhoneNumber : string.Empty;
                    model.ContactInfo.Mobile3 = phoneInfor3 != null ? phoneInfor3.PhoneNumber : string.Empty;
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

            return model;
        }

        private static StatusMapInfo GetStatusMap(ContactLevelInfoModel model, EmployeeType type)
        {
            if (model.ContactInfo == null) return null;
            var statusMaps = StoreData.ListStatusMap.Count == 0
                                 ? StatusMapRepository.GetAll() ?? new List<StatusMapInfo>()
                                 : StoreData.ListStatusMap;
            switch (type)
            {
                case EmployeeType.Collector:
                    break;
                case EmployeeType.Collaborator:
                    return statusMaps.FirstOrDefault(c => c.Id == model.ContactInfo.StatusMapCollaboratorId);
                case EmployeeType.Consultant:
                    return statusMaps.FirstOrDefault(c => c.Id == model.ContactInfo.StatusMapConsultantId);
            }
            return null;
        }

        private static void UpdateMissedCallInfo(ContactAllInfo entity)
        {
            if (entity == null ||
               entity.ContactInfo == null ||
               entity.ContactLevelInfo == null)
                return;

            entity.MissedCallInfo = new MissedCallInfo
            {
                Status = 2,
                ContactId = entity.ContactInfo.Id,
            };
        }

        private static void UpdateContactLevelInfo(ContactAllInfo entity, ContactLevelInfoModel model, EmployeeType type)
        {
            if (model == null ||
                entity == null ||
                entity.ContactInfo == null ||
                entity.ContactLevelInfo == null)
                return;

            entity.ContactLevelInfo.ContactId = entity.ContactInfo.Id;
            if (!string.IsNullOrEmpty(model.AppointmentTime))
                entity.ContactLevelInfo.AppointmentTime = model.AppointmentTime.ToDateTime();
            switch (type)
            {
                case EmployeeType.Consultant:
                    if (!entity.ContactLevelInfo.HasAppointmentInterview)
                        entity.ContactLevelInfo.HasAppointmentInterview = entity.ContactLevelInfo.HasAppointmentInterviewHidden;
                    if (!entity.ContactLevelInfo.HasPointTestCasec)
                        entity.ContactLevelInfo.HasPointTestCasec = entity.ContactLevelInfo.HasPointTestCasecHidden;
                    if (!entity.ContactLevelInfo.HasPointInterview)
                        entity.ContactLevelInfo.HasPointInterview = entity.ContactLevelInfo.HasPointInterviewHidden;
                    if (!entity.ContactLevelInfo.HasCasecAccount)
                        entity.ContactLevelInfo.HasCasecAccount = entity.ContactLevelInfo.HasCasecAccountHidden;
                    if (!entity.ContactLevelInfo.HasLinkSb100)
                        entity.ContactLevelInfo.HasLinkSb100 = entity.ContactLevelInfo.HasLinkSb100Hidden;
                    if (!entity.ContactLevelInfo.HasSetSb100)
                        entity.ContactLevelInfo.HasSetSb100 = entity.ContactLevelInfo.HasSetSb100Hidden;
                    break;
            }
        }

        private static void UpdateContactInfo(ContactAllInfo entity, ContactLevelInfoModel model, StatusMapInfo statusMapInfo, EmployeeType type)
        {
            // Check
            if (model == null || entity == null || entity.ContactInfo == null)
                return;
            if (entity.ContactLevelInfo == null) entity.ContactLevelInfo = new ContactLevelInfo();

            // Get in Db
            entity.ContactInfo = ContactRepository.GetInfo(model.ContactInfo.Id);

            // AppointmentDate
            var datetime = string.IsNullOrEmpty(model.RecallTime) ? string.Empty : model.RecallTime;
            if (!datetime.IsStringNullOrEmpty())
                datetime += string.IsNullOrEmpty(model.RecallTime24h) ? " 00:00:00" : " " + model.RecallTime24h;
            var appointmentDate = string.IsNullOrEmpty(datetime) ? null : datetime.ToDateTime("dd/MM/yyyy HH:mm:ss");

            switch (type)
            {
                case EmployeeType.Collector:
                    break;
                case EmployeeType.Collaborator:
                    entity.ContactInfo.StatusCareCollaboratorId = statusMapInfo.StatusCareId;
                    entity.ContactInfo.CallCollaboratorDate = entity.CallHistoryInfo.CallTime;
                    entity.ContactInfo.CallInfoCollaborator = model.ContactInfo.CallInfoCollaborator;
                    entity.ContactInfo.StatusMapCollaboratorId = model.ContactInfo.StatusMapCollaboratorId;
                    entity.ContactInfo.CallCount = entity.ContactInfo.StatusCareCollaboratorId == (int)StatusCareType.UnKnown
                                        ? entity.ContactInfo.CallCount + 1
                                        : 0;
                    entity.ContactInfo.AppointmentCollaboratorDate = appointmentDate;
                    break;
                case EmployeeType.Consultant:
                    entity.ContactInfo.StatusCareConsultantId = statusMapInfo.StatusCareId;
                    entity.ContactInfo.CallConsultantDate = entity.CallHistoryInfo.CallTime;
                    entity.ContactInfo.CallInfoConsultant = model.ContactInfo.CallInfoConsultant;
                    entity.ContactInfo.StatusMapConsultantId = model.ContactInfo.StatusMapConsultantId;
                    entity.ContactInfo.CallCount = entity.ContactInfo.StatusCareConsultantId == (int)StatusCareType.UnKnown
                                        ? entity.ContactInfo.CallCount + 1
                                        : 0;
                    entity.ContactInfo.AppointmentConsultantDate = appointmentDate;
                    entity.ContactInfo.ProductSoldId = model.ContactInfo.ProductSoldId;
                    break;
            }
            entity.ContactInfo.Id = model.ContactInfo.Id;
            entity.ContactInfo.Email = model.ContactInfo.Email;
            entity.ContactInfo.Notes = model.ContactInfo.Notes;
            entity.ContactInfo.Email2 = model.ContactInfo.Email2;
            entity.ContactInfo.Gender = model.ContactInfo.Gender;
            entity.ContactInfo.Address = model.ContactInfo.Address;

            //==== 18/02/2016 Khi nghiem thu contact cua Loc, tu dong cap nhat lai UserCollaboratorId
            if (statusMapInfo.StatusIdNext == (int)StatusType.AcceptCollaborator || statusMapInfo.StatusIdNext == (int)StatusType.RecoveryCollaborator) 
            {
                entity.ContactInfo.UserCollaboratorId = 0;   
            }
            entity.ContactInfo.StatusId = statusMapInfo.StatusIdNext;
            if (statusMapInfo.LevelIdNext > 0) entity.ContactInfo.LevelId = statusMapInfo.LevelIdNext;

            entity.ContactInfo.Fullname = model.ContactInfo.Fullname;
            entity.ContactInfo.Birthday = model.Birthday.ToDateTime();
            entity.ContactInfo.CallCount = model.ContactInfo.CallCount;
            entity.ContactInfo.QualityId = model.ContactInfo.QualityId;
            entity.ContactInfo.ProductSellId = model.ContactInfo.ProductSellId;
            entity.ContactInfo.ProductSaleId = model.ContactInfo.ProductSaleId;
            entity.ContactInfo.CreatedBy = UserContext.GetCurrentUser().UserID;
            entity.ContactInfo.NationId = model.ContactInfo.NationId;
            
            entity.ContactInfo.HandoverHistoryConsultantId = model.ContactInfo.HandoverHistoryConsultantId;
        }

        private static void UpdateCallHistoryInfo(ContactAllInfo entity, ContactLevelInfoModel model, StatusMapInfo statusMapInfo, EmployeeType type)
        {
            if (model == null || entity == null || entity.ContactInfo == null)
                return;
            if (entity.ContactLevelInfo == null) entity.ContactLevelInfo = new ContactLevelInfo();

            var user = UserContext.GetCurrentUser();

            // Datetime
            var datetime = string.IsNullOrEmpty(model.RecallTime)
                               ? DateTime.Now.ToString("dd/MM/yyyy")
                               : model.RecallTime;
            datetime += string.IsNullOrEmpty(model.RecallTime24h)
                            ? " 00:00:00"
                            : " " + model.RecallTime24h;

            // CallInfo
            var callInfo = string.Empty;
            switch (type)
            {
                case EmployeeType.Collector:
                    break;
                case EmployeeType.Collaborator:
                    callInfo = model.ContactInfo.CallInfoCollaborator;
                    break;
                case EmployeeType.Consultant:
                    callInfo = model.ContactInfo.CallInfoConsultant;
                    break;
            }
            entity.CallHistoryInfo = new CallHistoryInfo
            {
                StatusUpdate = 1,
                UserLogType = (int)type,
                CreatedBy = user.UserID,
                CallTime = DateTime.Now,
                CallCenterInfo = callInfo,
                CreatedDate = DateTime.Now,
                StatusMapId = statusMapInfo.Id,
                ContactId = model.ContactInfo.Id,
                CallType = (int)CallType.Outcoming,
                LevelId = statusMapInfo.Id == 97 ? 3 : statusMapInfo.LevelIdNext, //97 la status ID CC3 dong y nhan thong tin. 
                CallHistoryId = model.CallHistoryId,
                StatusCareId = statusMapInfo.StatusCareId,
                RecallTime = datetime.ToDateTime("dd/MM/yyyy HH:mm:ss"),
            };
        }
        #endregion


       
    }
}

