using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliburnMicro.ViewModel
{
    public class ViewModelBase:Screen
    {
        public IWindowManager WindowManager =>
           IoC.Get<IWindowManager>();

        public IEventAggregator EventAggregator
            => IoC.Get<IEventAggregator>();
    }
}
