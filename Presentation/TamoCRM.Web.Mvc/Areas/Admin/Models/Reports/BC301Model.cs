namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Reports
{
    public class BC301Model
    {
        public int Id { get; set; }
        public int CampaindTypeId { get; set; }
        public string CampaindName { get; set; }

        public string SumCampaindLevel { get; set; } //Tong so contact tu level 1 den level 8
        public string Level0 { get; set; }
        public string Level0Percent { get; set; }
        public string Level1 { get; set; }
        public string Level1Percent { get; set; }
        public string Level2 { get; set; }
        public string Level2Percent { get; set; }
        public string Level3 { get; set; }
        public string Level3Percent { get; set; }
        public string Level4 { get; set; }
        public string Level4Percent { get; set; }
        public string Level5 { get; set; }
        public string Level5Percent { get; set; }
        public string Level6 { get; set; }
        public string Level6Percent { get; set; }
        public string Level7 { get; set; }
        public string Level7Percent { get; set; }
        public string Level8 { get; set; }
        public string Level8Percent { get; set; }
    }
}