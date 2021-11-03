using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PermissionsPage : ContentPage
    {
        public PermissionsPage()
        {
            InitializeComponent();
            InitValues();
        }

        void InitValues()
        {
            var permissionItems = new List<PermissionItem>
            {
                new PermissionItem("Battery", new Permissions.Battery()),
                new PermissionItem("Calendar (Read)", new Permissions.CalendarRead()),
                new PermissionItem("Calendar (Write)", new Permissions.CalendarWrite()),
                new PermissionItem("Camera", new Permissions.Camera()),
                new PermissionItem("Contacts (Read)", new Permissions.ContactsRead()),
                new PermissionItem("Contacts (Write)", new Permissions.ContactsWrite()),
                new PermissionItem("Flashlight", new Permissions.Flashlight()),
                new PermissionItem("Launch Apps", new Permissions.LaunchApp()),
                new PermissionItem("Location (Always)", new Permissions.LocationAlways()),
                new PermissionItem("Location (Only When In Use)", new Permissions.LocationWhenInUse()),
                new PermissionItem("Maps", new Permissions.Maps()),
                new PermissionItem("Media Library", new Permissions.Media()),
                new PermissionItem("Microphone", new Permissions.Microphone()),
                new PermissionItem("Network State", new Permissions.NetworkState()),
                new PermissionItem("Phone", new Permissions.Phone()),
                new PermissionItem("Photos", new Permissions.Photos()),
                new PermissionItem("Reminders", new Permissions.Reminders()),
                new PermissionItem("Sensors", new Permissions.Sensors()),
                new PermissionItem("SMS", new Permissions.Sms()),
                new PermissionItem("Speech", new Permissions.Speech()),
                new PermissionItem("Storage (Read)", new Permissions.StorageRead()),
                new PermissionItem("Storage (Write)", new Permissions.StorageWrite()),
                new PermissionItem("Vibrate", new Permissions.Vibrate())
            };
            MyList.ItemsSource = permissionItems;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PermissionItem, Exception>(this, nameof(PermissionException), async (p, ex) =>
                await DisplayAlert("Permission Error", ex.Message, "OK"));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PermissionItem, Exception>(this, nameof(PermissionException));
        }
    }
}