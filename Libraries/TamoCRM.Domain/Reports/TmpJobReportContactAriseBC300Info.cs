using System;

namespace TamoCRM.Domain.Reports
{
    /// <summary>
    /// phulv
    /// nhứng contact phát sinh của tư vấn tuyển sinh trong một khoảng thời gian (x,y)
    /// những contact phát sinh được hiểu là những contact có ngày bàn giao nằm ngoài khoảng thời gian (x,y)
    /// và có thay đổi level trong khoảng thời gian (x,y)
    /// </summary>
    public class TmpJobReportContactAriseBC300Info
    {
        public int TVTSId { get; set; }
        public int Level { get; set; }
    }
}
