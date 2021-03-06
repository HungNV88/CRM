using System;
using System.Collections.Generic;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Levels;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.Levels
{
    public class LevelRepository
    {
        public static int Create(LevelInfo info)
        {
            return DataProvider.Instance().Levels_Insert( info.Name, info.Description, info.Priority,info.CreatedBy);
        }
        public static void Update(LevelInfo info)
        {
            DataProvider.Instance().Levels_Update( info.LevelId, info.Name, info.Description, info.Priority,info.ChangedBy);
        }
        public static void Delete(int levelId,int deletedby)
        {
            DataProvider.Instance().Levels_Delete(levelId,deletedby);
        }
        public static LevelInfo GetInfo(int levelId)
        {
            return ObjectHelper.FillObject<LevelInfo>(DataProvider.Instance().Levels_GetInfo(levelId));
        }
        public static List<LevelInfo> GetAll()
        {
            return ObjectHelper.FillCollection<LevelInfo>(DataProvider.Instance().Levels_GetAll());
        }

        public static List<LevelInfo> GetAllWithHistoryCount(int groupUserId, int userIds, string hanoverDate, string callTime, int statusCare, int statusMap)
        {
            return ObjectHelper.FillCollection<LevelInfo>(DataProvider.Instance().Levels_GetAll_WithHistoryCount(groupUserId, userIds, hanoverDate, callTime, statusCare, statusMap));
        }
        public static List<LevelInfo> GetAllWithContactCount(string userIds, int branchId, EmployeeType employeeType)
        {
            return ObjectHelper.FillCollection<LevelInfo>(DataProvider.Instance().Levels_GetAll_WithContactCount(userIds, branchId, employeeType));
        }

        //Code lại chức năng load số lượng level cho Lịch làm việc TVTS 
        public static List<LevelInfo> GetAllWithContactCountForConsultant(int userId, int branchId, EmployeeType employeeType)
        {
            return ObjectHelper.FillCollection<LevelInfo>(DataProvider.Instance().Levels_GetAll_WithContactCountForConsultant(userId, branchId, employeeType));
        }

        public static List<LevelInfo> Search(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            var reader=DataProvider.Instance().Levels_Search(keyword, pageIndex, pageSize);
            var val = ObjectHelper.FillCollection<LevelInfo>(reader, false);
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
            catch (Exception)
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
