using XEADemo.Models;
using System;
using Xamarin.Forms;
using XEADemo.DependencyInjection;
using System.Threading.Tasks;

namespace XEADemo.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnSampleTapped(object sender, ItemTappedEventArgs e)
        {
            SampleItem item = e.Item as SampleItem;
            if (item == null)
                return;

            await Navigation.PushAsync((Page)Activator.CreateInstance(item.PageType));

            // deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var message = DIContainer.DialogService?.Message();
            DisplayAlert("Message", message, "Cancel");
        }
    }
}
