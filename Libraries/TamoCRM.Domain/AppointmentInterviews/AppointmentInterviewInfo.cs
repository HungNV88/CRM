using System;
using TamoCRM.Core.Commons.Extensions;

namespace TamoCRM.Domain.AppointmentInterviews
{
    public class AppointmentInterviewInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
        public int ContactId { get; set; }
        public string NotesCM { get; set; }
        public int TimeSlotId { get; set; }
        public int LevelTesterId { get; set; }
        public int TeacherTypeId { get; set; }
        public int CasecAccountId { get; set; }
        public int StatusInterviewId { get; set; }
        public DateTime? AppointmentDate { get; set; }

        private string _AppointmentDateString;
        public string AppointmentDateString
        {
            get
            {
                return AppointmentDate.HasValue 
                    ? AppointmentDate.Value.ToString("dd/MM/yyyy") 
                    : _AppointmentDateString;
            }
            set
            {
                if (!value.IsStringNullOrEmpty())
                {
                    AppointmentDate = value.ToDateTime();
                }
                _AppointmentDateString = value;
            }
        }

        public string TimeSlotName { get; set; }
        public string TeacherTypeName { get; set; }
        public string StatusInterviewName { get; set; }
    }
}

