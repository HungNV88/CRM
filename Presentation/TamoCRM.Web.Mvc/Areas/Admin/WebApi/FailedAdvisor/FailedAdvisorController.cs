using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TamoCRM.Core;
using TamoCRM.Domain.FailedAdvisor;
using TamoCRM.Services.FailedAdvisor;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.FailedAdvisor
{
    public class FailedAdvisorController : CustomApiController
    {
        [HttpGet]
        public List<FailedAdvisorInfo> GetAllNotifyFailedAdvisor()
        {
            const int status = 1; //Notify chua duoc duyet
            var userId = UserContext.GetCurrentUser().UserID;
            var list = FailedAdvisorRepository.GetAll(userId, status) ?? new List<FailedAdvisorInfo>();

            var result = new List<FailedAdvisorInfo>();
            foreach (var item in list.OrderByDescending(c => c.CreateDate))
            {
                var db = result.FirstOrDefault(c => c.Code == item.Code);
                if (db == null) result.Add(item);
                else db.Amount += 1;
            }
            return result;
        }
    }
}
