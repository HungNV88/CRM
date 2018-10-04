using TamoCRM.Core.Providers;

namespace TamoCRM.Core.Data
{
    public abstract class CheckDuplicateProvider
    {
        private static CheckDuplicateProvider _objProvider;

        public static CheckDuplicateProvider Instance()
        {
            if (_objProvider == null)
            {
                var _objProviderConfig = ProviderConfiguration.GetProviderConfiguration("checkDuplicate");
                var _objProviderInfo = (ProviderBase)_objProviderConfig.Providers[_objProviderConfig.DefaultProvider];
                _objProvider = (CheckDuplicateProvider)DataProviderFactory.CreateDataProvider(_objProviderInfo);
            }
            return _objProvider;
        }

        public abstract int IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2);
        public abstract int IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId);
        public abstract bool IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, int contactId, out string info);
        public abstract bool IsDuplicate(string mobile1, string mobile2, string tel, string email, string email2, out int contactId, out string info);
    }
}
