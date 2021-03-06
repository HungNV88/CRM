using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TamoCRM.Core.Data;
using TamoCRM.Domain.EducationLevels;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.EducationLevels
{
    public class EducationLevelRepository
    {
        public static int Create(EducationLevelInfo info)
        {
            return DataProvider.Instance().EducationLevels_Insert( info.Name, info.Description, info.CreatedBy, info.CreatedDate);
        }
        public static void Update(EducationLevelInfo info)
        {
            DataProvider.Instance().EducationLevels_Update( info.EducationLevelId, info.Name, info.Description, info.CreatedBy,info.CreatedDate);
        }
        public static void Delete(int educationLevelId)
        {
            DataProvider.Instance().EducationLevels_Delete(educationLevelId);
        }
        public static EducationLevelInfo GetInfo(int educationLevelId)
        {
            return ObjectHelper.FillObject<EducationLevelInfo>(DataProvider.Instance().EducationLevels_GetInfo(educationLevelId));
        }
        public static List<EducationLevelInfo> GetAll()
        {
            return ObjectHelper.FillCollection<EducationLevelInfo>(DataProvider.Instance().EducationLevels_GetAll());
        }
        public static List<EducationLevelInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillEducationLevelCollection(DataProvider.Instance().EducationLevels_Search(keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<EducationLevelInfo> FillEducationLevelCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = new List<EducationLevelInfo>();
            totalRecords = 0;
            try
            {
                while (reader.Read())
                {
                    //fill business object
                    var info = new EducationLevelInfo();
                    info.EducationLevelId = ConvertHelper.ToInt32(reader["EducationLevelId"]);
                    info.Name = ConvertHelper.ToString(reader["Name"]);
                    info.Description = ConvertHelper.ToString(reader["Description"]);
                    info.CreatedBy = ConvertHelper.ToInt32(reader["CreatedBy"]);
                    info.CreatedDate = ConvertHelper.ToDateTime(reader["CreatedBy"]);
                    retVal.Add(info);
                }
                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
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
            return retVal;
        }

    }        
}
