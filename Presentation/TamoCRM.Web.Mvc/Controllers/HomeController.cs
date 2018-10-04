using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var curUser = UserContext.GetCurrentUser();
            if (curUser != null)
            {
                var userRoles = RoleRepository.GetRoleOfUser(curUser.UserID);
                if (userRoles != null && userRoles.Count > 0)
                {
                    var home = RolePermisionRepository.GetRoleHomePage(userRoles[0].RoleID);
                    if (!string.IsNullOrEmpty(home))
                        return RedirectToLocal(home);
                }
            }
           return Redirect("/Admin/Dashboard");                
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
