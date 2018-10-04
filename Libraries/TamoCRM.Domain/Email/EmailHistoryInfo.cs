using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Domain.Email
{
    class EmailHistoryInfo : BaseClassInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public int EmailTemplateId { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
