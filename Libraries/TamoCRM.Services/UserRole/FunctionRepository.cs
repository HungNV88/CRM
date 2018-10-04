using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.UserRole;

namespace TamoCRM.Services.UserRole
{
   public class FunctionRepository
    {
       public static List<FunctionInfo> GetApprovedForUser(int userid)
       {
           return ObjectHelper.FillCollection<FunctionInfo>(DataProvider.Instance().Function_GetApprovedForUser(userid));
       }

       public static List<FunctionInfo> GetApprovedForRole(int roleid)
       {
           return ObjectHelper.FillCollection<FunctionInfo>(DataProvider.Instance().Function_GetApprovedForRole(roleid));
       }

       public static List<FunctionInfo> GetChild(int parentid)
       {
           return ObjectHelper.FillCollection<FunctionInfo>(DataProvider.Instance().Function_GetChild(parentid));
       }
       public static List<FunctionInfo> GetByRoleAndBranch(int roleId, int branchId)
       {
           return ObjectHelper.FillCollection<FunctionInfo>(DataProvider.Instance().Function_GetByRoleAndBranch(roleId, branchId));
       }
       public static List<FunctionInfo> GetByUser(int userId, int branchId, int parentId)
       {
           return ObjectHelper.FillCollection<FunctionInfo>(DataProvider.Instance().Function_GetByUser(userId, branchId, parentId));
       }
    }
}
