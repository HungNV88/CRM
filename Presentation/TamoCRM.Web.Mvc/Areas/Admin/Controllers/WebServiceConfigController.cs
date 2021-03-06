using System.Collections.Generic;
using System.Web.Mvc;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.WebServiceConfig;
using TamoCRM.Services.WebServiceConfig;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.Controllers
{

    public class WebServiceConfigController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public IEnumerable<WebServiceConfigInfo> Get()
        {
            return WebServiceConfigRepository.GetAll();
        }

        [HttpPost]
        public string Edit(FormCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("Id"));
            WebServiceConfigInfo info;
            switch (operation)
            {
                case "edit":
                    info = WebServiceConfigRepository.GetInfo(id);
                    if (info != null)
                    {
                        info.Value = form.Get("Value");
                        info.Type = form.Get("Type").ToInt32();
                        info.BranchId = form.Get("BranchId").ToInt32();
                        WebServiceConfigRepository.Update(info);

                    }
                    break;
                case "add":
                    info = new WebServiceConfigInfo
                    {
                        Value = form.Get("Value"),
                        Type = form.Get("Type").ToInt32(),
                        BranchId = form.Get("BranchId").ToInt32(),
                    };
                    WebServiceConfigRepository.Create(info);
                    break;
                case "del":
                    WebServiceConfigRepository.Delete(id);
                    break;
            }
            return retVal;
        }
    }
}

