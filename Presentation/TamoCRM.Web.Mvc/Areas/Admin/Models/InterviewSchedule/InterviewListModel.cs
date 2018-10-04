using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.InterviewSchedule
{
    public class InterviewListModel
    {
        public DateTime AppoimentDate { get; set; }
        public int TimeSlotId { get; set; }
        public string Notes { get; set; }
        public string NotesCM { get; set; }
        public int StatusInterview { get; set; }
        public int TeacherTyprId { get; set; }
        public int CasecAccountId { get; set; }
    }
}