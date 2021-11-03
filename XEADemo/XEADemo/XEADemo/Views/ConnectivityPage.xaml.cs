using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectivityPage : ContentPage
    {
        public ConnectivityPage()
        {
            InitializeComponent();
            InitValues();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        protected override void OnDisappearing()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;

            base.OnDisappearing();
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            this.NetworkAccess.Text = $"NetworkAccess: { Connectivity.NetworkAccess }";
            this.ConnectionProfiles.Text = $"NetworkAccess: { GetConnectionProfiles }";
        }

        void InitValues()
        {
            this.NetworkAccess.Text = $"NetworkAccess: { Connectivity.NetworkAccess }";
            this.ConnectionProfiles.Text = $"NetworkAccess: { GetConnectionProfiles }";
        }

        public string GetConnectionProfiles
        {
            get
            {
                var profiles = string.Empty;
                foreach (var p in Connectivity.ConnectionProfiles)
                    profiles += "\n" + p.ToString();
                return profiles;
            }
        }
    }
}