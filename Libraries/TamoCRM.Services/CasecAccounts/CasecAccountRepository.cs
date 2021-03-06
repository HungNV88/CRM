using System.Collections.Generic;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.CasecAccounts;
using TamoCRM.Domain.TopicaAccounts;

namespace TamoCRM.Services.CasecAccounts
{
    public class CasecAccountRepository
    {
        public static List<CasecAccountInfo> Provide(int contactId)
        {
            return ObjectHelper.FillCollection<CasecAccountInfo>(DataProvider.Instance().CasecAccount_Provide(contactId));
        }
        public static CasecAccountInfo GetInfo(int id)
        {
            return ObjectHelper.FillObject<CasecAccountInfo>(DataProvider.Instance().CasecAccount_GetInfo(id));
        }
        public static CasecAccountInfo SelectCasecAccount()
        {
            return ObjectHelper.FillObject<CasecAccountInfo>(DataProvider.Instance().CasecAccount_Get());
        }
        public static List<CasecAccountInfo> GetAllByContactId(int contactId)
        {
            return ObjectHelper.FillCollection<CasecAccountInfo>(DataProvider.Instance().CasecAccount_GetAllByContactId(contactId));
        }
        public static int CheckCountCasecRest()
        {
            return DataProvider.Instance().CasecAccount_CountAllNotUse();
        }

        public static void Insert(string account, string password)
        {
            DataProvider.Instance().CasecAccount_Insert(account, password);
        }

        public static List<CasecAccountInfo> GetAll()
        {
            return ObjectHelper.FillCollection<CasecAccountInfo>(DataProvider.Instance().CasecAccount_GetAll());
        }
    }        
}
