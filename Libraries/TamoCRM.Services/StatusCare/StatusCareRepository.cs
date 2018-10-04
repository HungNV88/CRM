using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;

namespace TamoCRM.Services.StatusCare
{
    public class StatusCareRepository
    {
        public static List<StatusCareInfo> GetAll()
        {
            return ObjectHelper.FillCollection<StatusCareInfo>(DataProvider.Instance().StatusCare_GetAll());
        }
    }
}
