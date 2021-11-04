using XEADemo.Models;
using XEADemo.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace XEADemo.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private INavigation Navigation { get; set; }
        SampleItem[] samples;
        IEnumerable<SampleItem> filteredItems;
        string filterText;

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            NavigateCommand = new Command<SampleItem>(OnNavigate);
            samples = new SampleItem[]
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
                    new[] { "auth", "authenticate", "authenticator", "web", "webauth" }),
            };
            filteredItems = samples;
            filterText = string.Empty;
        }

        public ICommand NavigateCommand { get; private set; }

        public IEnumerable<SampleItem> FilteredItems
        {
            get => filteredItems;
            private set => SetProperty(ref filteredItems, value);
        }

        public string FilterText
        {
            get => filterText;
            set
            {
                SetProperty(ref filterText, value);
                FilteredItems = Filter(samples, value);
            }
        }

        static IEnumerable<SampleItem> Filter(IEnumerable<SampleItem> samples, string filterText)
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

        async void OnNavigate(SampleItem item)
        {
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
        }
    }
}
