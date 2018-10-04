namespace TamoCRM.Domain.StatusMap
{
    public class StatusMapInfo
    {
        public int Id { get; set; }
        public bool IsShow { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        public int LevelIdNext { get; set; }
        public int StatusIdNext { get; set; }
        public int StatusCareId { get; set; }
        public int StatusMapType { get; set; }
    }

}

