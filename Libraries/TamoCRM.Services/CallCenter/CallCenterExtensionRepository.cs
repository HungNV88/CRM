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
    public class CallCenterExtensionRepository
    {
        public static List<CallCenterExtensionInfo> GetAll()
        {
            return ObjectHelper.FillCollection<CallCenterExtensionInfo>(DataProvider.Instance().CallCenter_Extension_GetAll());
        }

        public static List<CallCenterExtensionInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillCallCenterCollection(DataProvider.Instance().CallCenter_Extension_Search(keyword, pageIndex, pageSize), out totalRecord);
        }


        private static List<CallCenterExtensionInfo> FillCallCenterCollection(IDataReader reader, out int totalRecords)
        {
            List<CallCenterExtensionInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<CallCenterExtensionInfo>(reader, false);

                reader.NextResult();
                if (reader.Read()) totalRecords = reader[0].ToInt32();
            }
            finally
            {
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retVal;
        }


        public static void Insert(CallCenterExtensionInfo info)
        {
            //info.Id = DataProvider.Instance().CallCenter_Insert(info.Name, info.PhoneNumber, info.UseFor, info.EndPoint, info.Token, info.Port, info.CreatedBy);
        }
    }
}
