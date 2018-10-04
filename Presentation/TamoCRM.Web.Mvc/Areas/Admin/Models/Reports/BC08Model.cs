namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC09Model
    {
        public BC09Model()
        {
            L1 = 0;
            L1A = 0;
            L1B = 0;
        }
        public int L1 { get; set; }
        public int L1A { get; set; }
        public int L1B { get; set; }
        public string DateTime { get; set; }
        public string SourceType { get; set; }
        public string ImportName { get; set; }
    }
}