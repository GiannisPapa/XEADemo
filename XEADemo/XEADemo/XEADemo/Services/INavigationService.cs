using System;
using Xamarin.Forms;

namespace XEADemo.Services
{
    public interface INavigationService
    {
        Page MainPage { get; }

        void Map(string key, Type pageType);
        void GoBack();
        void Navigate(string key, object parameter = null);
    }
}
