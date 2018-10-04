namespace TamoCRM.Web.Mvc.Areas.Admin.Models.ContactDeleted
{
    public class ContactDeletedModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int ContactId { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string LevelName { get; set; }
        public string BranchName { get; set; }
        public string DeletedName { get; set; }
        public string DeletedTime { get; set; }
        public string StatusMapName { get; set; }
    }
}