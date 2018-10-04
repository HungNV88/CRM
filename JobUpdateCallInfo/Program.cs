using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TamoCRM.Services.CallHistories;
using TamoCRM.Domain.CallHistories;
using System.Threading;
using TamoCRM.Call;
using TamoCRM.Core;

namespace JobUpdateCallInfo
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            var list = CallHistoryRepository.GetCallsEmptyLinkAll() ?? new List<CallHistoryInfo>();
         
            RepairCall(list.Where(c => !c.CallId.IsStringNullOrEmpty())
                                                     .Where(c => c.Duration.IsStringNullOrEmpty()));           

            list = list.Where(c => c.StatusMapId > 0).ToList();
            list = userLogType == (int)EmployeeType.Consultant
                       ? list.Where(c => c.UserLogType.IsIntegerNull() || c.UserLogType == userLogType).ToList()
                       : list.Where(c => c.UserLogType == userLogType).ToList();
        }

        private static int RepairCall(IEnumerable<CallHistoryInfo> list)
        {
            var count = 0;
            foreach (var entity in list)
            {
                // Repair call
                try
                {
                    if (entity.CallId != "KO_CO")
                    {
                        StoreData.WsUpdateCallHistoryInfo(entity);
                        count += 1;
                    }
                }
                catch
                {

                }
            }
            return count;
        }
    }
}
