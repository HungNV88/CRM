namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC02Model
    {
        public string User { get; set; }
        public string Group { get; set; }
        public int SourceTypeId { get; set; }
        public string SourceType { get; set; }

        public double Level2 { get; set; }
        public double Level5B { get; set; }
        public double Level8 { get; set; }
    }
}