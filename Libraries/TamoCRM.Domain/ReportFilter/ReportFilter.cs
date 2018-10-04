using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.ReportFilter
{
    // model cho report nghiệm thu contact lọc và report tổng hợp chất lượng contact
    public partial class ReportFilter
    {
        public string GroupName { get; set; } 
        public int UserID { get; set; }  
        public string TVTS { get; set; } 
        public int Handover { get; set; }    
        public int Calls { get; set; } 
        public int NotCalls { get; set; }    
        public int Wrong { get; set; } 
        public int Incorrect { get; set; }   
        public int NotConnected { get; set; }
        public int MissCall { get; set; }    
        public int Busy { get; set; } 
        public int L2 { get; set; }  
        public int L3 { get; set; } 
        public int L4 { get; set; }  
        public int L5 { get; set; } 
        public int L6 { get; set; }  
        public int L7 { get; set; } 
        public int L8 { get; set; }
    }
}
