using System.Web.Mvc;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class GroupController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexCollaborator()
        {
            return View();
        }
    }
}

