using TamoCRM.Core.Providers;

namespace TamoCRM.Core.Caching
{
    public abstract class CachingProvider
    {
        private static CachingProvider _objProvider;
        
        public static CachingProvider Instance()
        {
            if (_objProvider == null)
            {
                var _objProviderConfig = ProviderConfiguration.GetProviderConfiguration("caching");
                var _objProviderInfo = (ProviderBase)_objProviderConfig.Providers[_objProviderConfig.DefaultProvider];
                _objProvider = (CachingProvider)DataProviderFactory.CreateDataProvider(_objProviderInfo);
            }
            return _objProvider;
        }

        public abstract bool Del(string key);

        public abstract string Get(string key);

        public abstract void Set(string key, string value);
    }
}
