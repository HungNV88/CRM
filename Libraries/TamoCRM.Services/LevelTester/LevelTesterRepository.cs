using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;

namespace TamoCRM.Services.LevelTester
{
    public class LevelTesterRepository
    {
        public static List<LevelTesterInfo> GetAll()
        {
            return ObjectHelper.FillCollection<LevelTesterInfo>(DataProvider.Instance().LevelTester_GetAll());
        }
    }
}
