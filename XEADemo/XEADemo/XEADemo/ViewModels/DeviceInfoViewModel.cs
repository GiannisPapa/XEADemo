﻿using Xamarin.Essentials;

namespace XEADemo.ViewModels
{
    public class DeviceInfoViewModel : BaseViewModel
    {
        public DeviceInfoViewModel()
        {
            ScreenMetrics = DeviceDisplay.MainDisplayInfo;
            DeviceDisplay.MainDisplayInfoChanged -= OnScreenMetricsChanged;
            DeviceDisplay.MainDisplayInfoChanged += OnScreenMetricsChanged;
        }

        DisplayInfo screenMetrics;

        public string Model => DeviceInfo.Model;

        public string Manufacturer => DeviceInfo.Manufacturer;

        public string Name => DeviceInfo.Name;

        public string VersionString => DeviceInfo.VersionString;

        public string Version => DeviceInfo.Version.ToString();

        public DevicePlatform Platform => DeviceInfo.Platform;

        public DeviceIdiom Idiom => DeviceInfo.Idiom;

        public DeviceType DeviceType => DeviceInfo.DeviceType;

        public DisplayInfo ScreenMetrics
        {
            get => screenMetrics;
            set => SetProperty(ref screenMetrics, value);
        }

        void OnScreenMetricsChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            ScreenMetrics = e.DisplayInfo;
        }
    }
}
