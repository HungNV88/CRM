namespace TamoCRM.Domain.WebServiceConfig
{
    public class WebServiceConfigInfo : BaseClassInfo
    {
        public int Id { get; set; }

        public int Type { get; set; }
	
		public string Value { get;set; }

        public int BranchId { get; set; }
    }
}

