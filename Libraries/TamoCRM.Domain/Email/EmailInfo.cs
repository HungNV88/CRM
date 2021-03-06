using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Domain.EmailInfo
{
    public class EmailInfo: BaseClassInfo
    {       
	
		public int Id { get;set; }

        public int EmailType { get; set; }
                
		public string Content { get;set; }
	
		public string Subject { get;set; }			
        
    }
    public class EmailHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public int EmailTemplateId { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

