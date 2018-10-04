namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC300Model
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string School { get; set; }
        public string DateTime { get; set; }
        public int SourceTypeId { get; set; }
        public string SourceType { get; set; }

        public string Level { get; set; } //Tong so contact tu level 1 den level 8
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string Level6 { get; set; }
        public string Level7 { get; set; }
        public string Level8 { get; set; }
        public string Level9 { get; set; }
        // phulv
        public string NameArise { get; set; }

        public string Arise { get; set; }  //Tong so contact phat sinh tu L1 den L8
        public string Arise_L1 { get; set; }
        public string Arise_L2 { get; set; }
        public string Arise_L3 { get; set; }
        public string Arise_L4 { get; set; }
        public string Arise_L5 { get; set; }
        public string Arise_L6 { get; set; }
        public string Arise_L7 { get; set; }
        public string Arise_L8 { get; set; }
        public string Arise_L9 { get; set; }
    }
}