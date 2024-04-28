using CaliburnMicro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliburnMicro.ViewModels.Heavenly_Stems.H1_os
{
    public class H12ViewModel:ViewModelBase
    {
        public string PageName { get => _PageName; set => Set(ref _PageName, value); }
        private string _PageName = "H12ViewModel";
    }
}
