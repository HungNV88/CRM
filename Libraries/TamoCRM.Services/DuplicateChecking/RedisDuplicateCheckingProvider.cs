using TamoCRM.Core.Caching;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Services.Phones;
using TamoCRM.Domain.Phones;
using System.Collections.Generic;
using TamoCRM.Core;

namespace TamoCRM.Services.DuplicateChecking
{
    public class RedisDuplicateCheckingProvider : CheckDuplicateProvider 
    {
        public override bool IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId, out string info)
        {
            int contactIdDb;
            info = string.Empty;
            string contactInfoDb;
            var check = IsDuplicate(mobile1, mobile2, tel, email, email2, out contactIdDb, out contactInfoDb);
            if (check)
            {
                if (contactId != contactIdDb)
                {
                    info = contactInfoDb;
                    return true;
                }
                return false;
            }
            return false;
        }

        public override bool IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, out int contactId, out string info)
        {
            int retVal;
            contactId = 0;
            info = string.Empty;
            if (!string.IsNullOrEmpty(mobile1))
            {
                retVal = ConvertHelper.ToInt32(CachingProvider.Instance().Get(Constant.NameSystem + mobile1));
                if (retVal != 0) 
                {
                    info = mobile1;
                    contactId = retVal;
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(mobile2))
            {
                retVal = ConvertHelper.ToInt32(CachingProvider.Instance().Get(Constant.NameSystem + mobile2));
                if (retVal != 0)
                {
                    info = mobile2;
                    contactId = retVal;
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(tel))
            {
                retVal = ConvertHelper.ToInt32(CachingProvider.Instance().Get(Constant.NameSystem  + tel));
                if (retVal != 0)
                {
                    info = tel;
                    contactId = retVal;
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(email))
            {
                retVal = ConvertHelper.ToInt32(CachingProvider.Instance().Get(Constant.NameSystem  + email));
                if (retVal != 0)
                {
                    info = email;
                    contactId = retVal;

                    //Trường hợp trùng email và số đt khác với số đã đăng ký từ trước thì thêm số mới này vào số thứ 2
                    List<PhoneInfo> phone_info = new List<PhoneInfo>();
                    phone_info = PhoneRepository.GetByContact(retVal);
                    if(phone_info.Count == 1)
                    {
                        PhoneInfo phone_info_update = new PhoneInfo();
                        phone_info_update.ContactId = retVal;
                        phone_info_update.PhoneType = "2";
                        phone_info_update.IsPrimary = 1;
                        phone_info_update.PhoneNumber = mobile1;
                        PhoneRepository.Create(phone_info_update);

                        //CachingProvider.Instance().Set(Constant.NameSystem + mobile1, retVal.ToString()); // Them sdt 2 do vào redis
                    }              
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(email2))
            {
                retVal = ConvertHelper.ToInt32(CachingProvider.Instance().Get(Constant.NameSystem + email2));
                if (retVal != 0)
                {
                    info = email2;
                    contactId = retVal;
                    return true;
                }
            }
            return false;
        }

        public override int IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2)
        {
            int contactIdDb;
            string contactInfoDb;
            IsDuplicate(mobile1, mobile2, tel, email, email2, out contactIdDb, out contactInfoDb);
            return contactIdDb;
        }

        public override int IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId)
        {
            int contactIdDb;
            string contactInfoDb;
            var check = IsDuplicate(mobile1, mobile2, tel, email, email2, out contactIdDb, out contactInfoDb);
            return check ? (contactId != contactIdDb ? contactIdDb : 0) : 0;
        }
    }
}
