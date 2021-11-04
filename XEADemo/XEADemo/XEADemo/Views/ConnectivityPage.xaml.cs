using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XEADemo.DependencyInjection;
using XEADemo.ViewModels;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectivityPage : ContentPage
    {
        public ConnectivityPage()
        {
            InitializeComponent();

            BindingContext = new ConnectivityViewModel(DIContainer.NavigationService);
        }
    }
}