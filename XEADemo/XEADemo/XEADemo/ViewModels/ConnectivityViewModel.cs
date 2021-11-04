using Xamarin.Essentials;

namespace XEADemo.ViewModels
{
    public class ConnectivityViewModel : BaseViewModel
    {
        public ConnectivityViewModel()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public string NetworkAccess =>
            Connectivity.NetworkAccess.ToString();

        public string ConnectionProfiles
        {
            get
            {
                var profiles = string.Empty;
                foreach (var p in Connectivity.ConnectionProfiles)
                    profiles += "\n" + p.ToString();
                return profiles;
            }
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ConnectionProfiles));
            OnPropertyChanged(nameof(NetworkAccess));
        }
    }
}
