using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Contacts;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.Phones;
using TamoCRM.Services.Contacts;
using TamoCRM.Services.Phones;
using TamoCRM.Services.UserRole;
using TamoCRM.Services.ContactDuplicates;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Contacts;
using TamoCRM.Services.Channels;
using TamoCRM.Domain.ContactDuplicates;
using System.Diagnostics;
using TamoCRM.ImportExcel.Library;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ContactController : AdminController
    {
        [HttpGet]
        public ActionResult CreateHotLine()
        {
            var model = new ContactAddModel();
            ViewBag.Channels = StoreData.ListChannel;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateHotLine(ContactAddModel model)
        {
            int duplicateId;
            try
            {
                duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2);
                if (duplicateId == 0)
                    duplicateId = ContactRepository.ContactIsDuplicate(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2);
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Hệ thống cache bị lỗi, vui lòng thử lại. [" + ex.Message.ToString() + "]";
                return CreateHotLine();
            }
            
            model.ContactInfo.ChannelId = ChannelRepository.GetChannelId(model.ContactInfo.Channel,model.ContactInfo.TypeId,1);
            var entity = new ContactInfo
            {
                CollectorId = 1,
                StatusMapConsultantId = 1,
                StatusCareConsultantId = 1,
                CreatedDate = DateTime.Now,
                LevelId = (int)LevelType.L1,
                RegisteredDate = DateTime.Now,
                Notes = model.ContactInfo.Notes,
                Email = model.ContactInfo.Email,
                Email2 = model.ContactInfo.Email2,
                TypeId = model.ContactInfo.TypeId,
                Gender = model.ContactInfo.Gender,
                Address = model.ContactInfo.Address,
                Fullname = model.ContactInfo.Fullname,
                HandoverConsultantDate = DateTime.Now,
                Birthday = model.Birthday.ToDateTime(),
                ChannelId = model.ContactInfo.ChannelId,
                BranchId = UserContext.GetDefaultBranch(),
                AppointmentConsultantDate = DateTime.Now,
                StatusId = (int)StatusType.HandoverConsultant,
                ProductSellId = model.ContactInfo.ProductSellId,
                CreatedBy = UserContext.GetCurrentUser().UserID,
                UserConsultantId = model.ContactInfo.UserConsultantId,
                Mobile1 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile1),    
                Mobile2 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile2),
            };
            
            var contactInfo = duplicateId == 0 ? null : ContactRepository.GetInfo(duplicateId);
            if (contactInfo == null)
            {
                try
                {
                    // Save Contacts
                    if (entity.UserConsultantId > 0) entity.StatusId = (int)StatusType.HandoverConsultant;
                    entity.Id = ContactRepository.CreateHotline(entity);

                    if (entity.Id > 0)
                    {
                        // Message
                        ViewBag.Message = "Thêm mới contact thành công";
                        // Redis
                        StoreData.LoadRedis(entity.Id);
                    }
                    else
                    {
                        ViewBag.Message = "Thêm mới contact bị lỗi, vui lòng thử lại sau";
                        //Redis
                        StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, string.Empty, model.ContactInfo.Email, model.ContactInfo.Email2);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Thêm mới contact bị lỗi. [" + ex.Message.ToString() + "]";

                    //Redis
                    StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, string.Empty, model.ContactInfo.Email, model.ContactInfo.Email2);
                }
            }
            else
            {
                // Update email, phone
                UpdatePhone(contactInfo.Id, new List<string> { entity.Mobile1, entity.Mobile2, entity.Mobile3 });
                
                // Email
                if (!entity.Email.IsStringNullOrEmpty())
                {
                    if (contactInfo.Email.IsStringNullOrEmpty()) contactInfo.Email = entity.Email;
                    else if (contactInfo.Email != entity.Email && contactInfo.Email2.IsStringNullOrEmpty())
                        contactInfo.Email2 = entity.Email;
                }
                ContactRepository.Update(contactInfo);

                // Log duplicate contact
                var contactDuplicateInfo = new ContactDuplicateInfo
                {
                    ContactId = contactInfo.Id,
                    SourceTypeId = contactInfo.TypeId,
                    Status = contactInfo.StatusId,
                    ImportId = contactInfo.ImportId,
                    DuplicateInfo = contactInfo.Mobile1,
                    CreatedDate = DateTime.Now,
                    IsNotyfiDuplicate = true
                };
                
                //ContactRepository.ContactUpdateDuplicate(entity.Id, entity.TypeId, entity.StatusId);
                ContactDuplicateRepository.Update(contactDuplicateInfo);

                // Message
                var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == contactInfo.UserConsultantId) ?? UserRepository.GetInfo(contactInfo.UserConsultantId);
                ViewBag.Message = user == null
                                      ? "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfo.Id + " - " + contactInfo.Fullname + " - Level " + contactInfo.LevelId + "- TVTS: chưa có ai chăm sóc"
                                      : "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfo.Id + " - " + contactInfo.Fullname + " - Level " + contactInfo.LevelId + "- TVTS: " + user.FullName;

            }
            return CreateHotLine();
        }
        private static void UpdatePhone(int contactId, List<string> mobiles)
        {
            if (mobiles.IsNullOrEmpty()) return;
            var listPhones = PhoneRepository.GetByContact(contactId) ?? new List<PhoneInfo>();
            foreach (var mobile in mobiles
                .Where(c => !c.IsStringNullOrEmpty())
                .Where(c => !listPhones.Exists(p => p.PhoneNumber.Equals(c))))
            {
                if (!listPhones.Exists(c => c.PhoneType.ToInt32() == (int)PhoneType.HomePhone))
                {
                    var phone = new PhoneInfo
                    {
                        IsPrimary = 0,
                        PhoneNumber = mobile,
                        ContactId = contactId,
                        PhoneType = ((int)PhoneType.HomePhone).ToString(),
                    };
                    PhoneRepository.Create(phone);
                    listPhones.Add(phone);
                }
                if (!listPhones.Exists(c => c.PhoneType.ToInt32() == (int)PhoneType.MobilePhone))
                {
                    var phone = new PhoneInfo
                    {
                        IsPrimary = 0,
                        PhoneNumber = mobile,
                        ContactId = contactId,
                        PhoneType = ((int)PhoneType.MobilePhone).ToString(),
                    };
                    PhoneRepository.Create(phone);
                    listPhones.Add(phone);
                }
                if (!listPhones.Exists(c => c.PhoneType.ToInt32() == (int)PhoneType.Tel))
                {
                    var phone = new PhoneInfo
                    {
                        IsPrimary = 0,
                        PhoneNumber = mobile,
                        ContactId = contactId,
                        PhoneType = ((int)PhoneType.Tel).ToString(),
                    };
                    PhoneRepository.Create(phone);
                    listPhones.Add(phone);
                }
            }
        }

        [HttpGet]
        public ActionResult CreateTvts()
        {
            var model = new ContactAddModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateTvts(ContactAddModel model)
        {
            int duplicateId;
            try
            {
                #region "Start Checkpoint"
                CheckPointApi checkPointApi = new CheckPointApi();
                var watch = new Stopwatch();
                watch.Start();
                checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "CheckDuplicate", "Start", 0);
                #endregion

                duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2);
                if (duplicateId == 0)
                    duplicateId = ContactRepository.ContactIsDuplicate(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, model.ContactInfo.Mobile3, model.ContactInfo.Email, model.ContactInfo.Email2);

                #region "End CheckPoint"
                watch.Stop();
                checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "CheckDuplicate", "End", watch.ElapsedMilliseconds);
                #endregion
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Hệ thống bị lỗi, vui lòng thử lại. [" + ex.Message.ToString() + "]";
                return CreateTvts();
            }
            var entity = new ContactInfo
                             {
                                 CreatedDate = DateTime.Now,
                                 LevelId = (int)LevelType.L1,
                                 RegisteredDate = DateTime.Now,
                                 Notes = model.ContactInfo.Notes,
                                 Email = model.ContactInfo.Email,
                                 Email2 = model.ContactInfo.Email2,
                                 TypeId = model.ContactInfo.TypeId,
                                 Gender = model.ContactInfo.Gender,
                                 Address = model.ContactInfo.Address,
                                 Fullname = model.ContactInfo.Fullname,
                                 Birthday = model.Birthday.ToDateTime(),
                                 BranchId = UserContext.GetDefaultBranch(),
                                 CollectorId = model.ContactInfo.CollectorId,
                                 ProductSellId = model.ContactInfo.ProductSellId,
                                 CreatedBy = UserContext.GetCurrentUser().UserID,
                                 Mobile1 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile1),
                                 Mobile2 = Util.CleanAlphabetAndFirstZero(model.ContactInfo.Mobile2),
                                 CampaindTpeId = model.ContactInfo.CampaindTpe.IsStringNullOrEmpty() ? 0 : StaticData.GetCampaindTpeId(model.ContactInfo.CampaindTpe),
                                 StatusMapConsultantId = 1,
                                 StatusCareConsultantId = 1,
                                 HandoverConsultantDate = DateTime.Now,
                                 AppointmentConsultantDate = DateTime.Now,
                                 StatusId = (int)StatusType.HandoverConsultant,
                                 UserConsultantId = UserContext.GetCurrentUser().UserID,
                             };
            
            var contactInfo = duplicateId == 0 ? null : ContactRepository.GetInfo(duplicateId);
            if (contactInfo == null)
            {
                try
                {
                    // Contacts
                    entity.Id = ContactRepository.CreateTvts(entity);
                    if (entity.Id > 0)
                    {
                        // Redis
                        StoreData.LoadRedis(entity.Id);
                        // Message
                        ViewBag.Message = "Thêm mới contact thành công, bạn có thể chăm sóc luôn (" + "<a style=\"cursor: pointer;\" onclick=\"openDialog(" + entity.Id + ")\">Chăm sóc</a>)";
                    }
                    else
                    {
                        ViewBag.Message = "Thêm mới contact bị lỗi, vui lòng thử lại sau";
                        //Redis
                        StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, string.Empty, model.ContactInfo.Email, model.ContactInfo.Email2);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Thêm mới contact lỗi. Vui lòng thử lại. [" + ex.Message.ToString() + "]";
                    //Redis
                    StoreData.DeleteRedis(model.ContactInfo.Mobile1, model.ContactInfo.Mobile2, string.Empty, model.ContactInfo.Email, model.ContactInfo.Email2);
                }
            }
            else
            {
                #region "Start Checkpoint"
                CheckPointApi checkPointApi = new CheckPointApi();
                var watch = new Stopwatch();
                watch.Start();
                checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "ContactUpdateUserId", "Start", 0);
                #endregion

                // Log duplicate
                entity.Id = contactInfo.Id;
                entity.TypeId = contactInfo.TypeId;
                ContactRepository.ContactUpdateDuplicate(entity.Id, entity.TypeId, entity.StatusId);

                if (contactInfo.UserConsultantId == 0)
                {
                    // Update userId
                    entity.BranchId = UserContext.GetDefaultBranch();
                    entity.CreatedBy = UserContext.GetCurrentUser().UserID;
                    ContactRepository.ContactUpdateUserId(entity.Id, entity.UserConsultantId, entity.BranchId, entity.CreatedBy);

                    // Message
                    ViewBag.Message = "Contact đã có trong hệ thống, và chưa được ai chăm sóc, bạn có thể chăm sóc luôn (" + "<a style=\"cursor: pointer;\" onclick=\"openDialog(" + entity.Id + ")\">Chăm sóc</a>)";
                }
                else if (contactInfo.UserConsultantId == entity.UserConsultantId)
                {
                    // Update userId
                    entity.BranchId = UserContext.GetDefaultBranch();
                    entity.CreatedBy = UserContext.GetCurrentUser().UserID;
                    ContactRepository.ContactUpdateUserId(entity.Id, entity.UserConsultantId, entity.BranchId, entity.CreatedBy);
                    ViewBag.Message = "Contact đã có trong hệ thống, bạn có thể chăm sóc luôn (" + "<a style=\"cursor: pointer;\" onclick=\"openDialog(" + entity.Id + ")\">Chăm sóc</a>)";
                }
                else
                {
                    if (contactInfo.StatusId != (int)StatusType.HandoverConsultant)
                    {
                        // Update userId
                        entity.BranchId = UserContext.GetDefaultBranch();
                        entity.UserConsultantId = UserContext.GetCurrentUser().UserID;
                        entity.CreatedBy = UserContext.GetCurrentUser().UserID;
                        ContactRepository.ContactUpdateUserId(entity.Id, entity.UserConsultantId, entity.BranchId, entity.CreatedBy);
                        ViewBag.Message = "Contact đã có trong hệ thống, bạn có thể chăm sóc luôn (" + "<a style=\"cursor: pointer;\" onclick=\"openDialog(" + entity.Id + ")\">Chăm sóc</a>)";
                    }
                    else
                    {
                        var user = StoreData.ListUser.FirstOrDefault(c => c.UserID == contactInfo.UserConsultantId) ??
                                   UserRepository.GetInfo(contactInfo.UserConsultantId);
                        ViewBag.Message = user == null
                                              ? "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfo.Id + " - " + contactInfo.Fullname + " - Level " + contactInfo.LevelId + "- TVTS: chưa có ai chăm sóc"
                                              : "Sđt hoặc Email bạn nhập đã bị trùng với Ứng viên: " + contactInfo.Id + " - " + contactInfo.Fullname + " - Level " + contactInfo.LevelId + "- TVTS: " + user.FullName;
                        //haihm
                        return CreateTvts();
                    }
                }

                #region "End CheckPoint"
                watch.Stop();
                checkPointApi.CheckPointNew(UserContext.GetCurrentUser().UserName, "ContactUpdateUserId", "End", watch.ElapsedMilliseconds);
                #endregion
            }

            return CreateTvts();
        }

        public ActionResult CountDulicateContact()
        {
            return View();
        }
    }
}

