using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.FailedAdvisor;

namespace TamoCRM.Services.FailedAdvisor
{
    public class FailedAdvisorRepository
    {
        public static List<FailedAdvisorInfo> GetAll(int userId, int status)
        {
            return ObjectHelper.FillCollection<FailedAdvisorInfo>(DataProvider.Instance().FailedAdvisor_GetAll(userId, status));
        }
    }
}
