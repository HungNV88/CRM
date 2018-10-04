using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamoCRM.Core;
using TamoCRM.Core.Data;
using TamoCRM.Domain.CC3Report;

namespace TamoCRM.Services.CC3ContactReport
{
    public class CC3ContactReportRepository
    {
        public static void Create(CC3ReportInfo info)
        {
            DataProvider.Instance().CC3ContactReport_Insert(info.FullName, info.PhoneNumber, info.Email, info.Level, info.CallInfoCollaborator, info.HandoverCollaboratorDate, info.CallCollaboratorDate, info.StatusCareCollaboratorId, info.UserCollaboratorId, info.CallCount);
        //public abstract void CC3ContactReport_Insert(string userName, string phoneNumber, string email, string level, string CallInfoCollaborator, DateTime HandoverCollaboratorDate, DateTime CallCollaboratorDate, int StatusCareCollaboratorId, int UserCollaboratorId, int CallCount);  
           
        }
    }
}
