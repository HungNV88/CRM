using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.Branches;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Roles;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Users;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class RolePermisionController : AdminController
    {
        public ActionResult ConfigRoleHomePage()
        {
            var rootFuncs = FunctionRepository.GetChild(0);
            var lstFunctions = new List<FunctionInfo>();
            foreach (FunctionInfo func in rootFuncs)
            {
                if (!func.IncludeMenu) continue;                
                func.SetLevel(0);
                lstFunctions.Add(func);
                LoadChildFunctions(lstFunctions, func);
            }
            int i = 0;
            while (i < lstFunctions.Count)
            {
                if (!lstFunctions[i].IncludeMenu) lstFunctions.RemoveAt(i);
                else i++;
            }
            lstFunctions.Insert(0, new FunctionInfo(){FunctionID = 0, Name = "--"});
            ViewBag.AllFunctions = lstFunctions;

            ViewBag.RoleHomePageConfigs = RolePermisionRepository.GetRoleHomePageConfigs();
            
            return View();
        }
        [HttpPost]
        public ActionResult ConfigRoleHomePage(FormCollection form)
        {
            var allRoles = RoleRepository.GetAll();
            foreach (RoleInfo role in allRoles)
            {
                int functionId = ConvertHelper.ToInt32(form.Get(role.RoleID + "_dropRoleFunction"));
                RolePermisionRepository.UpdateRoleHomePage(role.RoleID, functionId);
            }

            var rootFuncs = FunctionRepository.GetChild(0);
            var lstFunctions = new List<FunctionInfo>();
            foreach (FunctionInfo func in rootFuncs)
            {
                if (!func.IncludeMenu) continue;
                func.SetLevel(0);
                lstFunctions.Add(func);
                LoadChildFunctions(lstFunctions, func);
            }
            int i = 0;
            while (i < lstFunctions.Count)
            {
                if (!lstFunctions[i].IncludeMenu) lstFunctions.RemoveAt(i);
                else i++;
            }
            lstFunctions.Insert(0, new FunctionInfo() { FunctionID = 0, Name = "--" });
            ViewBag.AllFunctions = lstFunctions;
            ViewBag.RoleHomePageConfigs = RolePermisionRepository.GetRoleHomePageConfigs();
            
            return View();
        }
        //
        // GET: /Admin/RolePermision/
        [HttpPost]
        public ActionResult Index(RolePermisionModel model)
        {
            if (model.PostedFunction != null && model.PostedFunction.FunctionID != null)
            {
                model.AllBranch = BranchRepository.GetAll();
                model.AllRole = RoleRepository.GetAll();
                var rootFuncs = FunctionRepository.GetChild(0);
                var lstFunctions = new List<FunctionInfo>();
                foreach (FunctionInfo func in rootFuncs)
                {
                    lstFunctions.Add(func);
                    LoadChildFunctions(lstFunctions, func);
                }
                model.AllFunction = lstFunctions;
                var deleteFunctions = model.PostedFunction.FunctionID.Where(p => !model.PostedFunction.FunctionID.Any(p2 => p2 == p));
                int curUserId = UserRepository.GetCurrentUserInfo().UserID;
                foreach (int roleId in model.PostedRole.Id)
                {
                    foreach (int branchId in model.PostedBranch.Id)
                    {
                        RolePermisionRepository.Clear(roleId, branchId);
                        foreach (int functionId in model.PostedFunction.FunctionID)
                        {
                            RolePermisionRepository.Create(roleId, branchId, functionId, curUserId);
                        }
                    }
                }
            }
            return View(model);
        }
        public ActionResult Index()
        {
            //ViewBag.Roles = RoleRepository.GetAll();
            var model = new RolePermisionModel()
            {
                AllBranch = BranchRepository.GetAll(),
                AllRole = RoleRepository.GetAll(),
                
                SelectedBranch = new Collection<BranchInfo>(),
                SelectedRole = new Collection<RoleInfo>(),
                
                PostedBranch = new PostedBranch(),
                PostedRole = new PostedRole(),
                PostedFunction = new PostedFunction()
            };
            var rootFuncs = FunctionRepository.GetChild(0);
            var lstFunctions = new List<FunctionInfo>();
            foreach (FunctionInfo func in rootFuncs)
            {
                func.SetLevel(0);
                lstFunctions.Add(func);
                LoadChildFunctions(lstFunctions, func);
            }
            model.AllFunction = lstFunctions;
            return View(model);
        }
        private void LoadChildFunctions(List<FunctionInfo> lstFunctions, FunctionInfo curFunc)
        {
            int level = curFunc.GetLevel() + 1;
            var lstChilds = FunctionRepository.GetChild(curFunc.FunctionID);
            foreach (FunctionInfo func in lstChilds)
            {
                func.SetLevel(level);
                func.Name = MiscUtility.StringIndent(level) + func.Name;
                lstFunctions.Add(func);
                LoadChildFunctions(lstFunctions, func);
            }
        }
    }
}
