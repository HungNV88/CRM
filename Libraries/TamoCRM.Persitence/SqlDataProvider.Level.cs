using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int Levels_Insert(string name, string description, int priority, int createdBy)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Levels_Insert"), name, description, priority, createdBy);
        }

        public override void Levels_Update(int levelId, string name, string description, int priority, int changedBy)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Levels_Update"), levelId, name, description, priority, changedBy);
        }

        public override void Levels_Delete(int levelId, int deletedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Levels_Delete"), levelId, deletedby);
        }

        public override IDataReader Levels_GetInfo(int levelId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_GetInfo"), levelId);
        }

        public override IDataReader Levels_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_GetAll"));
        }
        
        public override IDataReader Levels_GetAll_WithContactCount(string userIds, int branchId, EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Collaborator:
                {
                    const int statusIds = (int) StatusType.HandoverCollaborator;
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_GetAll_WithContactCount_Collaborator"), userIds, branchId, statusIds);
                }
                case EmployeeType.Consultant:
                {
                    const int statusIds = (int) StatusType.HandoverConsultant;
                    return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_GetAll_WithContactCount_Consultant"), userIds, branchId, statusIds);
                }
            }
            return null;
        }
        //Viet lai ham load so luong danh sach cac level tren lich lam viec cua tvts
        public override IDataReader Levels_GetAll_WithContactCountForConsultant(int userId, int branchId, EmployeeType employeeType)
        {
            const int statusId = (int)StatusType.HandoverConsultant;
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_GetAll_With_ContactCount_Consultant"), userId, branchId, statusId);
        }

        public override IDataReader Levels_GetAll_WithHistoryCount(int groupUserId, int userIds, string hanoverDate, string callTime, int statusCare, int statusMap)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_GetAll_WithHistoryCount"), groupUserId, userIds, hanoverDate, callTime, statusCare, statusMap);
        }

        public override IDataReader Levels_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Levels_Search"), keyword, pageIndex, pageSize);
        }
    }
}

