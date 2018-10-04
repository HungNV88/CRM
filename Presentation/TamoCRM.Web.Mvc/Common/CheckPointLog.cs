using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using TamoCRM.Core;
using TamoCRM.Web.Framework.Users;

namespace TamoCRM.Web.Mvc.Common
{
    public class CheckPointInfo
    {
        public DateTime Time { get; set; }
        public int ContactId { get; set; }
        public string ParentCheckName { get; set; }
        public int UserId { get; set; }
        public int RunTime { get; set; }
        public int Session { get; set; }
    }
    public class CheckPointLog
    {
        private DataTable dtLogCheckPoint;
        private DataTable CreateLogCheckPoint()
        {
            var retVal = new DataTable("LogCheckPoint");

            retVal.Columns.Add("Time");
            retVal.Columns.Add("ContactId");
            retVal.Columns.Add("ParentCheckName");
            retVal.Columns.Add("UserId");
            retVal.Columns.Add("RunTime");
            retVal.Columns.Add("Session");

            return retVal;
        }

        private static object _lock = new object();
        private static CheckPointLog _logger = null;
        private static List<CheckPointInfo> _list;
        //private SqlConnection connection;

        public CheckPointLog()
        {
            _list = new List<CheckPointInfo>();
            dtLogCheckPoint = CreateLogCheckPoint();
        }

        public static CheckPointLog GetInstance()
        {
            if (_logger == null)
            {
                lock (_lock)
                {
                    if (_logger == null)
                    {
                        _logger = new CheckPointLog();
                    }
                }
            }

            return _logger;
        }
        public void WriteLog(string filePath, string NameMethod, int ContactId)
        {
            CheckPointInfo info = new CheckPointInfo();
            info.Time = DateTime.Now;
            info.ContactId = ContactId;
            info.ParentCheckName = NameMethod;
            info.UserId = UserContext.GetCurrentUser().UserID;
            //info.RunTime = RunTime;
            WriteFileLogCheckPoint(info, filePath);
        }
        public void WriteFileLogCheckPoint(CheckPointInfo dtData, string filePath)
        {
            try
            {
                StreamWriter swExtLogFile = new StreamWriter(filePath, true);
                //swExtLogFile.Write(Environment.NewLine);
                swExtLogFile.WriteLine(dtData.Time.ToString("dd/MM/yyyy HH:mm:ss:fff") + "  " + dtData.ContactId.ToString() + " " + dtData.ParentCheckName.ToString() + " " + dtData.UserId);
                swExtLogFile.Flush();
                swExtLogFile.Close();
            }
            catch (Exception ex)
            {

            }

        }
    }
}

