using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Levels;
using TamoCRM.Services.Levels;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Levels;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Levels
{
    public class LevelController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<LevelInfo> Get()
        {
            return LevelRepository.GetAll();
        }

        // GET api/<controller>
        public LevelListModel Get(int page, int rows)
        {
            int totalRecords;
            var model = new LevelListModel
                            {
                                Rows = LevelRepository.Search(string.Empty, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords/rows) + 1,
                                Records = rows
                            };
            return model;
        }

        // GET api/<controller>/5
        public LevelInfo Get(int id)
        {
            return LevelRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("LevelId"));
            if (!string.IsNullOrEmpty(operation))
            {
                LevelInfo info;
                switch (operation)
                {
                    case "edit":
                        info = LevelRepository.GetInfo(id);
                        if (info != null)
                        {
							
							info.Name = form.Get("Name");
							
							info.Description = form.Get("Description");
							
							info.Priority = ConvertHelper.ToInt32(form.Get("Priority"));
							
							info.ChangedBy = UserRepository.GetCurrentUserInfo().UserID;
							                            
                            LevelRepository.Update(info);
							
                        }
                        break;
                    case "add":
                        info = new LevelInfo
                                   {
                                       Name = form.Get("Name"),
                                       Description = form.Get("Description"),
                                       Priority = ConvertHelper.ToInt32(form.Get("Priority")),
                                       CreatedBy = UserRepository.GetCurrentUserInfo().UserID
                                   };

                        LevelRepository.Create(info);
                        break;
                    case "del":
                        LevelRepository.Delete(id,UserRepository.GetCurrentUserInfo().UserID);
                        break;
                }
            }
            //StoreData._listLevel = LevelRepository.GetAll();
            return retVal;
        }

        public List<LevelInfo> GetLevelWithContactCount(string userIds, int employeeTypeId)
        {
            var branchId = UserContext.GetDefaultBranch();
            var employeeType = (EmployeeType) employeeTypeId;
            var levels = LevelRepository.GetAllWithContactCount(userIds, branchId, employeeType) ?? new List<LevelInfo>();
            foreach (var level in StoreData.ListLevel.Where(c => !levels.Exists(p => p.LevelId == c.LevelId)))
                levels.Add(level);
            return levels.OrderBy(c => c.LevelId).ToList();
        }

        public List<LevelInfo> GetLevelWithCallHistoryCount(int groupUserId, int userIds, string hanoverDate,string callTime,int statusCare, int statusMap)
        {
            var branchId = UserContext.GetDefaultBranch();
            var levels = LevelRepository.GetAllWithHistoryCount(groupUserId, userIds, hanoverDate, callTime, statusCare, statusMap) ?? new List<LevelInfo>();
            foreach (var level in StoreData.ListLevel.Where(c => !levels.Exists(p => p.LevelId == c.LevelId)))
                levels.Add(level);
            return levels.OrderBy(c => c.LevelId).ToList();
        }
    }
}

