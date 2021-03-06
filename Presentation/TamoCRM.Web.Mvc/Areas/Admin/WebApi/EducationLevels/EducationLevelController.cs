using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.EducationLevels;
using TamoCRM.Services.EducationLevels;
using TamoCRM.Services.Email;
using TamoCRM.Services.UserRole;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Mvc.Areas.Admin.Models.EducationLevels;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.EducationLevels
{
    public class EducationLevelController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<EducationLevelInfo> Get()
        {
            return EducationLevelRepository.GetAll();
        }

        // GET api/<controller>
        public EducationLevelListModel Get(int page, int rows)
        {
            int totalRecords;
            var model = new EducationLevelListModel
                            {
                                Rows = EducationLevelRepository.Search(string.Empty, page, rows, out totalRecords),
                                Page = page,
                                Total = (totalRecords/rows) + 1,
                                Records = rows
                            };
            return model;
        }

        // GET api/<controller>/5
        public EducationLevelInfo Get(int id)
        {
            return EducationLevelRepository.GetInfo(id);
        }      
        // POST api/<controller>
        [HttpPost]
        public string Edit(FormDataCollection form)
        {
            var retVal = string.Empty;
            var operation = form.Get("oper");
            var id = ConvertHelper.ToInt32(form.Get("EducationLevelId"));
            var curUserInfo = UserRepository.GetCurrentUserInfo();
            if (!string.IsNullOrEmpty(operation))
            {
                EducationLevelInfo info = null;
                switch (operation)
                {
                    case "edit":
                        info = EducationLevelRepository.GetInfo(id);
                        if (info != null)
                        {
							info.Name = form.Get("Name");
							info.Description = form.Get("Description");
                            EducationLevelRepository.Update(info);
                        }
                        break;
                    case "add":
                        info = new EducationLevelInfo
                                   {
                                       Name = form.Get("Name"),
                                       Description = form.Get("Description"),
                                       CreatedBy = curUserInfo.UserID,
                                       CreatedDate = DateTime.Now
                                   };
                        EducationLevelRepository.Create(info);
                        break;
                    case "del":
                        EducationLevelRepository.Delete(id);
                        break;
                }
            }
            //StoreData.ListEducationLevel = EducationLevelRepository.GetAll();
            return retVal;
        }
    }
}

