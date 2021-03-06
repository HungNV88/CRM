using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Sources;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.Sources
{
    public class SourceRepository
    {
        public static int Create(SourceInfo info)
        {
            return DataProvider.Instance().Sources_Insert( info.Name, info.Code, info.Description,  info.CreatedBy);
        }
        public static void Update(SourceInfo info)
        {
            DataProvider.Instance().Sources_Update( info.SourceId, info.Name, info.Code, info.Description,info.ChangedBy);
        }
        public static void Delete(int sourceId,int deletedby)
        {
            DataProvider.Instance().Sources_Delete(sourceId,deletedby);
        }
        public static SourceInfo GetInfo(int sourceId)
        {
            return ObjectHelper.FillObject<SourceInfo>(DataProvider.Instance().Sources_GetInfo(sourceId));
        }
        public static List<SourceInfo> GetAll()
        {
            return ObjectHelper.FillCollection<SourceInfo>(DataProvider.Instance().Sources_GetAll());
        }
        public static List<SourceInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            var reader=DataProvider.Instance().Sources_Search(keyword, pageIndex, pageSize);
            var val = ObjectHelper.FillCollection<SourceInfo>(reader, false);
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
            return val;
        }
    }        
}
