using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.UserRole;
using TamoCRM.Core.Commons.Utilities;

namespace TamoCRM.Services.UserRole
{
    public class UserRepository
    {
        public enum eUserStatus
        {
            Actived = 0,
            Disabled = 2,
            Deleted = 1
        }
        public static void ClearGroups(int userId)
        {
            DataProvider.Instance().User_ClearGroups(userId);
        }
        public static bool CheckPermision(int userId, int branchId, string area, string controller, string action, string param)
        {
            return DataProvider.Instance().Users_CheckPermisions(userId, branchId, area, controller, action, param) > 0;
        }
        public static IEnumerable<UserInfo> Search(string username, int pageIndex, int pageSize, out int totalrecord)
        {
            var reader = DataProvider.Instance().User_Search(username, pageIndex, pageSize);
            var lst = ObjectHelper.FillCollection<UserInfo>(reader,false);
            totalrecord = 0;
            try
            {
                reader.NextResult();
                //Get the total no of records from the second result
                if (reader.Read())
                {
                    totalrecord = Convert.ToInt32(reader[0]);
                }
            }
            catch
            {

            }
            finally
            {
                ObjectHelper.CloseDataReader(reader, true);
            }
            return lst;
        }


        public static UserInfo GetInfo(string username)
        {
            return ObjectHelper.FillObject<UserInfo>(DataProvider.Instance().User_GetInfo(username));
        }

        public static int GetIPPhone(string ipphone)
        {
            return DataProvider.Instance().User_GetIP_Phone(ipphone);
        }

        public static UserInfo GetInfo(int userid)
        {
            return ObjectHelper.FillObject<UserInfo>(DataProvider.Instance().User_GetInfo(userid));
        }

        public static void Insert(string roleIds, string groupIds, string branchIds, UserInfo info)
        {
            info.Password = SecurityHelper.GetMD5Hash(info.Password);
            info.UserID = DataProvider.Instance().User_Insert(roleIds, groupIds, branchIds, info.UserName, info.Password, info.FullName, info.Email, info.Mobile, info.Description, info.Status, info.CreatedBy, info.StationId, info.IsSuperAdmin, info.IsCollector, info.IsCollaborator, info.GroupCollaboratorType, info.NormsCollaborator, info.IsConsultant, info.GroupConsultantType, info.NormsConsultant);
        }

        public static void Update(string roleIds, string groupIds, string branchIds, UserInfo info)
        {
            DataProvider.Instance().User_Update(info.UserID, roleIds, groupIds, branchIds, info.UserName, info.Password, info.FullName, info.Email, info.Mobile, info.Description, info.Status, info.ChangedBy, info.StationId, info.IsSuperAdmin, info.IsCollector, info.IsCollaborator, info.GroupCollaboratorType, info.NormsCollaborator, info.IsConsultant, info.GroupConsultantType, info.NormsConsultant);
        }

        public static void UpdateNorms(UserInfo info)
        {
            DataProvider.Instance().User_Update_Norms(info.UserID, info.ChangedBy, info.NormsCollaborator, info.NormsConsultant);
        }

        public static void Delete(int userid, int deletedby)
        {
            DataProvider.Instance().User_Delete(userid, deletedby);
        }

        public static void UpdateStatus(int userid, eUserStatus status, int changedby)
        {
            DataProvider.Instance().User_UpdateStatus(userid, (int)status, changedby);
        }

        public static void ChangePassword(int userid, string password)
        {
            var pass = SecurityHelper.GetMD5Hash(password);
            DataProvider.Instance().User_ChangePassword(userid, pass);
        }

        public static UserInfo GetCurrentUserInfo()
        {
            const string MEMBER_PREFIX  = "UserInfo";
            UserInfo objUser;
            if (HttpContext.Current == null)
            {
                if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    return new UserInfo();
                objUser = GetInfo(Thread.CurrentPrincipal.Identity.Name);
                return objUser ?? new UserInfo();
            }
            objUser = (UserInfo)HttpContext.Current.Items[MEMBER_PREFIX];
            if (objUser == null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpContext.Current.Items[MEMBER_PREFIX] = objUser = GetInfo(HttpContext.Current.User.Identity.Name.Replace(MEMBER_PREFIX, string.Empty));
            }
            return objUser ?? new UserInfo();
        }


        public static List<UserInfo> GetAll()
        {
            return ObjectHelper.FillCollection<UserInfo>(DataProvider.Instance().User_GetAll());
        }

        public static List<UserInfoTVST> GetAll_TVTS()
        {
            return ObjectHelper.FillCollection<UserInfoTVST>(DataProvider.Instance().User_GetAll_TVTS());
        }

        public static List<UserInfo> GetAll(DateTime hanoverDate)
        {
            return ObjectHelper.FillCollection<UserInfo>(DataProvider.Instance().User_GetAllNormByDate(hanoverDate));
        }

        public static void SetRole(int userid,List<int> lstRole,int createdby)
        {
            foreach (var i in lstRole)
            {
                DataProvider.Instance().User_SetRole(userid,i,createdby);
            }
        }

        public static void ClearRole(int userid)
        {
            DataProvider.Instance().User_ClearRole(userid);
        }

        public static bool ValidateUser(string username, string password)
        {
            return DataProvider.Instance().User_Validate(username, password) > 0;
        }

        public static void ClearBranch(int userid)
        {
            DataProvider.Instance().User_ClearBranch(userid);
        }

        public static void SetBranch(int userid, List<int> lstBranchid, int createdby)
        {
            foreach (var i in lstBranchid)
            {
                DataProvider.Instance().User_SetBranch(userid, i, createdby);
            }
        }

        public static List<BranchInfo> GetBranchOfUser(int userid)
        {
            return ObjectHelper.FillCollection<BranchInfo>(DataProvider.Instance().User_GetBranchOfUser(userid));
        }

        public static List<UserDraftNormsInfo> GetDraft(int branchId)
        {
            return ObjectHelper.FillCollection<UserDraftNormsInfo>(DataProvider.Instance().User_GetDraft(branchId));
        }

        public static void SetEmailSend(int id, string emailSend, string passwordSend, string signEmailSend)
        {
            var pass = SecurityHelper.Encrypt(passwordSend);
            DataProvider.Instance().User_SetEmailSend(id, emailSend, pass, signEmailSend);
        }

        public static void LockUser(int id)
        {
            DataProvider.Instance().User_LockUser(id);
        }
        public static void OpenUser(int id)
        {
            DataProvider.Instance().User_OpenUser(id);
        }
        public static void SetPhone(int userId,string phone,string email)
        {
            DataProvider.Instance().User_SetPhone(userId, phone, email);
        }
        public static int SetPassWord(string userName)
        {
            return (int)DataProvider.Instance().User_SetPass(userName);
        }
    }
}
