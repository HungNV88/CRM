using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamoCRM.Core.Commons
{
    public interface INotifyPropertyChange
    {
        void SetNotifyPropertyChange(bool isNotify);
        void NotifyPropertyChange(int logPropertyField, object logPropertyValue);
    }
}
