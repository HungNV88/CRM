using System.Collections.Generic;
using TamoCRM.Domain.Table;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Tables
{
    public class TableModel
    {
        public IEnumerable<TableInfo> AllTables { get; set; }
        public IEnumerable<TableInfo> SelectedTable { get; set; }
        public PostedTable PostedTable { get; set; }
    }
    public class PostedTable
    {
        public string[] Name { get; set; }
    }
}
