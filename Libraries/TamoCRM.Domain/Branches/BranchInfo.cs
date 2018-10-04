namespace TamoCRM.Domain.Branches
{
    public class BranchInfo : BaseClassInfo
    {
        public int BranchId { get;set; }
	
		public string Code { get;set; }
	
		public string Name { get;set; }
	
		public int LocationID { get;set; }
	
		public string Description { get;set; }
	
    }
}

