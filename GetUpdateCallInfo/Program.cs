using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using TamoCRM;
using TamoCRM.Core;
using TamoCRM.Domain.CallHistories;
using TamoCRM.Services.CallHistories;
using TamoCRM.Web.Framework;
using TamoCRM.Services.Logs;
using TamoCRM.Domain.Logs;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Domain.WebServiceConfig;
using TamoCRM.Services.WebServiceConfig;
using TamoCRM.Call;
namespace GetUpdateCallInfo
{
    class Program
    {
        public static List<WebServiceConfigInfo> ListWebServiceConfig = new List<WebServiceConfigInfo>();
            
        static void Main(string[] args)
        {

            //Goi ham get CallHistoryInfo tat ca cac cuoc goi chua co link ghi am.
            int count = 1;         
         
            while (true)
            {
                count = 1;
                List<CallHistoryInfo> items = CallHistoryRepository.GetAllCallInfoNotLinkRecords();

                foreach (CallHistoryInfo item in items)
                {
                    try
                    {
                        StoreData.Job_WsUpdateCallHistoryInfo(item.CallHistoryId);
                        var itemInfo = CallHistoryRepository.GetInfo(item.CallHistoryId);
                        Console.WriteLine(count + "/" + items.Count + " TIME: " + itemInfo.CreatedDate + " MESSAGE: " + itemInfo.ErrorDesc + " CALLID: " + itemInfo.CallId + " LINKRECORD: " + itemInfo.LinkRecord);
                        Console.WriteLine();
                        count++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Log exception: " + ex.Message);
                        var itemInfo = CallHistoryRepository.GetInfo(item.CallHistoryId);
                        Console.WriteLine("EXCEPTION - MESSAGE: " + itemInfo.ErrorDesc + " CALLID: " + itemInfo.CallId + "TIME: " + itemInfo.CreatedDate);
                        Console.WriteLine();
                        count++;
                        TmpLogServiceInfo tmp = new TmpLogServiceInfo();
                        tmp.CallType = (int)CallType.ExceptionLogGetLinkRecord;
                        tmp.Status = 1;
                        tmp.Description = "";
                        tmp.Time = DateTime.Now;
                        TmpLogServiceRepository.Create(tmp);
                    }
                }
            }
            Console.ReadLine();
        }

       

    }
}
