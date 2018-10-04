using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Branches
{
    public class BranchModel
    {
        public int BranchId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int LocationID { get; set; }

        public string LocationName { get; set; }

        public string Description { get; set; }

    }
}