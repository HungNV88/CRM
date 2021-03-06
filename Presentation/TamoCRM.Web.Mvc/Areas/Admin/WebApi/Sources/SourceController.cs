using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Sources;
using TamoCRM.Services.Sources;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Sources;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.Sources
{
    public class SourceController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<SourceInfo> Get()
        {
            return SourceRepository.GetAll();
        }

        // GET api/<controller>
        public SourceListModel Get(int page, int rows)
        {
            int totalRecords = 0;
            var model = new SourceListModel();
            model.Rows = SourceRepository.Search(string.Empty, page, rows, out totalRecords);
            model.Page = page;
            model.Total = ((int)totalRecords / rows) + 1;
            model.Records = rows;
            return model;
        }

        // GET api/<controller>/5
        public SourceInfo Get(int id)
        {
            return SourceRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            string retVal = string.Empty;
            string operation = form.Get("oper");
            int id = ConvertHelper.ToInt32(form.Get("SourceId"));
            SourceInfo info = null;
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "edit":
                        info = SourceRepository.GetInfo(id);
                        if (info != null)
                        {
													
							info.Name = form.Get("Name");
							
							info.Code = form.Get("Code");
							
							info.Description = form.Get("Description");
							
							
							info.ChangedBy = UserRepository.GetCurrentUserInfo().UserID;
							                            
                            SourceRepository.Update(info);
						
                        }
                        break;
                    case "add":
                        info = new SourceInfo
                               {
                                   Name = form.Get("Name"),
                                   Code = form.Get("Code"),
                                   Description = form.Get("Description"),
                                   CreatedBy = UserRepository.GetCurrentUserInfo().UserID
                               };


                        SourceRepository.Create(info);
                        break;
                    case "del":
                        SourceRepository.Delete(id, UserRepository.GetCurrentUserInfo().UserID);
                        break;
                    default:
                        break;
                }
            }
            return retVal;
        }
    }
}

