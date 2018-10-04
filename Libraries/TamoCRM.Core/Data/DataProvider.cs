using TamoCRM.Core.Providers;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
	{
		private static DataProvider _objProvider = null;
		public static DataProvider Instance()
		{
            if (_objProvider == null)
            {
                var _objProviderConfig = ProviderConfiguration.GetProviderConfiguration("data");
                var _objProviderInfo = (ProviderBase)_objProviderConfig.Providers[_objProviderConfig.DefaultProvider];
                _objProvider = (DataProvider)DataProviderFactory.CreateDataProvider(_objProviderInfo);
            }
            return _objProvider;
        }

        #region abstract methods
        public abstract string Test();
        #endregion
    }
}

