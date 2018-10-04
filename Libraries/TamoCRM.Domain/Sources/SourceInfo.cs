using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Domain.Sources
{
    public class SourceInfo : BaseClassInfo
    {
    
		public int SourceId { get;set; }
	
		public string Name { get;set; }
	
		public string Code { get;set; }
	
		public string Description { get;set; }
    }
}

