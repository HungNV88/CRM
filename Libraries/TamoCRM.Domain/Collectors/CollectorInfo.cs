using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Domain.Collectors
{
    public class CollectorInfo : BaseClassInfo
    { 
		public int CollectorId { get;set; }
	
		public string Code { get;set; }
	
		public string Name { get;set; }
	
		public string Description { get;set; }
	
		
	
    }
}

