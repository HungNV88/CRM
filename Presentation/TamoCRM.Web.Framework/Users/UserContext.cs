using System.Linq;
using System.Threading;
using System.Web;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.UserRole;

namespace TamoCRM.Web.Framework.Users
{
    public class UserContext
    {
        private const string UserBranchCookie = "{0}_branch";
        public static int GetDefaultBranch()
        {
            var retVal = -1;
            var userInfo = GetCurrentUser();
            if (userInfo.UserID > 0)
            {
                retVal = ConvertHelper.ToInt32(CookieHelper.GetCookie(string.Format(UserBranchCookie, userInfo.UserName)));
                if (retVal == 0)
                {
                    var branches = UserRepository.GetBranchOfUser(userInfo.UserID);
                    if (branches != null && branches.Count > 0)
                    {
                        retVal = branches[0].BranchId;
                        CookieHelper.SetCookie(string.Format(UserBranchCookie, userInfo.UserName), retVal.ToString());
                    }
                }
            }

            return retVal;
        }
        public static UserInfo GetCurrentUser()
        {
            const string MEMBER_PREFIX = "UserInfo";
            UserInfo objUser = null;
            if (HttpContext.Current == null)
            {
                if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    return new UserInfo();

                var userName = Thread.CurrentPrincipal.Identity.Name;
                if (!StoreData.ListUser.IsNullOrEmpty())
                    objUser = StoreData.ListUser.FirstOrDefault(c => c.UserName == userName);
                if (objUser == null) objUser = UserRepository.GetInfo(userName);
                return objUser ?? new UserInfo();
            }
            objUser = (UserInfo)HttpContext.Current.Items[MEMBER_PREFIX];
            if (objUser == null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userName = HttpContext.Current.User.Identity.Name.Replace(MEMBER_PREFIX, string.Empty);
                if (!StoreData.ListUser.IsNullOrEmpty())
                    objUser = StoreData.ListUser.FirstOrDefault(c => c.UserName == userName);
                if (objUser == null) objUser = UserRepository.GetInfo(userName);
                HttpContext.Current.Items[MEMBER_PREFIX] = objUser;
            }
            return objUser ?? new UserInfo();
        }
    }
}
