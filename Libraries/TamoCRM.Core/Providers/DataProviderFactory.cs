using System;
namespace TamoCRM.Core.Providers
{
    public class DataProviderFactory
    {
        public static object CreateDataProvider(ProviderBase _objProviderInfo)
        {
            Type _type = Type.GetType(_objProviderInfo.ProviderType, true, true);
            return Activator.CreateInstance(_type, true);
        }
    }
}
