using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XEADemo.ViewModels;

namespace XEADemo.Services
{
    public class NavigationService : INavigationService
    {
        private Dictionary<string, Type> pages { get; } = new Dictionary<string, Type>();

        public Page MainPage => Application.Current.MainPage;

        public void Map(string key, Type pageType) => pages[key] = pageType;

        public void GoBack() => MainPage.Navigation.PopAsync();

        public void Navigate(string key, object parameter = null)
        {
            if (pages.TryGetValue(key, out Type pageType))
            {
                var page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                MainPage.Navigation.PushAsync(page);

                (page.BindingContext as BaseViewModel).OnNavigated(parameter);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {key}.", nameof(key));
            }
        }
    }

    public static class NavigationExtensions
    {
        private static ConditionalWeakTable<Page, object> arguments = new ConditionalWeakTable<Page, object>();

        public static object GetNavigationArgs(this Page page)
        {
            object argument;
            arguments.TryGetValue(page, out argument);

            return argument;
        }

        public static void SetNavigationArgs(this Page page, object args)
            => arguments.Add(page, args);
    }
}
