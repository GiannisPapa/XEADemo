using XEADemo.Models;
using System;
using Xamarin.Forms;
using XEADemo.DependencyInjection;
using System.Threading.Tasks;
using XEADemo.ViewModels;

namespace XEADemo.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(DIContainer.NavigationService);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
