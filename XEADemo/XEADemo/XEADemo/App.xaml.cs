using XEADemo.Views;
using Xamarin.Forms;
using XEADemo.DependencyInjection;

namespace XEADemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DIContainer.Initialize();

            MainPage = new NavigationPage(new MainPage());
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
