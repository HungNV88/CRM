using Microsoft.ReportingServices.ReportRendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Mail;
using System.Web.Http;
using TamoCRM.Call;
using TamoCRM.Persitence;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.AppointmentInterviews;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Domain.Contacts;
using TamoCRM.Services.AppointmentInterview;
using TamoCRM.Services.Catalogs;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.AppointmentInterview
{
    
    public class AppointmentInterviewController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<AppointmentInterviewInfo> Get()
        {
            return AppointmentInterviewRepository.GetAll();
        }

        // GET api/<controller>/5
        public AppointmentInterviewInfo Get(int id)
        {
            return AppointmentInterviewRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public Result Schedule(FormDataCollection form)
        {
            var contactId = form.Get("contactId").ToInt32();
            var entityDbs = AppointmentInterviewRepository.GetAllByContactId(contactId);
            string noteConsulant = "";
            var userConsulant = UserContext.GetCurrentUser();
            noteConsulant = userConsulant.IsDATester == false ? form.Get("notes") : "VIP+: " + form.Get("notes");
            var entity = new AppointmentInterviewInfo
                     {
                         ContactId = contactId,
                         Notes = noteConsulant,
                         NotesCM = form.Get("notesCM"),
                         UserId = userConsulant.UserID,
                         TimeSlotId = form.Get("timeSlotId").ToInt32(),
                         TeacherTypeId = form.Get("teacherTypeId").ToInt32(),
                         LevelTesterId = form.Get("levelTesterId").ToInt32(),
                         StatusInterviewId = form.Get("statusInterviewId").ToInt32(),
                         CasecAccountId = entityDbs.IsNullOrEmpty() ? 0 : entityDbs[0].CasecAccountId,
                         AppointmentDate = form.Get("appointmentDate").ToDateTime("ddMMyyyy") ?? DateTime.Now,
                     };
            var reSchedule = form.Get("reSchedule").ToBoolean();
            entity.StatusInterviewId = reSchedule
                ? (int)StatusInterviewType.HocVienDoiLichPhongVan
                : (int)StatusInterviewType.DaDatHang;
            var result = reSchedule
                ? (new HelpUtils()).SendChangeInterviewInfo(entity)
                : (new HelpUtils()).SendInterviewInfo(entity, userConsulant.IsDATester);
            if (result.Code == 0)
            {
                result.Tag = entity.StatusInterviewId;
                AppointmentInterviewRepository.Create(entity);
                ContactLevelInfoRepository.UpdateHasAppointmentInterview(entity.ContactId, true);
            }
            return result;
        }

        [HttpPost]
        public Result CancelSchedule(FormDataCollection form)
        {
            var contactId = form.Get("contactId").ToInt32();
            var entityDbs = AppointmentInterviewRepository.GetAllByContactId(contactId) ?? new List<AppointmentInterviewInfo>();
            var entity = entityDbs.OrderBy(c => c.Id).LastOrDefault();
            if (entity != null)
            {
                var result = (new HelpUtils()).SendCancelInterview(entity.ContactId);
                if (result.Code == 0)
                {
                    entity.StatusInterviewId = (int)StatusInterviewType.HocVienHuyPhongVan;
                    AppointmentInterviewRepository.Update(entity);
                    ContactLevelInfoRepository.UpdateHasAppointmentInterview(entity.ContactId, false);
                }
                return result;
            }
            return new Result
                   {
                       Code = 1,
                       Description = "Lịch không tồn tại, bạn phải đặt lịch trước khi hủy lịch"
                   };
        }

        [HttpPost]
        public Result RequestSb100(FormDataCollection form)
        {
            var user = UserContext.GetCurrentUser();
            var contactId = form.Get("contactId").ToInt32();
            var feePackageType = form.Get("feePackageType").ToInt32();
            var hocvien_name = form.Get("hocvien_name").ToString();
            //var tvts_name_id = form.Get("tvts_name").ToInt32();
            string tvts_name = user.FullName.ToString();
            var result = (new HelpUtils()).SendRequestSB100(contactId, feePackageType);
            if (result.Code == 0)
            {
                ContactLevelInfoRepository.UpdateHasSetSb100(contactId, true);
            }

            //Send Mail
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("sonvn@topica.edu.vn");
            mail.From = new MailAddress("crmnative@topica.edu.vn", "Thông báo đặt hàng SB100", System.Text.Encoding.UTF8);
            mail.Subject = "Đặt SB100";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            
            //string hocvien_name = "";

            mail.Body = tvts_name + " muốn đặt hàng SB100 cho học viên " + hocvien_name;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("crmnative@topica.edu.vn", "thanghva");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
            return result;
        }
        [HttpPost]
        public Result RequestSb100Topica(FormDataCollection form)
        {
            var user = UserContext.GetCurrentUser();
            var contactId = form.Get("contactId").ToInt32();
            var feePackageType = form.Get("feePackageType").ToInt32();
            var hocvien_name = form.Get("hocvien_name").ToString();
            //var tvts_name_id = form.Get("tvts_name").ToInt32();
            string tvts_name = user.FullName.ToString();
            var result = (new HelpUtils()).SendRequestSB100Topica(contactId, feePackageType);
            if (result.Code == 0)
            {
                ContactLevelInfoRepository.UpdateHasSetSb100Topica(contactId, true);
            }

            //Send Mail
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("sonvn@topica.edu.vn");
            mail.From = new MailAddress("crmnative@topica.edu.vn", "Thông báo đặt hàng SB100", System.Text.Encoding.UTF8);
            mail.Subject = "Đặt SB100";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            //string hocvien_name = "";

            mail.Body = tvts_name + " muốn đặt hàng SB100 cho học viên " + hocvien_name;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("crmnative@topica.edu.vn", "thanghva");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
            return result;
        }
        [HttpPost]
        public Result RequestLinkSb100(FormDataCollection form)
        {
            var contactId = form.Get("contactId").ToInt32();
            var result = new Result
                         {
                             Code = 0,
                         };
            if (result.Code == 0)
            {
                ContactLevelInfoRepository.UpdateHasLinkSb100(contactId, true);
            }
            return result;
        }
        [HttpPost]
        public Result RequestLinkSb100Topica(FormDataCollection form)
        {
            var contactId = form.Get("contactId").ToInt32();
            var result = new Result
            {
                Code = 0,
            };
            if (result.Code == 0)
            {
                ContactLevelInfoRepository.UpdateHasLinkSb100Topica(contactId, true);
            }
            return result;
        }
        public List<AppointmentInterviewInfo> GetAllByContactId(int contactId)
        {
            var list = AppointmentInterviewRepository.GetAllByContactId(contactId);
            if (!list.IsNullOrEmpty())
            {
                foreach (var item in list)
                {
                    item.AppointmentDateString = item.AppointmentDate.HasValue
                        ? item.AppointmentDate.Value.ToString("dd/MM/yyyy")
                        : string.Empty;
                    var timeSlot = CatalogRepository.GetInfo<TimeSlotInfo>(item.TimeSlotId);
                    item.TimeSlotName = timeSlot != null ? timeSlot.Name : string.Empty;

                    var teacherType = CatalogRepository.GetInfo<TeacherTypeInfo>(item.TeacherTypeId);
                    item.TeacherTypeName = teacherType != null ? teacherType.Name : string.Empty;

                    item.NotesCM = item.NotesCM.IsStringNullOrEmpty() ? string.Empty : item.NotesCM;
                    item.StatusInterviewName = ObjectExtensions.GetEnumDescription((StatusInterviewType)item.StatusInterviewId);
                }
                return list;
            }
            return null;
        }
    }
}

