using Caliburn.Micro;
using CaliburnMicro.ViewModel;
using CaliburnMicro.ViewModels.FirstPage;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaliburnMicro.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public Screen CurrentView { get => _CurrentView; set => Set(ref _CurrentView, value); }
        private Screen _CurrentView;
        public int SelectedViewIndex { get => _SelectedViewIndex; set => Set(ref _SelectedViewIndex, value); }
        private int _SelectedViewIndex;

        public PackIconKind IconShell { get => _IconShell; set => Set(ref _IconShell, value); }
        private PackIconKind _IconShell= PackIconKind.Show;

        public ObservableCollection<SubItem> Menu { get => _Menu; set => Set(ref _Menu, value); }
        private ObservableCollection<SubItem> _Menu = new ObservableCollection<SubItem>
        {
            new SubItem("天干",1)
            {
                Children=new ObservableCollection<SubItem>

            {
                new SubItem("干一",11){ Icon=(PackIconKind)11},
                new SubItem("干二",12){ Icon=(PackIconKind)12},
                new SubItem("干三",13){ Icon=(PackIconKind)13},
            },
                 Icon= (PackIconKind)1
            },
            new SubItem("地支",2)
            {
                Children=new ObservableCollection<SubItem>
            {
                new SubItem("支一",21){ Icon=(PackIconKind)21},
                new SubItem("支二",22){ Icon=(PackIconKind)22},
                new SubItem("支三", 23)
                { 
                    Icon=(PackIconKind)23, 
                    Children=new ObservableCollection<SubItem>
                    {
                         new SubItem("支三一", 231){ Icon=(PackIconKind)231},
                          new SubItem("支三二", 232){ Icon=(PackIconKind)232},
                           new SubItem("支三三", 233){ Icon=(PackIconKind)233},
                    },
                     
                }
            },
                 Icon= (PackIconKind)2
            },
        };
        public void UpdateScreen(SubItem subItem)
        {
            MessageBox.Show($"页面{subItem.Screen}");
            switch(subItem.Screen)
            {
                default:

                    CurrentView=new  FirstPageViewModel();
                    break;
            }
        }
        public void Close()
        {
            this.TryCloseAsync();
        }
    }

    //名字 屏幕编号
    public class SubItem
    {
        public SubItem(string name, int screen)
        {
            Name = name;
            Screen = screen;
        }
        public PackIconKind Icon { get; set; }
        public string Name { get; private set; }
        public int Screen { get; private set; }
        public ObservableCollection<SubItem> Children { get; set; }
    }
}
