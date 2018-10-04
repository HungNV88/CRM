using System.Web.Mvc;
using TamoCRM.Services.Channels;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{
    public class ContainerController : AdminController
    {
        //
        // GET: /Admin/Container/

        public ActionResult Index()
        {
            ViewBag.Channels = ChannelRepository.GetAll();
            return View();
        }

        public ActionResult ContainerMOL()
        {
            ViewBag.Channels = ChannelRepository.GetAll();
            return View();
        }



    }
}
