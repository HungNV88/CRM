using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.UserRole;
using TamoCRM.Web.Mvc.Areas.Admin.Models.Users;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Roles
{
    public class RolePermisionModel
    {
        public IEnumerable<RoleInfo> AllRole { get; set; }
        public PostedRole PostedRole { get; set; }
        public IEnumerable<RoleInfo> SelectedRole { get; set; }

        public IEnumerable<BranchInfo> AllBranch { get; set; }
        public IEnumerable<BranchInfo> SelectedBranch { get; set; }
        public PostedBranch PostedBranch { get; set; }

        public IEnumerable<FunctionInfo> AllFunction { get; set; }
        public PostedFunction PostedFunction { get; set; }
        public IEnumerable<RoleInfo> SelectedFunction { get; set; }
    }    

}