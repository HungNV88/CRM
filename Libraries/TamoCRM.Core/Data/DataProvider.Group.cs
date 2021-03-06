using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void Core_Groups_AddUser(int userId, int groupId, int createdBy);
        public abstract void Core_Groups_DeleteUser(int userId, int groupId);
        public abstract int Core_Groups_Insert(string name, int leaderId, string description, int createdBy, DateTime createdDate, int branchId, int employeeTypeId);
        public abstract void Core_Groups_Update(int groupId, string name, int leaderId, string description, int createdBy, DateTime createdDate, int branchId, int employeeTypeId);
        public abstract void Core_Groups_Delete(int groupId);
        public abstract IDataReader Core_Groups_GetInfo(int groupId);
        public abstract IDataReader Core_Groups_GetAll();
        public abstract IDataReader Core_Groups_GetByUser(int userId);
        public abstract IDataReader Core_Groups_Search(EmployeeType employeeType, string keyword, int pageIndex, int pageSize);
    }
}

