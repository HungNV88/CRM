using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override IDataReader TopicaAccount_GetCountTopicaCasec(DateTime? checkTime)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TopicaAccount_GetCountTopicaCasec"), checkTime);
        }
        public override IDataReader TopicaAccount_GetInfoTopicaTest(string accountTopica)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TopicaAccount_GetInfoTopicaTest"), accountTopica);
        }
        public override IDataReader TopicaAccount_Provide(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TopicaAccount_Provide"), contactId);
        }

        public override IDataReader TopicaAccount_GetInfo(string account)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TopicaAccount_GetInfo"), account);
        }

        public override IDataReader TopicaAccount_GetAllByContactId(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TopicaAccount_GetAllByContactId"), id);
        }

        public override IDataReader TopicaAccount_Update( int contactId, string Account, string Pass)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("TopicaAccount_Update"), contactId, Account, Pass);
        }
    }
}
