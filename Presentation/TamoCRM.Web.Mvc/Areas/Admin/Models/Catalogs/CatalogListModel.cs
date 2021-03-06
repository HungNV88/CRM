using System.Collections.Generic;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Catalogs
{
    public class CatalogListModel<T> where T : class 
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
        public IEnumerable<T> Rows { get; set; }
    }
}

