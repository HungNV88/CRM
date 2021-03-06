using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace TamoCRM.Persitence
{
    public partial class SqlDataProvider
    {
        public override int AppointmentInterview_Insert(int contactId, int userId, DateTime? appointmentDate, int timeSlotId, string notes, string notesCM, int statusInterviewId, int teacherTypeId, int casecAccountId, int levelTesterId)
        {
            return  (int)SqlHelper.ExecuteScalar(ConnectionString, GetFullyQualifiedName("AppointmentInterview_Insert"), contactId, userId, appointmentDate, timeSlotId, notes, notesCM, statusInterviewId, teacherTypeId, casecAccountId, levelTesterId);
        }

        public override void AppointmentInterview_Update(int contactId, int userId, DateTime? appointmentDate, int timeSlotId, string notes, string notesCM, int statusInterviewId, int teacherTypeId,int casecAccountId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AppointmentInterview_Update"),  contactId, userId, appointmentDate, timeSlotId, notes, notesCM, statusInterviewId, teacherTypeId,casecAccountId);
        }

        public override void AppointmentInterview_Delete(int contactId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AppointmentInterview_Delete"), contactId);
        }

        public override IDataReader AppointmentInterview_GetInfo(int id)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("AppointmentInterview_GetInfo"), id);
        }

        public override IDataReader AppointmentInterview_GetAll()
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("AppointmentInterview_GetAll"));
        }
        
        public override IDataReader AppointmentInterview_GetAllByContactId(int contactId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("AppointmentInterview_GetAllByContactId"), contactId);
        }

        public override IDataReader AppointmentInterview_Search(string keyword, int pageIndex, int pageSize)
        {
            return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("AppointmentInterview_Search"), keyword, pageIndex, pageSize);
        }
    }
}

