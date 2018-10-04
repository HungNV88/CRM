using System.Collections.Generic;
using System.Web;
using System.Web.Http.Filters;
using TamoCRM.Domain.Activity;
using TamoCRM.Domain.Contacts;
using TamoCRM.Services.Activity;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Framework.ActionFilters
{
    public class WebApiLogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                //Apply logging
                var objChanges = HttpContext.Current.Items[ContactInfo.CONTACT_CHANGE] as List<ActivityObjectChangeInfo>;
                if (objChanges != null)
                {
                    var activityInfo = new ActivityLogInfo
                                           {
                                               FunctionId = 0,
                                               CreatedBy = UserContext.GetCurrentUser().UserID
                                           };
                    activityInfo.Id = ActivityLogRepository.Create(activityInfo);
                    foreach (var objChange in objChanges)
                    {
                        objChange.ActivityId = activityInfo.Id;
                        ActivityObjectChangeRepository.Create(objChange);
                    }
                }
                HttpContext.Current.Items[ContactInfo.CONTACT_CHANGE] = null;
            }
            catch
            {
                //Dont throw exception if loging failed
            }
            base.OnActionExecuted(actionExecutedContext);
            
        }
    }
}
