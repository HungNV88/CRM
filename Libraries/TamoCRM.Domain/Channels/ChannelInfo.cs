namespace TamoCRM.Domain.Channels
{
    public class ChannelInfo : BaseClassInfo
    {
        public int SourceTypeId { get; set; }

        public int ChannelId { get; set; }

        public int BranchId { get; set; }

		public string Name { get; set; }
	
		public string Code { get;set; }
	
		public string Description { get; set; }

        public int Amount { get; set; }
    }
}

