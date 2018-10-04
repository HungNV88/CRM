using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TamoCRM.Core;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Framework.Controllers
{
    public class ApiControllerFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);
            //string filePath = actionContext.Request.RequestUri.LocalPath+"/"+actionContext.Request.Method;
            ////string controller_name = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string userName = UserContext.GetCurrentUser().UserName;

            //LogTracking.CreateCheckPoint("CRM_NATIVE_VN", filePath, "Begin Request", userName, DateTime.Now, "LogTime", (int)CheckPoint.time);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            //base.OnActionExecuted(actionContext);
            //string filePath = actionContext.Request.RequestUri.LocalPath + "/" + actionContext.Request.Method;
            //string userName = UserContext.GetCurrentUser().UserName;
            //LogTracking.CreateCheckPoint("CRM_NATIVE_VN", filePath, "End Request", userName, DateTime.Now, "LogTime", (int)CheckPoint.time);
        }
    }
}
