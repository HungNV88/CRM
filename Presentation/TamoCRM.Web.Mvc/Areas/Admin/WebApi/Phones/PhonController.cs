using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TamoCRM.Core.Commons.Utilities;
using TamoCRM.Domain.Phones;
using TamoCRM.Services.Phones;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Phones;
using TamoCRM.Web.Framework.Controllers;

namespace TamoCRM.Web.Mvc.Areas.Admin.WebApi
{
    public class PhonController : CustomApiController
    {
        // GET api/<controller>
        public IEnumerable<PhoneInfo> Get()
        {
            return PhoneRepository.GetAll();
        }

        // GET api/<controller>
        public PhonListModel Get(int page, int rows)
        {
            int totalRecords = 0;
            var model = new PhonListModel();
            model.Rows = PhoneRepository.Search(string.Empty, page, rows, out totalRecords);
            model.Page = page;
            model.Total = ((int)totalRecords / rows) + 1;
            model.Records = rows;
            return model;
        }

    }
}

