using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CaliburnMicro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //private AppBootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //处理UI线程上的未处理异常
            e.Handled = true;
            // MessageBox.Show("发生错误：" + e.Exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //处理非UI线程上的未处理异常
            Exception ex = (Exception)e.ExceptionObject;
            // MessageBox.Show("发生错误：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Task线程内未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs ex)
        {
            // MessageBox.Show("Task线程异常：" + ex.Exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            //设置该异常已察觉（这样处理后就不会引起程序崩溃）
            ex.SetObserved();
        }
    }
}
