using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract string FeeMoneyType_GetFeemoneyTypeName(int id);
        public abstract IDataReader FeeMoneyType_GetAll();
    }
}
