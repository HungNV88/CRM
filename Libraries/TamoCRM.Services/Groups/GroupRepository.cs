using System;
using System.Collections.Generic;
using System.Data;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Groups;
using TamoCRM.Core.Commons.Utilities;
namespace TamoCRM.Services.Groups
{
    public class GroupRepository
    {
        public static void AddUser(int userId, int groupId, int createdBy)
        {
            DataProvider.Instance().Core_Groups_AddUser(userId, groupId, createdBy);
        }
        public static void DeleteUser(int userId, int groupId)
        {
            DataProvider.Instance().Core_Groups_DeleteUser(userId, groupId);
        }
        public static int Create(GroupInfo info)
        {
            return DataProvider.Instance().Core_Groups_Insert( info.Name, info.LeaderId, info.Description, info.CreatedBy, info.CreatedDate, info.BranchId, info.EmployeeTypeId);
        }
        public static void Update(GroupInfo info)
        {
            DataProvider.Instance().Core_Groups_Update(info.GroupId, info.Name, info.LeaderId, info.Description, info.CreatedBy, info.CreatedDate, info.BranchId, info.EmployeeTypeId);
        }
        public static void Delete(int groupId)
        {
            DataProvider.Instance().Core_Groups_Delete(groupId);
        }
        public static GroupInfo GetInfo(int groupId)
        {
            return ObjectHelper.FillObject<GroupInfo>(DataProvider.Instance().Core_Groups_GetInfo(groupId));
        }
        public static List<GroupInfo> GetAll()
        {
            return ObjectHelper.FillCollection<GroupInfo>(DataProvider.Instance().Core_Groups_GetAll());
        }
        public static List<GroupInfo> GetByUser(int userId)
        {
            return ObjectHelper.FillCollection<GroupInfo>(DataProvider.Instance().Core_Groups_GetByUser(userId));
        }
        public static List<GroupInfo> Search(EmployeeType employeeType, string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            return FillGroupCollection(DataProvider.Instance().Core_Groups_Search(employeeType, keyword, pageIndex, pageSize), out totalRecord);
        }
        private static List<GroupInfo> FillGroupCollection(IDataReader reader, out int totalRecords)
        {
            var retVal = ObjectHelper.FillCollection<GroupInfo>(reader, false);
            totalRecords = 0;
            try
            {
                //Get the next result (containing the total)
                reader.NextResult();

                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt32(reader[0]);
                }
                                
            }
            catch
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
