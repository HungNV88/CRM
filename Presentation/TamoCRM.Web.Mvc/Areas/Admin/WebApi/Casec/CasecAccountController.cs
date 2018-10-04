using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Call;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Services.CasecAccounts;
using TamoCRM.Services.ContactLevelInfos;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Casec;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Casec
{
    public class CasecAccountController : CustomApiController
    {
        public CasecAccountInfo SelectCasecAccount()
        {
            var info = CasecAccountRepository.SelectCasecAccount();
            var model = new CasecAccountInfo
                        {
                            ContactId = info.ContactId,
                            Account = info.Account,
                            Password = info.Password
                        };

            //gọi link của CongNN truyền vào các tham số contactId, Account, Password trên

            return model;
        }

        [HttpPost]
        public List<CasecAccountInfo> Provide(FormDataCollection form)
        {
            var contactId = form.Get("contactId").ToInt32();
            var list = CasecAccountRepository.Provide(contactId);
            if (!list.IsNullOrEmpty())
            {
                    foreach (var item in list)
                    {
                        item.DateTimeString = item.DateTime.ToString("dd/MM/yyyy");
                        item.CreatedDateString = item.CreatedDate.ToString("dd/MM/yyyy");
                        item.StatusString = ObjectExtensions.GetEnumDescription((StatusCasecType)item.StatusCasecAccountId);

                        // Call WS
                        if (item.StatusCasecAccountId == (int) StatusCasecType.Used)
                        {
                            var result = (new HelpUtils()).SendCasecAccount(item);
                            if (result.Code == 0)
                            {
                                ContactLevelInfoRepository.UpdateHasCasecAccount(contactId, true);
                            }
                        }
                    }
                return list;
            }
            return null;
        }

        public List<CasecAccountInfo> GetAllByContactId(int contactId)
        {
            var list = CasecAccountRepository.GetAllByContactId(contactId);
            if (!list.IsNullOrEmpty())
            {
                foreach (var item in list)
                {
                    item.DateTimeString = item.DateTime.ToString("dd/MM/yyyy");
                    item.CreatedDateString = item.CreatedDate.ToString("dd/MM/yyyy");
                    item.StatusString = ObjectExtensions.GetEnumDescription((StatusCasecType)item.StatusCasecAccountId);
                }
                return list;
            }
            return null;
        }

        //Get tất cả các tài khoản Casec chưa được sử dụng
        [HttpGet]
        public int CheckSumCasecAccount() 
        {
            int count = CasecAccountRepository.CheckCountCasecRest();
            return count;
        }
    }
}