using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppInfoPage : ContentPage
    {
        public AppInfoPage()
        {
            InitializeComponent();
            InitValues();
        }

        void InitValues()
        {
            this.AppName.Text = AppInfo.Name;
            this.AppPackageName.Text = AppInfo.PackageName;
            this.AppVersion.Text = AppInfo.VersionString;
            this.AppBuild.Text = AppInfo.BuildString;
            this.AppTheme.Text = AppInfo.RequestedTheme.ToString();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            AppInfo.ShowSettingsUI();
        }
    }
}