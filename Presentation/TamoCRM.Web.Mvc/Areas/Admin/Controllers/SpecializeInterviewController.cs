using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class SpecializeInterviewController : Controller
    {
        //
        // GET: /Admin/SpecializeInterview/

        public ActionResult Index()
        {
            var url = StoreData.LinkSpecializeInterview;
            url = string.Format(url);
            ViewBag.Url = url;     
            return View();
        }

    }
}
