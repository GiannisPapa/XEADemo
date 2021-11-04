using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XEADemo.Services;

namespace XEADemo.ViewModels
{
    public class ConnectivityViewModel : BaseViewModel
    {
        INavigationService _navigationService;

        public ConnectivityViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;

            GoBackCommand = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        public ICommand GoBackCommand { get; private set; }
        

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

        public override void OnNavigated(object parameters)
        {
            base.OnNavigated(parameters);

            if (parameters is string message)
                Message = message;
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ConnectionProfiles));
            OnPropertyChanged(nameof(NetworkAccess));
        }
    }
}
