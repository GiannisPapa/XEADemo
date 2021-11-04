using XEADemo.Views;
using Xamarin.Forms;
using XEADemo.DependencyInjection;
using XEADemo.Services;

namespace XEADemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DIContainer.Initialize();

            NavigationMap();

            MainPage = new NavigationPage(new MainPage());
        }

        private void NavigationMap()
        {
            var navigationService = DIContainer.NavigationService;
            navigationService.Map(NavigationKeys.AppInfoPage, typeof(AppInfoPage));
            navigationService.Map(NavigationKeys.ClipboardPage, typeof(ClipboardPage));
            navigationService.Map(NavigationKeys.ConnectivityPage, typeof(ConnectivityPage));
            navigationService.Map(NavigationKeys.DeviceInfoPage, typeof(DeviceInfoPage));
            navigationService.Map(NavigationKeys.FilePickerPage, typeof(FilePickerPage));
            navigationService.Map(NavigationKeys.PermissionsPage, typeof(PermissionsPage));
            navigationService.Map(NavigationKeys.WebAuthenticatorPage, typeof(WebAuthenticatorPage));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
