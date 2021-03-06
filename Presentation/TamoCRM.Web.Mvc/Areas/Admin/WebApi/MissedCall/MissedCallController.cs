using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TamoCRM.Domain.MissedCall;
using TamoCRM.Services.MissedCall;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.MissedCall
{
    public class MissedCallController : CustomApiController
    {
        [HttpGet]
        public List<MissedCallInfo> GetAllMissedCall()
        {
            const int status = 1; 
            var userId = UserContext.GetCurrentUser().UserID;
            var list = MissedCallRepository.GetAll(userId, status) ?? new List<MissedCallInfo>();

            var result = new List<MissedCallInfo>();
            foreach (var item in list.OrderByDescending(c => c.CreatedTime))
            {
                var db = result.FirstOrDefault(c => c.PhoneNumber == item.PhoneNumber);
                if (db == null) result.Add(item);
                else db.Amount += 1;
            }
            return result;
        }
    }
}

