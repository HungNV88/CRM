using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Collectors;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.Collectors
{
    public class CollectorRepository
    {
        public static int Create(CollectorInfo info)
        {
            return DataProvider.Instance().Collectors_Insert(info.Code, info.Name, info.Description, info.CreatedBy);
        }
        public static void Update(CollectorInfo info)
        {
            DataProvider.Instance().Collectors_Update(info.CollectorId, info.Code, info.Name, info.Description, info.ChangedBy);
        }
        public static void Delete(int collectorId, int deletedby)
        {
            DataProvider.Instance().Collectors_Delete(collectorId, deletedby);
        }
        public static CollectorInfo GetInfo(int collectorId)
        {
            return ObjectHelper.FillObject<CollectorInfo>(DataProvider.Instance().Collectors_GetInfo(collectorId));
        }
        public static List<CollectorInfo> GetAll()
        {
            return ObjectHelper.FillCollection<CollectorInfo>(DataProvider.Instance().Collectors_GetAll());
        }
        public static List<CollectorInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            var reader = DataProvider.Instance().Collectors_Search(keyword, pageIndex, pageSize);
            var retval = ObjectHelper.FillCollection<CollectorInfo>(reader, false);
            totalRecord = 0;
            try
            {
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecord = Convert.ToInt32(reader[0]);
                }

            }
            catch (Exception exc)
            {
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
            }
            finally
            {
                //close datareader
                ObjectHelper.CloseDataReader(reader, true);
            }
            return retval;
        }
    }
}
