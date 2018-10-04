using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Core;
using TamoCRM.Domain.Catalogs;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.FeeMoneyType
{
    public class FeeMoneyTypeRepository
    {
        public static string GetFeeMoneyTypeWithId(int Id)
        {
            return DataProvider.Instance().FeeMoneyType_GetFeemoneyTypeName(Id);
        }
        public static List<FeeMoneyTypeInfo> GetAll()
        {
            return ObjectHelper.FillCollection<FeeMoneyTypeInfo>(DataProvider.Instance().FeeMoneyType_GetAll());
        }
    }
}
