using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class NotifyAdvisorHandoverFailedController : AdminController
    {
        //
        // GET: /Admin/NotifyAdvisorHandoverFailed/

        public ActionResult NotifyHandoverFailedAdvisor()
        {
            return View();
        }

    }
}
