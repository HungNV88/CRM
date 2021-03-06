using System.Collections.Generic;
using System.Web.Mvc;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Branches;
using TamoCRM.Services.Branches;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Branches;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{

    public class BranchController : Controller
    {
        //
        // GET: /Admin/Branch/
        
        public ActionResult BranchSelectBox()
        {
            var curUser = UserContext.GetCurrentUser();
            ViewBag.UserBranches = UserRepository.GetBranchOfUser(curUser.UserID);
            return View();
        }
        [HttpPost]
        public ActionResult BranchSelectBox(FormCollection form)
        {
            var curUser = UserContext.GetCurrentUser();
            ViewBag.UserBranches = UserRepository.GetBranchOfUser(curUser.UserID);
            CookieHelper.SetCookie(string.Format("{0}_branch", curUser.UserName), form.Get("branchId"));
            return View();
        }
        public JsonResult Get(int page, int rows)
        {
            int totalRecords;
            var model = new BranchListModel();
            var data = BranchRepository.Search(string.Empty, page, rows, out totalRecords);
            var lstModel = new List<BranchModel>();
            foreach (var info in data)
            {
                var objModel = ObjectHelper.Transform<BranchModel, BranchInfo>(info);
                lstModel.Add(objModel);
            }
            model.Rows = lstModel;
            model.Page = page;
            model.Total = (totalRecords / rows) + 1;
            model.Records = rows;
            model.UserData = totalRecords;
            return new JsonResult {Data  = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
        public ActionResult Index()
        {
            return View();
        }
        
        //
        // POST: /Admin/Branch/Edit/5

        [HttpPost]
        public string Edit(FormCollection form)
        {
            string retVal = string.Empty;
            string operation = form.Get("oper");
            int id = ConvertHelper.ToInt32(form.Get("BranchId"));
            if (!string.IsNullOrEmpty(operation))
            {
                BranchInfo info;
                switch (operation)
                {
                    case "edit":
                        info = BranchRepository.GetInfo(id);
                        if (info != null)
                        {

                            info.Code = form.Get("Code");
                            info.Name = form.Get("Name");
                            info.LocationID = ConvertHelper.ToInt32(form.Get("LocationName"));
                            info.Description = form.Get("Description");
                            info.ChangedBy = UserRepository.GetCurrentUserInfo().UserID;
                            BranchRepository.Update(info);

                        }
                        break;
                    case "add":
                        info = new BranchInfo
                        {
                            Code = form.Get("Code"),
                            Name = form.Get("Name"),
                            LocationID = ConvertHelper.ToInt32(form.Get("LocationName")),
                            Description = form.Get("Description"),
                            CreatedBy = UserRepository.GetCurrentUserInfo().UserID
                        };

                        BranchRepository.Create(info);
                        break;
                    case "del":
                        BranchRepository.Delete(id, UserRepository.GetCurrentUserInfo().UserID);
                        break;
                }
            }
            return retVal;
        }
    }
}

