using Microsoft.ApplicationBlocks.Data;
using System.Data;
using TamoCRM.Core.Commons;
using TamoCRM.Core.Data;
using TamoCRM.Core.Providers;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider : DataProvider
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
        public SqlDataProvider()
        {
            ConnectionString = Config.GetConnectionString(((ProviderBase)_providerConfig.Providers[_providerConfig.DefaultProvider]).ProviderAttributes["connectionString"].ToString());
            //   this.ConnectionString = Config.GetConnectionString("localsql");
        }
        public string GetFullyQualifiedName(string spName)
        {
            return dbo + spName;
        }
        public override string Test()
        {
            return (string)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, "select Top(1) Username from Xtt_Users");
        }
    }
}
