using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Domain.Levels
{
    public class LevelInfo:BaseClassInfo
    {
    
		public int LevelId { get;set; }
	
		public string Name { get;set; }
	
		public string Description { get;set; }
	
		public int Priority { get;set; }

        public int ContactCount { get; set; }

    }
}