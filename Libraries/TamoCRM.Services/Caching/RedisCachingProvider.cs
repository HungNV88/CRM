using StackExchange.Redis;
using TamoCRM.Core.Caching;
using TamoCRM.Core.Providers;

namespace TamoCRM.Services.Caching
{
    public class RedisCachingProvider : CachingProvider 
    {
        #region Private Member
        private string _connectionString;
        public ConnectionMultiplexer Redis { get; set; }
        private readonly ProviderConfiguration _providerConfig = ProviderConfiguration.GetProviderConfiguration("caching");
        #endregion

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public RedisCachingProvider()
        {
            ConnectionString = ((ProviderBase)_providerConfig.Providers[_providerConfig.DefaultProvider]).ProviderAttributes["connectionString"].ToString();
            Redis = ConnectionMultiplexer.Connect(ConnectionString);
        }

        public override string Get(string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            var db = Redis.GetDatabase();
            return db.StringGet(key);
        }

        public override void Set(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;
            var db = Redis.GetDatabase();
            db.StringSet(key, value);
        }

        public override bool Del(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key)) return true;
                var db = Redis.GetDatabase();
                return db.KeyDelete(key);
            }
            catch
            {
                return false;
            }
        }
    }
}
