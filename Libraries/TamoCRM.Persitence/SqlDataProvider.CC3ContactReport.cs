using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override void CC3ContactReport_Insert(string userName, string phoneNumber, string email, int level, string callInfoCollaborator, DateTime? handoverCollaboratorDate, DateTime? callCollaboratorDate, int statusCareCollaboratorId, int userCollaboratorId, int callCount)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CC3ContactReport_Insert"), userName, phoneNumber, email, level, callCollaboratorDate, handoverCollaboratorDate, callCollaboratorDate, statusCareCollaboratorId, userCollaboratorId, callCount);
        }
    }
}
