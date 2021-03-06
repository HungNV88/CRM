using System.Collections.Generic;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Table;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.Table
{
    public class TableRepository
    {
        public static List<TableInfo> GetAll()
        {
            return ObjectHelper.FillCollection<TableInfo>(DataProvider.Instance().Table_GetAll());
        }

        public static void Truncate(string names)
        {
            DataProvider.Instance().Table_Truncate(names);
        }
    }        
}
