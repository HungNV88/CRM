using System;
using System.Data;

using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void Core_Groups_AddUser(int userId, int groupId, int createdBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Groups_AddUser"), userId, groupId, createdBy);
        }
        public override void Core_Groups_DeleteUser(int userId, int groupId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Groups_DeleteUser"), userId, groupId);
        }

        public override int Core_Groups_Insert(string name, int leaderId, string description, int createdBy, DateTime createdDate, int branchId, int employeeTypeId)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_Groups_Insert"), name, leaderId, description, createdBy, createdDate, branchId, employeeTypeId);
        }

        public override void Core_Groups_Update(int groupId, string name, int leaderId, string description, int createdBy, DateTime createdDate, int branchId, int employeeTypeId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Groups_Update"), groupId, name, leaderId, description, createdBy, createdDate, branchId, employeeTypeId);
        }

        public override void Core_Groups_Delete(int groupId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Groups_Delete"), groupId);
        }

        public override IDataReader Core_Groups_GetInfo(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Groups_GetInfo"), groupId);
        }

        public override IDataReader Core_Groups_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Groups_GetAll"));
        }

        public override IDataReader Core_Groups_GetByUser(int userId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Groups_GetByUser"), userId);
        }

        public override IDataReader Core_Groups_Search(EmployeeType employeeType, string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Groups_Search"), (int)employeeType, keyword, pageIndex, pageSize);
        }
    }
}

