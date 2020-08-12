using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Data;
using TamoCRM.Domain.CallCenter;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Commons.Extensions;

namespace TamoCRM.Services.CallCenter
{
    public class CallCenterRepository
    {
        public static List<CallCenterInfo> GetAll()
        {
            return ObjectHelper.FillCollection<CallCenterInfo>(DataProvider.Instance().CallCenter_GetAll());
        }

        public static List<CallCenterInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillCallCenterCollection(DataProvider.Instance().SourceTypes_Search(keyword, pageIndex, pageSize), out totalRecord);
        }


        private static List<CallCenterInfo> FillCallCenterCollection(IDataReader reader, out int totalRecords)
        {
            List<CallCenterInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<CallCenterInfo>(reader, false);

                reader.NextResult();
                if (reader.Read()) totalRecords = reader[0].ToInt32();
            }
            finally
            {
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }
    }
}
