namespace TamoCRM.Domain.Phones
{
    public class PhoneInfo
    {
		public int ContactId { get;set; }
        public int IsPrimary { get; set; }
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
    }
}

