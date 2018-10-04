using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.LogDashBoard;
using TamoCRM.Domain.Logs;
using TamoCRM.Services.Logs;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Common;
using static TamoCRM.Services.Logs.TmpLogServiceRepository;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class LogLoadController : Controller
    {
        //
        // GET: /Admin/LogLoad/

        [HttpGet]
        public void SaveEndLoad(string fileName, string MethodName, int ContactId)
        {
            string filePath = HttpContext.Server.MapPath("~/Log/" + fileName);
            CheckPointLog.GetInstance().WriteLog(filePath, MethodName, ContactId);
        }

        [HttpGet]
        public void SaveEndLoadDashBoard(string time, int contactid, string name, int createdby, string timebegin, int session)
        {

            string _runtime = (DateTime.Now - DateTime.ParseExact(timebegin, "dd/MM/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture)).ToString();
            var logendform = new LogDashBoard
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"),
                Name = name,
                ContactId = contactid,
                CreatedBy = createdby,
                Runtime = "0",
                Session = session,
            };

            LogDashBoardRepository.CreateLogDashBoard(logendform);
        }
    }
}
