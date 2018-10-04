using System;

namespace TamoCRM.Domain.UserRole
{
    public class RoleInfo : BaseClassInfo
    {
        public int RoleID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class RoleHomePageConfigInfo
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public int FunctionId { get; set; }
    }
}
