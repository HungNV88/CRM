using System;
using TamoCRM.Core.Data;
using TamoCRM.Domain.LogDashBoard;
using TamoCRM.Domain.Logs;

namespace TamoCRM.Services.Logs
{
    public class TmpLogServiceRepository
    {
        public static void Create(TmpLogServiceInfo info)
        {
            try
            {
                DataProvider.Instance().TmpLogService_Insert(info.Status, info.CallType, info.Time, info.Description);
            }
            catch
            {

            }
        }


        public class LogDashBoardRepository
        {
            public static void CreateLogDashBoard(LogDashBoard info)
            {
                info.Time = info.Time.Substring(11);
                try
                {
                    DataProvider.Instance().LogDashBoard_Insert(info.Date, info.Time, info.ContactId, info.Name, info.CreatedBy, info.Runtime, info.Session);
                }
                catch
                {

                }
            }
        }
    }        
}
