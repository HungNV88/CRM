namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC301Model
    {
        public int Id { get; set; }
        public int CampaindTypeId { get; set; }
        public string CampaindName { get; set; }

        public string SumCampaindLevel { get; set; } //Tong so contact tu level 1 den level 8
        public string Level0 { get; set; }
        public string L0Percent { get; set; }
        public string Level1 { get; set; }
        public string L1Percent { get; set; }
        public string Level2 { get; set; }
        public string L2Percent { get; set; }
        public string Level3 { get; set; }
        public string L3Percent { get; set; }
        public string Level4 { get; set; }
        public string L4Percent { get; set; }
        public string Level5 { get; set; }
        public string L5Percent { get; set; }
        public string Level6 { get; set; }
        public string L6Percent { get; set; }
        public string Level7 { get; set; }
        public string L7Percent { get; set; }
        public string Level8 { get; set; }
        public string L8L6Percent { get; set; }
        public string L8Percent { get; set; }
    }
}