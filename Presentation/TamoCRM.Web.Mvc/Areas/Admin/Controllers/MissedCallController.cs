using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{

    public class MissedCallController : Controller
    {
        public ActionResult NotifyMissedCall()
        {
            return View();
        }
    }
}

