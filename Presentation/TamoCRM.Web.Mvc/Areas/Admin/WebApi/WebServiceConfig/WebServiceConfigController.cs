using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.WebServiceConfig;
using TamoCRM.Services.WebServiceConfig;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Mvc.Areas.Admin.Models.WebServiceConfig;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.WebServiceConfig
{
    public class WebServiceConfigController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<WebServiceConfigInfo> Get()
        {
            return WebServiceConfigRepository.GetAll();
        }

        // GET api/<controller>
        public WebServiceConfigListModel Get(int page, int rows)
        {
            var model = new WebServiceConfigListModel
                            {
                                Page = page,
                                Records = rows,
                                Rows = WebServiceConfigRepository.GetAll(),
                            };
            model.Total = model.Rows.Count();
            model.UserData = model.Rows.Count();
            return model;
        }

        // GET api/<controller>/5
        public WebServiceConfigInfo Get(int id)
        {
            return WebServiceConfigRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("Id").Split(',')[0]);
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
            //StoreData.ListWebServiceConfig = WebServiceConfigRepository.GetAll();
            return retVal;
        }
    }
}

