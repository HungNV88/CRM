using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TamoCRM.Web.Mvc.Attribute
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute 
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
           

            //if (IsUserExcluded())
            //    return false;
            //else
            
            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.DataTokens["area"] as string;
            //var path = currentArea + "/";
            if (currentController.ToLower() == "accessdeny" && currentArea.ToLower()=="admin")
            {
                return base.AuthorizeCore(httpContext);
            }
            var username = httpContext.User.Identity.Name;
            if (username == "tamvt")
                httpContext.Response.Redirect("/AccessDeny");
            return base.AuthorizeCore(httpContext);
        }
    }
}