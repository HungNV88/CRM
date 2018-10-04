using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Core.Data;
using TamoCRM.Domain.Catalogs;

namespace TamoCRM.Services.TimeSlot
{
    public class TimeSlotRepository
    {
        public static List<TimeSlotInfo> GetAll()
        {
            return ObjectHelper.FillCollection<TimeSlotInfo>(DataProvider.Instance().TimeSlot_GetAll());
        }
    }
}
