using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactTmps;
using TamoCRM.Services.ContactTmps;
using TamoCRM.Services.Contacts;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.ContactTmps;
using System;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.ContactTmps
{
    public class ContactTmpController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<ContactTmpInfo> Get()
        {
            return ContactTmpRepository.GetAll();
        }
       
        // GET api/<controller>
        public ContactTmpListModel Get(int collectorId, int channelId, int importId, string importDate, int page, int rows)
        {
            var importFDate = importDate.ToDateTime("ddMMyyyy");
            var branchId = UserContext.GetDefaultBranch();
            int totalRecords;
            var model = new ContactTmpListModel
                            {
                                Rows = ContactTmpRepository.Search(branchId, collectorId, channelId, importId, importFDate, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords / rows) + 1,
                                Records = rows,
                                UserData = totalRecords
                            };
            return model;
        }

        // GET api/<controller>/5
        public ContactTmpInfo Get(int id)
        {
            return ContactTmpRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            // Check valid
            var mobile1 = Util.CleanAlphabetAndFirstZero(form.Get("Mobile1"));
            var mobile2 = Util.CleanAlphabetAndFirstZero(form.Get("Mobile2"));
            var mobile3 = Util.CleanAlphabetAndFirstZero(form.Get("Mobile3"));
            if (mobile1.IsStringNullOrEmpty() && mobile2.IsStringNullOrEmpty() && mobile3.IsStringNullOrEmpty())
                return "SĐT không được rỗng";
            if (!mobile1.IsStringNullOrEmpty() && !ContactValidHelper.IsMobileValid(mobile1))
                return "SĐT 1 không hợp lệ";
            if (!mobile2.IsStringNullOrEmpty() && !ContactValidHelper.IsMobileValid(mobile2))
                return "SĐT 2 không hợp lệ";
            if (!mobile3.IsStringNullOrEmpty() && !ContactValidHelper.IsMobileValid(mobile3))
                return "SĐT 3 không hợp lệ";
            var email = form.Get("Email");
            if (!email.IsStringNullOrEmpty() && !ContactValidHelper.IsValidEmail(email))
                return "Email 1 không hợp lệ";
            var email2 = form.Get("Email2");
            if (!email2.IsStringNullOrEmpty() && !ContactValidHelper.IsValidEmail(email2))
                return "Email 2 không hợp lệ";

            // Check cache
            int duplicateId;
            try
            {
                duplicateId = CheckDuplicateProvider.Instance().IsDuplicate(mobile1, mobile2, mobile3, email, email2);
                if (duplicateId == 0) duplicateId = ContactRepository.ContactIsDuplicate(mobile1, mobile2, mobile3, email, email2);
            }
            catch
            {
                return "Hệ thống cache bị lỗi, vui lòng thử lại sau";
            }
            if (duplicateId > 0)
            {
                return "Contact đã có trong hệ thống, vui lòng thử lại sau";
            }

            var retVal = string.Empty;
            var operation = form.Get("oper");
            var userId = UserContext.GetCurrentUser().UserID;
            var id = ConvertHelper.ToInt32(form.Get("Id").Split(',')[0]);
            if (string.IsNullOrEmpty(operation)) return "Cập nhật contact bị lỗi, vui lòng thừ lại sau";

            switch (operation)
            {
                case "edit":
                    var info = ContactTmpRepository.GetInfo(id);
                    if (info != null)
                    {
                        try
                        {
                            info.Email = email;
                            info.Email2 = email2;
                            info.Mobile1 = mobile1;
                            info.Mobile2 = mobile2;
                            info.Mobile3 = mobile3;
                            info.Fullname = form.Get("Fullname");
                            info.Birthday = form.Get("Birthday").ToDateTime();

                            // Delete ContactTmp
                            ContactTmpRepository.Delete(info.Id);

                            // Create Contacts
                            var contact = info;
                            contact.CreatedBy = userId;
                            contact.UserErrorId = userId;
                            contact.Id = ContactRepository.CreateTmp(contact);

                            //Redis
                            StoreData.LoadRedis(contact.Id);
                            return "Cập nhật thành công";
                        }
                        catch
                        {
                            return "Cập nhật contact bị lỗi, vui lòng thừ lại sau";
                        }
                    }
                    break;
            }
            return retVal;
        }
    }
}

