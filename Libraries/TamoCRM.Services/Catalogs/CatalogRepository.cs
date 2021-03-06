using System.Collections.Generic;
using System.Data;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;

namespace TamoCRM.Services.Catalogs
{
    public class CatalogRepository
    {
        public static List<T> GetAll<T>()
        {
            return ObjectHelper.FillCollection<T>(DataProvider.Instance().Catalog_SelectAll<T>());
        }
        public static T GetInfo<T>(int id)
        {
            return ObjectHelper.FillObject<T>(DataProvider.Instance().Catalog_SelectOne<T>(id));
        }
        public static int Create<T>(T info)
        {
            var name = info.GetType().GetProperty("Name").GetValue(info, null).ToString();
            var propertyOrderTime = info.GetType().GetProperty("OrderTime");
            if (propertyOrderTime != null)
            {
                var objOrder = info.GetType().GetProperty("OrderTime").GetValue(info, null).ToString();
                return DataProvider.Instance().Catalog_Insert<T>(name, objOrder);
            }
            return DataProvider.Instance().Catalog_Insert<T>(name);
        }
        public static void Update<T>(T info)
        {
            var id = info.GetType().GetProperty("Id").GetValue(info, null).ToInt32();
            var name = info.GetType().GetProperty("Name").GetValue(info, null).ToString();
            var propertyOrderTime = info.GetType().GetProperty("OrderTime");
            if (propertyOrderTime != null)
            {
                var objOrder = info.GetType().GetProperty("OrderTime").GetValue(info, null).ToString();
                DataProvider.Instance().Catalog_Update<T>(id, name, objOrder);
            }
            else DataProvider.Instance().Catalog_Update<T>(id, name);
        }
        public static void Delete<T>(int id)
        {
            DataProvider.Instance().Catalog_Delete<T>(id);
        }
        public static List<T> Search<T>(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillCollection<T>(DataProvider.Instance().Catalog_Search<T>(keyword, pageIndex, pageSize), out totalRecord);
        }

        public static List<UserRoleInfo> UserRoleGetAll()
        {
            return ObjectHelper.FillCollection<UserRoleInfo>(DataProvider.Instance().UserRole_SelectAll());
        }
        public static List<UserGroupInfo> UserGroupGetAll()
        {
            return ObjectHelper.FillCollection<UserGroupInfo>(DataProvider.Instance().UserGroup_SelectAll());
        }
        public static List<UserBranchInfo> UserBranchGetAll()
        {
            return ObjectHelper.FillCollection<UserBranchInfo>(DataProvider.Instance().UserBranch_SelectAll());
        }
        private static List<T> FillCollection<T>(IDataReader reader, out int totalRecords)
        {
            List<T> retVal;
            totalRecords = 0;
            try
            {
                retVal = ObjectHelper.FillCollection<T>(reader, false);
                
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
