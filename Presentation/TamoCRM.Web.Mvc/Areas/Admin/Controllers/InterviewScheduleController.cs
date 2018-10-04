using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class InterviewScheduleController : AdminController
    {
        //
        // GET: /Admin/InterviewSchedule/

        public ActionResult Index()
        {
            var url = StoreData.LinkInterviewSchedule;            
            url = string.Format(url);
            ViewBag.Url = url;            
            return View();
        }

    }
}
