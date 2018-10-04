using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamoCRM.Domain.UserRole;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Roles
{
    public class RoleListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<RoleInfo> Rows { get; set; }
    }

    public class PermissionModel
    {
        public SelectList Roles { get; set; }
        public IEnumerable<FunctionInfo> Functions { get; set; }
        public IEnumerable<FunctionInfo> FunctionsSelected { get; set; }
        ///public List<SelectListItem> selectedItens { get; set; }
        public int SelectedRoleID { get; set; } 
        public PostedFunction PostedFunction { get; set; }
    }

    public class PostedFunction
    {
        public int[] FunctionID { get; set; }
    }
}