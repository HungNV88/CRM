using System;

namespace TamoCRM.Domain.Groups
{
    public class GroupInfo
    {
		public int GroupId { get;set; }
		public string Name { get;set; }
        public int BranchId { get; set; }
        public int LeaderId { get; set; }
		public int CreatedBy { get;set; }
        public int EmployeeTypeId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class GroupTypeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeTypeId { get; set; }
    }
}

