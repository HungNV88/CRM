using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.UserRole;
using TamoCRM.Services.Branches;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Roles;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Roles
{
    public class RoleController : CustomApiController
    {
        public RoleListModel Get(int page, int rows)
        {
            int totalRecords = 0;
            var model = new RoleListModel();
            model.Rows = RoleRepository.Search(string.Empty, page, rows, out totalRecords);
            model.Page = page;
            model.Total = (totalRecords / rows) + 1;
            model.Records = rows;
            return model;
        }

        // POST api/<controller>
        [System.Web.Http.HttpPost]
        public string Edit(FormDataCollection form)
        {
            string retVal = string.Empty;
            string operation = form.Get("oper");
            int id = ConvertHelper.ToInt32(form.Get("RoleId"));
            RoleInfo info = null;
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "edit":
                        info = RoleRepository.GetInfo(id);
                        if (info != null)
                        {
                            info.Name = form.Get("Name");
                            info.Description = form.Get("Description");
                            info.ChangedBy = UserRepository.GetCurrentUserInfo().UserID;
                            RoleRepository.Update(info);

                        }
                        break;
                    case "add":
                        info = new RoleInfo
                               {
                                   Name = form.Get("Name"),
                                   Description = form.Get("Description"),
                                   CreatedBy = UserRepository.GetCurrentUserInfo().UserID
                               };
                        RoleRepository.Insert(info);
                        break;
                    case "del":
                        RoleRepository.Delete(id,UserRepository.GetCurrentUserInfo().UserID);
                        break;
                    default:
                        break;
                }
            }
            return retVal;
        }
    }
}
