using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Contacts;
using TamoCRM.Domain.TopicaAccounts;


namespace TamoCRM.Services.TopicaAccounts
{
    public class TopicaAccountRepository
    {
        public static List<ContactInfo> GetInfoTopicaTest(string account)
        {
            return ObjectHelper.FillCollection<ContactInfo>(DataProvider.Instance().TopicaAccount_GetInfoTopicaTest(account));
        }
      
        public static List<TopicaAccountInfo> Provide(int contactId)
        {
            return ObjectHelper.FillCollection<TopicaAccountInfo>(DataProvider.Instance().TopicaAccount_Provide(contactId));
        }

        public static TopicaAccountInfo GetInfo(string account)
        {
            return ObjectHelper.FillObject<TopicaAccountInfo>(DataProvider.Instance().TopicaAccount_GetInfo(account));
        }

        public static List<TopicaAccountInfo> GetAllByContactId(int contactId)
        {
            return ObjectHelper.FillCollection<TopicaAccountInfo>(DataProvider.Instance().TopicaAccount_GetAllByContactId(contactId));
        }

        public static List<TopicaAccountInfo> UpdateTopicaAccount(int contactId, string Account, string Pass)
        {
            return ObjectHelper.FillCollection<TopicaAccountInfo>(DataProvider.Instance().TopicaAccount_Update(contactId, Account, Pass));          
        }

        public static TopicaCasec GetCountTopicaCasec(DateTime? checktime)
        {
            return ObjectHelper.FillObject<TopicaCasec>(DataProvider.Instance().TopicaAccount_GetCountTopicaCasec(checktime));
        }
    }
}
