using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TamoCRM.Domain.UserRole; 

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Users
{
    public class UserListModel
    {
        public int Page { get; set; }
        public int Records { get; set; }
        public int Total { get; set; }
        public IEnumerable<UserInfo> Rows { get; set; }
    }
}

