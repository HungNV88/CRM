using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void User_ClearGroups(int userId);
        public abstract int Users_CheckPermisions(int userId, int branchId, string area, string controller, string action, string param);
        public abstract int Role_Insert(string name, string description, int createdby);
        public abstract void Role_Update(int id, string name, string description, int changedby);
        public abstract void Role_Delete(int id,int deletedby);
        public abstract IDataReader Role_GetInfo(int id);
        public abstract IDataReader Role_GetAll();
        public abstract IDataReader Role_Search(string keyword, int pageIndex, int pageSize);
        public abstract IDataReader Role_GetRoleOfUser(int userid);
        public abstract void Role_ClearFunction(int roleId, int branchId);
        public abstract void Role_SetFunction(int roleId, int branchId, int functionid, int createdby);

        public abstract void RoleHomemPages_Create(int roleId, int functionId);

        public abstract IDataReader RoleHomemPages_GetRoleHomePageConfigs();

        public abstract string RoleHomemPages_GetRoleHomePage(int roleId);

        public abstract IDataReader User_Search(string username, int pageIndex, int pageSize);
        
        public abstract IDataReader User_GetInfo(string username);
        public abstract int User_GetIP_Phone(string ipphone);
        public abstract IDataReader User_GetInfo(int userid);
        public abstract int User_Insert(string roleIds, string groupIds, string branchIds, string userName, string password, string fullName, string email, string mobile, string description, int status, int createdBy, string stationId, bool isSuperAdmin, bool isCollector, bool isCollaborator, int groupCollaboratorType, int normsCollaborator, bool isConsultant, int groupConsultantType, int normsConsultant);
        public abstract void User_Update(int userid, string roleIds, string groupIds, string branchIds, string userName, string password, string fullName, string email, string mobile, string description, int status, int changedBy, string stationId, bool isSuperAdmin, bool isCollector, bool isCollaborator, int groupCollaboratorType, int normsCollaborator, bool isConsultant, int groupConsultantType, int normsConsultant);
        public abstract void User_Update_Norms(int userid, int changedBy, int normsCollaborator, int normsConsultant);
        public abstract void User_Delete(int userid, int deletedby);
        public abstract void User_UpdateStatus(int userid,int status, int changedby);

        public abstract void User_ChangePassword(int userid, string password);

        public abstract IDataReader User_GetAll();

        public abstract IDataReader User_GetAll_TVTS();

        public abstract IDataReader User_GetAllNormByDate(DateTime hanoverDate);

        public abstract void User_SetRole(int userid, int roleid,int createdby);

        public abstract void User_ClearRole(int userid);

        public abstract IDataReader Function_GetApprovedForUser(int userid);
        public abstract IDataReader Function_GetApprovedForRole(int roleid);
        public abstract IDataReader Function_GetChild(int parentid);
        public abstract IDataReader Function_GetByRoleAndBranch(int roleId, int branchId);
        public abstract IDataReader Function_GetByUser(int userId, int branchId, int parentId);
        public abstract int User_Validate(string username, string password);

        public abstract void User_ClearBranch(int userid);
        public abstract void User_SetBranch(int userid, int branchid, int createdby);
        public abstract IDataReader User_GetBranchOfUser(int userid);
        public abstract IDataReader User_GetDraft(int branchId);
        public abstract void User_SetEmailSend(int id, string emailSend, string passwordSend, string signEmailSend);
        public abstract void User_LockUser(int id);
        public abstract void User_OpenUser(int id);
        public abstract void User_SetPhone(int userId, string phone, string email);
        public abstract int User_SetPass(string userName);
    }
}
