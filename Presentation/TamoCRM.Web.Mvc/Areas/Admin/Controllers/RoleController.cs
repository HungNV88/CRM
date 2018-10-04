using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Roles;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class RoleController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Permission(int? id)
        {
            var fun = FunctionRepository.GetChild(0);
            var lstfun = new List<FunctionInfo>();
            foreach (var info in fun)
            {
                lstfun.Add(info);
                Function_Bind(lstfun, info.FunctionID, "");
            }
            var model = new PermissionModel
                        {
                            Functions = lstfun,
                            Roles =new SelectList(RoleRepository.GetAll(),"RoleID","Name")
                        };

            if (id != null)
            {
                model.SelectedRoleID = id.Value;
                model.FunctionsSelected = FunctionRepository.GetApprovedForRole(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Permission(PermissionModel model,FormCollection collection)
        {
            //RoleRepository.ClearFunction(model.SelectedRoleID);
            //RoleRepository.SetFunction(model.SelectedRoleID,model.PostedFunction.FunctionID.ToList(),UserRepository.GetCurrentUserInfo().UserID);
            //return RedirectToAction("Permission", new { id =model.SelectedRoleID});
            return View();
        }

        private void Function_Bind(List<FunctionInfo> lstfun,int parentid,string level)
        {
            var list = FunctionRepository.GetChild(parentid);
            if (list != null && list.Count > 0)
            {
            level += "---";
                foreach (var info in list)
                {
                    info.Name = level + info.Name;
                    lstfun.Add(info);
                    Function_Bind(lstfun,info.FunctionID,level);
                }
            }
            //return list;
        }
    }
}