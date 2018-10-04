namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC04Model
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Level { get; set; }
        public string DateTime { get; set; }
        public double HandoverCount { get; set; }
        public double CompleteInCount { get; set; }
        public double CompleteOutCount { get; set; }
        public double NotCompleteCount { get; set; }
    }
}