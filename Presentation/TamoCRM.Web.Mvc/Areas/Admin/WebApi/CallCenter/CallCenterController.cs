using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Extensions;
using TamoCRM.Domain.CallCenter;
using TamoCRM.Services.CallCenter;
using TamoCRM.Web.Framework;
using TamoCRM.Web.Framework.Users;
using TamoCRM.Web.Mvc.Areas.Admin.Models.CallCenter;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi.SourceTypes
{
    public class CallCenterController : CustomApiController
    {
        public IEnumerable<CallCenterInfo> Get()
        {
            return CallCenterRepository.GetAll();
        }

        // GET api/<controller>
        public CallCenterListModel Get(int page, int rows)
        {
            int totalRecords;
            var model = new CallCenterListModel
            {
                Rows = CallCenterRepository.Search(string.Empty, page, rows, out totalRecords),
                Page = page,
                Total = (totalRecords / rows) + 1,
                Records = rows
            };
            return model;
        }


        //// GET api/<controller>/5
        //public SourceTypeInfo Get(int id)
        //{
        //    return SourceTypeRepository.GetInfo(id);
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public string Edit(FormDataCollection form)
        //{
        //    var retVal = string.Empty;
        //    var operation = form.Get("oper");
        //    var id = form.Get("SourceTypeId").ToInt32();
        //    if (!string.IsNullOrEmpty(operation))
        //    {
        //        SourceTypeInfo info;
        //        switch (operation)
        //        {
        //            case "edit":
        //                info = SourceTypeRepository.GetInfo(id);
        //                if (info != null)
        //                {
        //                    info.Code = form.Get("Code");
        //                    info.Name = form.Get("Name");
        //                    info.ChangedDate = DateTime.Now;
        //                    info.Description = form.Get("Description");
        //                    info.ChangedBy = UserContext.GetCurrentUser().UserID;
        //                    info.IsCheckUpdate = form.Get("IsCheckUpdate").ToBoolean();
        //                    info.IsCheckDuplicate = form.Get("IsCheckDuplicate").ToBoolean();
        //                    info.IsShowHotLine = form.Get("IsShowHotLine").ToBoolean();
        //                    info.IsShowConsultant = form.Get("IsShowConsultant").ToBoolean();
        //                    SourceTypeRepository.Update(info);
        //                }
        //                break;
        //            case "add":
        //                info = new SourceTypeInfo
        //                       {
        //                           Code = form.Get("Code"),
        //                           Name = form.Get("Name"),
        //                           CreatedDate = DateTime.Now,
        //                           ChangedDate = DateTime.Now,
        //                           Description = form.Get("Description"),
        //                           CreatedBy = UserContext.GetCurrentUser().UserID,
        //                           IsShowHotLine = form.Get("IsShowHotLine").ToBoolean(),
        //                           IsCheckUpdate = form.Get("IsCheckUpdate").ToBoolean(),
        //                           IsCheckDuplicate = form.Get("IsCheckDuplicate").ToBoolean(),
        //                           IsShowConsultant = form.Get("IsShowConsultant").ToBoolean(),
        //                       };
        //                SourceTypeRepository.Create(info);
        //                break;
        //            case "del":
        //                SourceTypeRepository.Delete(id);
        //                break;
        //        }
        //    }
        //    //StoreData.ListSourceType = SourceTypeRepository.GetAll() ?? new List<SourceTypeInfo>();
        //    return retVal;
        //}
    }
}

