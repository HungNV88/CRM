using System.Web;
using System.Web.Mvc;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Framework.ActionFilters
{
    public class AdminActionFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {            
            var retVal = base.AuthorizeCore(httpContext);
            var curUser = UserContext.GetCurrentUser();
            if (curUser.IsSuperAdmin) return true;

            var branchId = UserContext.GetDefaultBranch();
            if(retVal)
            {
                var rd = httpContext.Request.RequestContext.RouteData;

                var currentArea = rd.DataTokens["area"] as string;
                var currentAction = rd.GetRequiredString("action");
                var currentController = rd.GetRequiredString("controller");
                if (currentController.ToLower() != "accessdenied" && currentAction.ToLower() != "changepassword")
                {
                    if (!httpContext.Request.IsAjaxRequest())
                    {
                        var permision = UserRepository.CheckPermision(curUser.UserID, branchId, currentArea, currentController, currentAction, string.Empty);
                        if (!permision)
                        {
                            httpContext.Response.Redirect("/admin/accessdenied");
                        }
                    }
                }
            }
            return retVal;
        }        
    }    
}
