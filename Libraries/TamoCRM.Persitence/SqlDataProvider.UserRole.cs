using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TamoCRM.Core;
using System;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void User_ClearGroups(int userId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_ClearGroups"), userId);
        }
        public override int Users_CheckPermisions(int userId, int branchId, string area, string controller, string action, string param)
        {
            return (int)(SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_RolePermisions_Check"), userId, branchId, area, controller, action, param));
        }
        public override int Role_Insert(string name, string description, int createdby)
        {
            return (int)(SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_Roles_Insert"), name, description, createdby));
            //return 0;
        }
        public override void Role_Update(int id, string name, string description, int changedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Roles_Update"), id, name, description, changedby);
        }

        public override void Role_Delete(int id, int deletedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Roles_Delete"), id, deletedby);
        }

        public override IDataReader Role_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Roles_GetInfo"), id);
        }

        public override IDataReader Role_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Roles_GetAll"));
        }

        public override IDataReader Role_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Roles_Search"), keyword, pageIndex, pageSize);
        }

        public override IDataReader Role_GetRoleOfUser(int userid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Roles_GetRoleOfUser"), userid);
        }

        public override void Role_ClearFunction(int roleId, int branchId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Roles_ClearFunction"), roleId, branchId);
        }

        public override void Role_SetFunction(int roleid, int branchId, int functionid, int createdby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Roles_SetFunction"), roleid, branchId, functionid, createdby);
        }

        public override void RoleHomemPages_Create(int roleId, int functionId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_RoleHomePages_Create"), roleId, functionId);
        }

        public override IDataReader RoleHomemPages_GetRoleHomePageConfigs()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_RoleHomePages_GetConfig"));
        }

        public override string RoleHomemPages_GetRoleHomePage(int roleId)
        {
            return (string)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_RoleHomePages_GetHomePageForRole"), roleId);
        }

        public override IDataReader Function_GetByUser(int userId, int branchId, int parentId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Roles_Function_GetByUser"), userId, branchId, parentId);
        }

        public override IDataReader User_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_Search"), keyword, pageIndex, pageSize);
        }

        public override IDataReader User_GetInfo(string username)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_GetInfoByUserName"), username);
        }

        public override int User_GetIP_Phone(string ipphone)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_Users_GetInfoByIpPhone"), ipphone);
        }

        public override IDataReader User_GetInfo(int userid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_GetInfo"), userid);
        }

        public override int User_Insert(string roleIds, string groupIds, string branchIds, string userName, string password, string fullName, string email, string mobile, string description, int status, int createdBy, string stationId, bool isSuperAdmin, bool isCollector, bool isCollaborator, int groupCollaboratorType, int normsCollaborator, bool isConsultant, int groupConsultantType, int normsConsultant)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_Users_Insert"), roleIds, groupIds, branchIds, userName, password, fullName, email, mobile, description, status, createdBy, stationId, isSuperAdmin, isCollector, isCollaborator, groupCollaboratorType, normsCollaborator, isConsultant, groupConsultantType, normsConsultant);
        }

        public override void User_Update(int userid, string roleIds, string groupIds, string branchIds, string userName, string password, string fullName, string email, string mobile, string description, int status, int changedBy, string stationId, bool isSuperAdmin, bool isCollector, bool isCollaborator, int groupCollaboratorType, int normsCollaborator, bool isConsultant, int groupConsultantType, int normsConsultant)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_Update"), userid, roleIds, groupIds, branchIds, userName, password, fullName, email, mobile, description, status, changedBy, stationId, isSuperAdmin, isCollector, isCollaborator, groupCollaboratorType, normsCollaborator, isConsultant, groupConsultantType, normsConsultant);
        }

        public override void User_Update_Norms(int userid, int changedBy, int normsCollaborator, int normsConsultant)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_Update_Norms"), userid, changedBy, normsCollaborator, normsConsultant);
        }

        public override void User_Delete(int userid, int deletedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_Delete"), userid, deletedby);
        }

        public override void User_UpdateStatus(int userid, int status, int changedby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_UpdateStatus"), userid, status, changedby);
        }

        public override void User_ChangePassword(int userid, string password)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_ChangePassword"), userid, password);
        }

        public override IDataReader Function_GetApprovedForUser(int userid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Function_GetApprovedByUserID"),
                userid);
        }

        public override IDataReader Function_GetApprovedForRole(int roleid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Function_GetApprovedByRoleID"), roleid);
        }
        public override IDataReader Function_GetChild(int parentid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Functions_GetChild"), parentid);
        }
        public override IDataReader Function_GetByRoleAndBranch(int roleId, int branchId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Function_GetByRoleAndBranch"), roleId, branchId);
        }
        public override IDataReader User_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_GetAll"));
        }

        //Ham viet de lay danh sach TVTS trong webservice WSCASEC2, lay thong tin va tra ve json cho thang he thong khac 
        public override IDataReader User_GetAll_TVTS()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_GetAll_TVTS")); 
        }

        public override IDataReader User_GetAllNormByDate(DateTime hanoverDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_GetAll_ByDate"),hanoverDate);
        }

        public override void User_SetRole(int userid, int roleid, int createdby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_SetRole"), userid, roleid,
                createdby);
        }

        public override void User_ClearRole(int userid)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_ClearRole"), userid);
        }

        public override int User_Validate(string username, string password)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, "Core_Users_Validate", username, password);
        }

        public override void User_SetBranch(int userid, int branch, int createdby)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_SetBranch"), userid, branch,
                createdby);
        }

        public override void User_ClearBranch(int userid)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_ClearBranch"), userid);
        }

        public override IDataReader User_GetBranchOfUser(int userid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_GetBranchOfUser"), userid);
        }

        public override IDataReader User_GetDraft(int branchId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("Core_Users_Select_Draft"), branchId);
        }

        public override void User_SetEmailSend(int id, string emailSend, string passwordSend, string signEmailSend)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_SetEmailSend"), id, emailSend, passwordSend, signEmailSend);
        }

        public override void User_SetPhone(int userId, string phone, string email)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_SetPhone"), userId, phone, email);
        }

        public override void User_LockUser(int id)
        {
            const int status = (int)StatusUserType.Locked;
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_LockUser"), id, status);
        }

        public override void User_OpenUser(int id)
        {
            const int status = (int)StatusUserType.Nomal;
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Core_Users_LockUser"), id, status);
        }

        public override int User_SetPass(string userName)
        {
            return (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("Core_Users_UpdatePass"), userName);
        }
    }
}
