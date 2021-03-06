using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int Levels_Insert(string name, string description, int priority, int createdBy);
        public abstract void Levels_Update(int levelId, string name, string description, int priority,int changedBy);
        public abstract void Levels_Delete(int levelId,int deletedby);
        public abstract IDataReader Levels_GetInfo(int levelId);
        public abstract IDataReader Levels_GetAll();
        public abstract IDataReader Levels_GetAll_WithHistoryCount(int groupUserId, int userIds, string hanoverDate, string callTime, int statusCare, int statusMap);
        
        public abstract IDataReader Levels_GetAll_WithContactCount(string userIds, int branchId, EmployeeType employeeType);

        public abstract IDataReader Levels_GetAll_WithContactCountForConsultant(int userId, int branchId, EmployeeType employeeType);
        public abstract IDataReader Levels_Search(string keyword, int pageIndex, int pageSize);
    }
}

