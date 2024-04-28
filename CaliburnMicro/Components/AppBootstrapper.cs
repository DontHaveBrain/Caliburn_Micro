using Caliburn.Micro; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using CaliburnMicro.Views;
using System.Reflection;
using System.Windows.Threading;
using CaliburnMicro.ViewModels;

namespace CaliburnMicro.Components
{
    internal class AppBootstrapper:BootstrapperBase
    {
        private CompositionContainer _container;//MEF组合，用于管理依赖关系
        private IWindowManager _windowManager;//管理窗口
        private IEventAggregator _eventAggregator;//用于传递事件
        public AppBootstrapper()
        {
            Initialize();
        }
        protected override void Configure()
        {
            var batch = new CompositionBatch();

            //绑定所有组件和服务
            var aggregateCatalog = new AggregateCatalog(
                              AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());
            _container = new CompositionContainer(aggregateCatalog);
            batch.AddExportedValue(_container);

            //注入IoC
            _windowManager = new WindowManager();//绑定管理窗口工具
            batch.AddExportedValue(_windowManager);

            _eventAggregator = new EventAggregator();//绑定事件聚合器
            batch.AddExportedValue(_eventAggregator);

            _container.Compose(batch);
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            ShellViewModel? viewModel = new ShellViewModel();
            _windowManager.ShowWindowAsync(viewModel);
        }
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            base.OnUnhandledException(sender, e);
        }

        protected override object GetInstance(Type service, string key)
        {
            string contract = string.IsNullOrEmpty(key)
               ? AttributedModelServices.GetContractName(service)
               : key;

            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Any())
                return exports.First();

            throw new Exception($"找不到实例 {contract}。");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = new List<Assembly>()
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly(),
            };

            return assemblies.Where(x => x != null).Distinct();
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce((ComposablePart)instance);
        }
    }
}
