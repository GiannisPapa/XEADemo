using XEADemo.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PermissionsPage : ContentPage
    {
        public PermissionsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Forms.MessagingCenter.Subscribe<PermissionItem, Exception>(this, nameof(PermissionException), async (p, ex) =>
                await DisplayAlert("Permission Error", ex.Message, "OK"));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Xamarin.Forms.MessagingCenter.Unsubscribe<PermissionItem, Exception>(this, nameof(PermissionException));
        }
    }
}