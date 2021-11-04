using Autofac;
using XEADemo.Services;

namespace XEADemo.DependencyInjection
{
    public class DIContainer
    {
        private static ContainerBuilder _builder;
        private static IContainer Container { get; set; }

        public static IDialogService DialogService
        {
            get
            {
                return Container?.Resolve<IDialogService>();
            }
        }

        public static INavigationService NavigationService
        {
            get
            {
                return Container?.Resolve<INavigationService>();
            }
        }

        public static void Initialize()
        {
            if (_builder == null)
            {
                _builder = new ContainerBuilder();
                _builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
                _builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
                Container = _builder.Build();
            }
        }
    }
}
