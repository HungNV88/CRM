using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Branches;
using TamoCRM.Services.Branches;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Branches;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Branches
{
    public class BranchController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<BranchInfo> Get()
        {
            return BranchRepository.GetAll();
        }

        // GET api/<controller>
        public BranchListModel Get(int page, int rows)
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
            return model;
        }

        // GET api/<controller>/5
        public BranchInfo Get(int id)
        {
            return BranchRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("BranchId"));
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
							//info.Status = form.Get("Status");
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
                                   //Status = form.Get("Status"),
                                   CreatedBy = UserRepository.GetCurrentUserInfo().UserID
                               };

                        BranchRepository.Create(info);
                        break;
                    case "del":
                        BranchRepository.Delete(id,UserRepository.GetCurrentUserInfo().UserID);
                        break;
                }
            }
            //StoreData.ListBranch = BranchRepository.GetAll();
            return retVal;
        }
    }
}

