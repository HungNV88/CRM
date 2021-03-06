using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Collectors;
using TamoCRM.Services.Collectors;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Collectors;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi
{
    public class CollectorController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<CollectorInfo> Get()
        {
            return CollectorRepository.GetAll();
        }

        // GET api/<controller>
        public CollectorListModel Get(int page, int rows)
        {
            int totalRecords = 0;
            var model = new CollectorListModel();
            model.Rows = CollectorRepository.Search(string.Empty, page, rows, out totalRecords);
            model.Page = page;
            model.Total = ((int)totalRecords / rows) + 1;
            model.Records = rows;
            model.UserData = totalRecords;
            return model;
        }

        // GET api/<controller>/5
        public CollectorInfo Get(int id)
        {
            return CollectorRepository.GetInfo(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            string retVal = string.Empty;
            string operation = form.Get("oper");
            int id = ConvertHelper.ToInt32(form.Get("CollectorId"));
            CollectorInfo info = null;
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "edit":
                        info = CollectorRepository.GetInfo(id);
                        if (info != null)
                        {
						
							info.Code = form.Get("Code");
							
							info.Name = form.Get("Name");
							
							info.Description = form.Get("Description");
						
							info.ChangedBy = UserRepository.GetCurrentUserInfo().UserID;
							                            
                            CollectorRepository.Update(info);
							
                        }
                        break;
                    case "add":
                        info = new CollectorInfo();
						
						info.Code = form.Get("Code");
						
						info.Name = form.Get("Name");
						
						info.Description = form.Get("Description");
						
						info.CreatedBy = UserRepository.GetCurrentUserInfo().UserID;
						
                        CollectorRepository.Create(info);
                        break;
                    case "del":
                        CollectorRepository.Delete(id, UserRepository.GetCurrentUserInfo().UserID);
                        break;
                    default:
                        break;
                }
            }
            return retVal;
        }
    }
}

