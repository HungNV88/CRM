using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TamoCRM.Domain.Contacts
{
    public enum ContactError
    {
        None = 0,
        MobilePhoneFormat = 1,
        HomePhoneFormat = 2,
        EmailFormat = 4,        
        Duplicate = 8,
        InternalDuplicate = 8
    }
}
