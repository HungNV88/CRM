using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract void CC3ContactReport_Insert(string userName, string phoneNumber, string email, int level, string callInfoCollaborator, DateTime? handoverCollaboratorDate, DateTime? callCollaboratorDate, int statusCareCollaboratorId, int userCollaboratorId, int callCount);  
    }
}
