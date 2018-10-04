using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core.Commons;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Core.Providers;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Persitence.CheckDuplicate
{
    public class CheckDuplicateSqlProvider : CheckDuplicateProvider
    {
        #region Private Member
        private string _connectionString;
        private readonly ProviderConfiguration _providerConfig = ProviderConfiguration.GetProviderConfiguration("data");
        private readonly string dbo = Config.Dbo + ".";
        #endregion

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public CheckDuplicateSqlProvider()
        {
            ConnectionString = Config.GetConnectionString(((ProviderBase)_providerConfig.Providers[_providerConfig.DefaultProvider]).ProviderAttributes["connectionString"].ToString());
        }

        public string GetFullyQualifiedName(string spName)
        {
            return dbo + spName;
        }
        
        public override bool IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, out int contactId, out string info)
        {
            contactId = 0;
            info = string.Empty;
            var reader = SqlHelper.ExecuteReader(ConnectionString, "Contacts_GetInfo_Duplicate", mobile1, mobile2, tel, email, email2);
            var contact = ObjectHelper.FillObject<ContactInfo>(reader);
            if (contact != null)
            {
                contactId = contact.Id;
                if (contact.Mobile3 == tel) info = tel;
                else if (contact.Email == email) info = email;
                else if (contact.Email2 == email2) info = email2;
                else if (contact.Mobile1 == mobile1) info = mobile1;
                else if (contact.Mobile2 == mobile2) info = mobile2;
                return true;
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

        public override int IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId)
        {
            int contactIdDb;
            string contactInfoDb;
            var check = IsDuplicate(mobile1, mobile2, tel, email, email2, out contactIdDb, out contactInfoDb);
            return check ? (contactId != contactIdDb ? contactIdDb : 0) : 0;
        }
    }
}
