using System;
using System.Data;

namespace TamoCRM.Core.Data
{
    public abstract partial class DataProvider
    {
        public abstract int AppointmentInterview_Insert(int contactId, int userId, DateTime? appointmentDate, int timeSlotId, string notes, string notesCM, int statusInterviewId, int teacherTypeId, int casecAccountId, int levelTesterId);
        public abstract void AppointmentInterview_Update(int contactId, int userId, DateTime? appointmentDate, int timeSlotId, string notes, string notesCM, int statusInterviewId, int teacherTypeId,int casecAccountId);
        public abstract void AppointmentInterview_Delete(int contactId);
        public abstract IDataReader AppointmentInterview_GetInfo(int id);
        public abstract IDataReader AppointmentInterview_GetAll();
        public abstract IDataReader AppointmentInterview_GetAllByContactId(int contactId);
        public abstract IDataReader AppointmentInterview_Search(string keyword, int pageIndex, int pageSize);
    }
}

