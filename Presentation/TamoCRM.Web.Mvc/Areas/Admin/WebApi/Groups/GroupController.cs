using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Groups;
using TamoCRM.Services.Groups;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Groups;
using TamoCRM.Core.Commons.Extensions;
using RestSharp;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Groups
{
    public class GroupController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<GroupInfo> Get()
        {
            return GroupRepository.GetAll();
        }

        // GET api/<controller>
        public GroupListModel Get(int employeeId, int page, int rows)
        {
            var employeeType = (EmployeeType)employeeId;
            int totalRecords;
            var model = new GroupListModel
                            {
                                Rows = GroupRepository.Search(employeeType, string.Empty, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords / rows) + 1,
                                Records = rows,
                                UserData = totalRecords
                            };
            if (model.Rows != null)
            {
                var branchId = UserContext.GetDefaultBranch();
                model.Rows = model.Rows.Where(c => c.BranchId == branchId).ToList();
            }
            return model;
        }

        // GET api/<controller>/5
        public GroupInfo Get(int id)
        {
            return GroupRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string EditConsultant(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("GroupId"));
            if (!string.IsNullOrEmpty(operation))
            {
                GroupInfo info;
                switch (operation)
                {
                    case "edit":
                        info = GroupRepository.GetInfo(id);
                        if (info != null)
                        {
                            info.Name = form.Get("Name");
                            info.Description = form.Get("Description");
                            info.LeaderId = form.Get("LeaderId").ToInt32();
                            info.BranchId = UserContext.GetDefaultBranch();
                            info.EmployeeTypeId = (int)EmployeeType.Consultant;
                            GroupRepository.Update(info);
                        }
                        break;
                    case "add":
                        info = new GroupInfo
                                   {
                                       Name = form.Get("Name"),
                                       CreatedDate = DateTime.Now,
                                       Description = form.Get("Description"),
                                       LeaderId = form.Get("LeaderId").ToInt32(),
                                       BranchId = UserContext.GetDefaultBranch(),
                                       EmployeeTypeId = (int)EmployeeType.Consultant,
                                       CreatedBy = UserContext.GetCurrentUser().UserID,
                                   };
                        GroupRepository.Create(info);
                        break;
                    case "del":
                        GroupRepository.Delete(id);
                        break;
                }
                StoreData.ReloadData<GroupInfo>();
            }
            return retVal;
        }

        [HttpPost]
        public string EditCollaborator(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("GroupId"));
            if (!string.IsNullOrEmpty(operation))
            {
                GroupInfo info;
                switch (operation)
                {
                    case "edit":
                        info = GroupRepository.GetInfo(id);
                        if (info != null)
                        {
                            info.Name = form.Get("Name");
                            info.Description = form.Get("Description");
                            info.LeaderId = form.Get("LeaderId").ToInt32();
                            info.BranchId = UserContext.GetDefaultBranch();
                            info.EmployeeTypeId = (int)EmployeeType.Collaborator;
                            GroupRepository.Update(info);
                        }
                        break;
                    case "add":
                        info = new GroupInfo
                        {
                            Name = form.Get("Name"),
                            CreatedDate = DateTime.Now,
                            Description = form.Get("Description"),
                            LeaderId = form.Get("LeaderId").ToInt32(),
                            BranchId = UserContext.GetDefaultBranch(),
                            EmployeeTypeId = (int)EmployeeType.Collaborator,
                            CreatedBy = UserContext.GetCurrentUser().UserID,
                        };
                        GroupRepository.Create(info);
                        break;
                    case "del":
                        GroupRepository.Delete(id);
                        break;
                }
                StoreData.ReloadData<GroupInfo>();
            }
            return retVal;
        }        

        public IEnumerable<GroupInfo> GetGroupByBranchId(int branchId, int employeeTypeId)
        {
            var groups = StoreData.ListGroup ?? GroupRepository.GetAll();
            return branchId == 0
                ? groups.Where(c => c.EmployeeTypeId == employeeTypeId).ToList()
                : groups.Where(c => c.BranchId == branchId).Where(c => c.EmployeeTypeId == employeeTypeId).ToList();
        }

        public IEnumerable<GroupInfo> GetGroup(int employeeTypeId)
        {
            var branchId = UserContext.GetDefaultBranch();
            return GetGroupByBranchId(branchId, employeeTypeId);
        }
    }
}

