using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ChannelController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

