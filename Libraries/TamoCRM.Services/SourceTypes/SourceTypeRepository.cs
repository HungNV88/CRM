using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Data;
using TamoCRM.Domain.SourceTypes;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Services.SourceTypes
{
    public class SourceTypeRepository
    {
        public static int Create(SourceTypeInfo info)
        {
            return DataProvider.Instance().SourceTypes_Insert(info.Code, info.Name, info.Description, info.IsCheckDuplicate, info.IsCheckUpdate, info.IsShowHotLine, info.IsShowConsultant, info.CreatedBy, info.CreatedDate, info.ChangedBy, info.ChangedDate);
        }
        public static void Update(SourceTypeInfo info)
        {
            DataProvider.Instance().SourceTypes_Update(info.SourceTypeId, info.Code, info.Name, info.Description, info.IsCheckDuplicate, info.IsCheckUpdate, info.IsShowHotLine, info.IsShowConsultant, info.CreatedBy, info.CreatedDate, info.ChangedBy, info.ChangedDate);
        }
        public static void Delete(int sourceTypeId)
        {
            DataProvider.Instance().SourceTypes_Delete(sourceTypeId);
        }
        public static SourceTypeInfo GetInfo(int sourceTypeId)
        {
            return ObjectHelper.FillObject<SourceTypeInfo>(DataProvider.Instance().SourceTypes_GetInfo(sourceTypeId));
        }

        public static SourceTypeInfo GetInfo(string code)
        {
            return ObjectHelper.FillObject<SourceTypeInfo>(DataProvider.Instance().SourceTypes_GetInfo(code));
        }

        public static List<ContactTestInfo> GetAllTest()
        {
            return ObjectHelper.FillCollection<ContactTestInfo>(DataProvider.Instance().SourceTypes_GetAllTest());
        }
        public static List<SourceTypeInfo> GetAll()
        {
            return ObjectHelper.FillCollection<SourceTypeInfo>(DataProvider.Instance().SourceTypes_GetAll());
        }
        public static List<SourceTypeInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillSourceTypCollection(DataProvider.Instance().SourceTypes_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<SourceTypeInfo> FillSourceTypCollection(IDataReader reader, out int totalRecords)
        {
            List<SourceTypeInfo> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<SourceTypeInfo>(reader, false);

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
