using System;

namespace TamoCRM.Domain
{
    [Serializable]
   public class BaseClassInfo
    {
        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ChangedBy { get; set; }

        public DateTime ChangedDate { get; set; }
    }
}
