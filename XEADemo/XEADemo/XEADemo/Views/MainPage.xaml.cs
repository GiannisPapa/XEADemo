using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XEADemo.Views
{
    public partial class MainPage
    {
        private List<SampleItem> sampleItems;
        public MainPage()
        {
            InitializeComponent();
            InitValues();
        }

        void InitValues()
        {
            sampleItems = new List<SampleItem>
            {
                new SampleItem(
                    "📦",
                    "App Info",
                    typeof(AppInfoPage),
                    "Find out about the app with ease.",
                    new[] { "app", "info" }),
                new SampleItem(
                    "📋",
                    "Clipboard",
                    typeof(ClipboardPage),
                    "Quickly and easily use the clipboard.",
                    new[] { "clipboard", "copy", "paste" }),
                new SampleItem(
                    "📶",
                    "Connectivity",
                    typeof(ConnectivityPage),
                    "Check connectivity state and detect changes.",
                    new[] { "connectivity", "internet", "wifi" }),
                new SampleItem(
                    "📱",
                    "Device Info",
                    typeof(DeviceInfoPage),
                    "Find out about the device with ease.",
                    new[] { "hardware", "device", "info", "screen", "display", "orientation", "rotation" }),
                new SampleItem(
                    "📁",
                    "File Picker",
                    typeof(FilePickerPage),
                    "Easily pick files from storage.",
                    new[] { "files", "picking", "filesystem", "storage" }),
                new SampleItem(
                    "🔒",
                    "Permissions",
                    typeof(PermissionsPage),
                    "Request various permissions.",
                    new[] { "permissions" }),
                new SampleItem(
                    "🔓",
                    "Web Authenticator",
                    typeof(WebAuthenticatorPage),
                    "Quickly and easily authenticate and wait for a callback.",
                    new[] { "auth", "authenticate", "authenticator", "web", "webauth" })
            };
            this.MyList.ItemsSource = sampleItems;
        }

        async void OnSampleTapped(object sender, ItemTappedEventArgs e)
        {
            SampleItem item = e.Item as SampleItem;
            if (item == null)
                return;

            if (item.PageType == typeof(AppInfoPage))
            {
                await Navigation.PushAsync(new AppInfoPage());
            }
            else if (item.PageType == typeof(ClipboardPage))
            {
                await Navigation.PushAsync(new ClipboardPage());
            }
            else if (item.PageType == typeof(ConnectivityPage))
            {
                await Navigation.PushAsync(new ConnectivityPage());
            }
            else if (item.PageType == typeof(DeviceInfoPage))
            {
                await Navigation.PushAsync(new DeviceInfoPage());
            }
            else if (item.PageType == typeof(FilePickerPage))
            {
                await Navigation.PushAsync(new FilePickerPage());
            }
            else if (item.PageType == typeof(PermissionsPage))
            {
                await Navigation.PushAsync(new PermissionsPage());
            }
            else if (item.PageType == typeof(WebAuthenticatorPage))
            {
                await Navigation.PushAsync(new WebAuthenticatorPage());
            }

            // deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is SearchBar search && search.Text != null)
            {
                MyList.ItemsSource = Filter(sampleItems, search.Text);
            }
        }

        IEnumerable<SampleItem> Filter(IEnumerable<SampleItem> samples, string filterText)
        {
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                var lower = filterText.ToLowerInvariant();
                samples = samples.Where(s =>
                {
                    var tags = s.Tags
                        .Union(new[] { s.Name })
                        .Select(t => t.ToLowerInvariant());
                    return tags.Any(t => t.Contains(lower));
                });
            }

            return samples.OrderBy(s => s.Name);
        }
    }
}
