using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class CallCenterController : AdminController
    {
        //
        // GET: /Admin/CallCenter/

        public ActionResult Index()
        {
            return View();
        }

    }
}
