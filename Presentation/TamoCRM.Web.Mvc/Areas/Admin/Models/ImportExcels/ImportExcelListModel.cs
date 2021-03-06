using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.ImportExcels;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ImportExcels
{
    public class ImportExcelListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<ImportExcelInfo> Rows { get; set; }
    }
}

