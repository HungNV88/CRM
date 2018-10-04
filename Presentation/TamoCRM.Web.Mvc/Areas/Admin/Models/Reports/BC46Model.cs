namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC46Model
    {
        public static string _fromdate;
        public static string _todate;
        public string Channel { get; set; }
        public int SumL { get; set; }
        public decimal Proportion { get; set; }
        public int L8 { get; set; }
        public decimal L8Sum { get; set; }
        public int L38 { get; set; }
        public decimal L38Sum { get; set; }
        public double C3B { get; set; }
        public decimal L8Price { get; set; }
        public string FromDate { get { return _fromdate; } }
        public string ToDate { get { return _todate; } }

    }
}