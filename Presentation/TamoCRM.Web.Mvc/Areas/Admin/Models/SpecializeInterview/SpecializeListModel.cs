using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.SpecializeInterview
{
    public class SpecializeListModel
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