using System;

namespace TamoCRM.Domain.EducationLevels
{
    public class EducationLevelInfo
    {
        public int EducationLevelId { get;set; }
	
		public string Name { get;set; }
	
		public string Description { get;set; }
	
		public int CreatedBy { get;set; }
	
		public DateTime CreatedDate { get;set; }
	
    }
}

