using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Domain.UserDraft
{
    public class UserDraftInfo
    {
        public UserDraftInfo()
        {
            
        }
	
		public int Id { get;set; }
	
		public int UserId { get;set; }
	
		public int BranchId { get;set; }
	
		public bool IsDraftCollabortor { get;set; }
	
		public bool IsDraftConsultant { get;set; }
	
		public DateTime CreatedDate { get;set; }
	
    }
}

