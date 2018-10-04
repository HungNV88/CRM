using System.Text;
using System.Web.Mvc;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Admin/Menu/
        [ChildActionOnly]
        public string Render()
        {
            var userInfo = UserContext.GetCurrentUser();
            int branchId = UserContext.GetDefaultBranch();
            int curUserId = userInfo.UserID;
            var lstFunctions = userInfo.IsSuperAdmin ? FunctionRepository.GetChild(0) : FunctionRepository.GetByUser(curUserId, branchId, 0);
            string retVal = "<ul class='nav nav-list'>";
            foreach (var info in lstFunctions)
            {
                info.SetLevel(0);
                retVal += GetMenuItem(info);
            }
            retVal += "</ul>";
            return retVal;
        }
        public string GetMenuItem(FunctionInfo func)
        {
            var currentUrl = System.Web.HttpContext.Current.Request.Url.AbsolutePath; 
            if (func.GetLevel() == 4 || !func.IncludeMenu)
                return string.Empty;
            
            var userInfo = UserContext.GetCurrentUser();
            var branchId = UserContext.GetDefaultBranch();
            var curUserId = userInfo.UserID;
            var retVal = new StringBuilder();
            var lstChilds = userInfo.IsSuperAdmin
                                ? FunctionRepository.GetChild(func.FunctionID)
                                : FunctionRepository.GetByUser(curUserId, branchId, func.FunctionID);
            var menuAClass = string.Empty;
            string iconClass;//
            if (lstChilds != null && lstChilds.Count > 0)
            {
                menuAClass = "dropdown-toggle";
                iconClass = string.Format("<i class='{0}'></i>", string.IsNullOrEmpty(func.Icon) ? "icon-double-angle-right" : func.Icon);
            }
            else
            {
                iconClass = string.IsNullOrEmpty(func.Icon) ? string.Empty : string.Format("<i class='{0}'></i>", func.Icon);
            }

            retVal.AppendFormat(currentUrl == func.Path
                                    ? "<li class='{0}'><a href='{1}' class='{2}'>{3}<span class='menu-text menu-active'>{4}</span>"
                                    : "<li class='{0}'><a href='{1}' class='{2}'>{3}<span class='menu-text'>{4}</span>",
                                "open", func.Path, menuAClass, iconClass, func.Name);
            if (lstChilds != null && lstChilds.Count > 0)
            {
                retVal.Append("<b class='arrow icon-angle-down'></b>");
            }
            retVal.Append("</a>");
            if (lstChilds != null && lstChilds.Count > 0)
            {
                retVal.Append("<ul class='submenu' id='" + func.FunctionID + "' style='display:none;'>");
                foreach (var child in lstChilds)
                {
                    child.SetLevel(func.GetLevel() + 1);
                    var menuItem = GetMenuItem(child);
                    retVal.Append(menuItem);
                    if (menuItem.Contains("menu-active"))
                        retVal = retVal.Replace("id='" + func.FunctionID + "' style='display:none;'>", "id='" + func.FunctionID + "' style='display:block;'>");
                }
                retVal.Append("</ul>");
            }

            retVal.Append("</li>");
            return retVal.ToString();
        }
        //
        // GET: /Admin/Menu/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Menu/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Menu/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Menu/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Menu/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Menu/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Menu/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
