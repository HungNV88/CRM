using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract IDataReader TopicaAccount_Provide(int contactId);
        public abstract IDataReader TopicaAccount_GetInfo(string account);
        public abstract IDataReader TopicaAccount_GetAllByContactId(int id);
        public abstract IDataReader TopicaAccount_Update(int contactId, string Account, string Pass);
        public abstract IDataReader TopicaAccount_GetInfoTopicaTest(string Account);
        public abstract IDataReader TopicaAccount_GetCountTopicaCasec(DateTime? checkTime);

    }
}
