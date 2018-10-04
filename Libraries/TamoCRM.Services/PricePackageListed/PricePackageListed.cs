using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;

namespace TamoCRM.Services.PricePackageListed
{
    public class PricePackageListed
    {
        public static string GetNamePackageListed(int id)
        {
            return DataProvider.Instance().PricePackageListed_GetNamePackageListed(id);
        }
    }
}
