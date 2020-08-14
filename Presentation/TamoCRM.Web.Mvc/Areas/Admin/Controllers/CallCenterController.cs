using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;
using TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter;

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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CallCenterModel();
            return View(model);
        }

    }
}
