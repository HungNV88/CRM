using System;
using TamoCRM.Domain.Contacts;

namespace TamoCRM.Domain.Reports
{
    public class TmpJobReportBC10Info : ContactInfo
    {
        public int PropertyType { get; set; }
        public int PropertyValueInt { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
