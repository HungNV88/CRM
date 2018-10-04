using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TamoCRM.Core.Providers
{
    public class CheckDuplicateProviderFactory
    {
        public static object CreateCheckDuplicateProvider(ProviderBase _objProviderInfo)
        {
            //ype d1 = Type.GetType("TaskA"); /
            Type _type = Type.GetType(_objProviderInfo.ProviderType, true, true);
            return Activator.CreateInstance(_type, true);
        }
    }
}
