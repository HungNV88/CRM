using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Data;
using TamoCRM.Domain.ContactTmps;
using TamoCRM.Core.Commons.Utilities;
using System;

namespace TamoCRM.Services.ContactTmps
{
    public class ContactTmpRepository
    {
        public static void Delete(int contactId)
        {
            DataProvider.Instance().ContactTmps_Delete(contactId);
        }
        public static ContactTmpInfo GetInfo(int contactId)
        {
            return ObjectHelper.FillObject<ContactTmpInfo>(DataProvider.Instance().ContactTmps_GetInfo(contactId));
        }
        public static List<ContactTmpInfo> GetAll()
        {
            return ObjectHelper.FillCollection<ContactTmpInfo>(DataProvider.Instance().ContactTmps_GetAll());
        }
        public static List<ContactTmpInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactTmpCollection(DataProvider.Instance().ContactTmps_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        public static List<ContactTmpInfo> Search(int branchId, int collector, int channelId, int importId,DateTime? importDate, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillContactTmpCollection(DataProvider.Instance().ContactTmps_Search(branchId, collector, channelId, importId, importDate, pageIndex, pageSize), out totalRecord);
        }
        private static List<ContactTmpInfo> FillContactTmpCollection(IDataReader reader, out int totalRecords)
        {
            List<ContactTmpInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<ContactTmpInfo>(reader, false);

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
