using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.UserRole;

namespace TamoCRM.Services.UserRole
{
    public class RolePermisionRepository
    {
        public static void UpdateRoleHomePage(int roleId, int functionId)
        {
            DataProvider.Instance().RoleHomemPages_Create(roleId, functionId);
        }
        public static List<RoleHomePageConfigInfo> GetRoleHomePageConfigs()
        {
            return ObjectHelper.FillCollection<RoleHomePageConfigInfo>(DataProvider.Instance().RoleHomemPages_GetRoleHomePageConfigs());
        }
        public static string GetRoleHomePage(int roleId)
        {
            return DataProvider.Instance().RoleHomemPages_GetRoleHomePage(roleId);
        }
        public static void Clear(int roleId, int branchId)
        {
            DataProvider.Instance().Role_ClearFunction(roleId, branchId);
        }
        public static void Create(int roleId, int branchId, int functionId, int createdBy)
        {
            DataProvider.Instance().Role_SetFunction(roleId, branchId, functionId, createdBy);
        }
    }
}
