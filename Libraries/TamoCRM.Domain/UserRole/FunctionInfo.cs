namespace TamoCRM.Domain.UserRole
{
    public class FunctionInfo : BaseClassInfo
    {

        public int FunctionID { get; set; }

        public int ParentID { get; set; }

        public string Icon { get; set; }

        public bool IncludeMenu { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Params { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private int level;
        public void SetLevel(int l)
        {
            level = l;
        }
        public int GetLevel()
        {
            return level;
        }
        
    }
    
}