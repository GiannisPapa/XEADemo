using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceInfoPage : ContentPage
    {
        public DisplayInfo ScreenMetrics { get; private set; }

        public DeviceInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DeviceDisplay.MainDisplayInfoChanged += OnScreenMetricsChanged;
            ScreenMetrics = DeviceDisplay.MainDisplayInfo;
            InitValues();
        }

        protected override void OnDisappearing()
        {
            DeviceDisplay.MainDisplayInfoChanged -= OnScreenMetricsChanged;

            base.OnDisappearing();
        }

        void OnScreenMetricsChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            ScreenMetrics = e.DisplayInfo;
            RefreshMetricsValues();
        }

        void RefreshMetricsValues()
        {
            this.Width.Text = $"Width: { ScreenMetrics.Width}";
            this.Height.Text = $"Height: { ScreenMetrics.Height}";
            this.Density.Text = $"Density: { ScreenMetrics.Density}";
            this.Orientation.Text = $"Orientation: { ScreenMetrics.Orientation}";
            this.Rotation.Text = $"Rotation: { ScreenMetrics.Rotation}";
            this.RefreshRate.Text = $"RefreshRate: { ScreenMetrics.RefreshRate}";
        }

        void InitValues()
        {
            this.Model.Text = $"Model: { DeviceInfo.Model }";
            this.Manufacturer.Text = $"Manufacturer: {DeviceInfo.Manufacturer}";
            this.Name.Text = $"Name: { DeviceInfo.Name }";
            this.VersionString.Text = $"VersionString: { DeviceInfo.VersionString }";
            this.Version.Text = $"Version: { DeviceInfo.Version}";
            this.Platform.Text = $"Platform: { DeviceInfo.Platform}";
            this.Idiom.Text = $"Idiom: { DeviceInfo.Idiom }";
            this.DeviceType.Text = $"DeviceType: { DeviceInfo.DeviceType}";
            this.Width.Text = $"Width: { ScreenMetrics.Width}";
            this.Height.Text = $"Height: { ScreenMetrics.Height}";
            this.Density.Text = $"Density: { ScreenMetrics.Density}";
            this.Orientation.Text = $"Orientation: { ScreenMetrics.Orientation}";
            this.Rotation.Text = $"Rotation: { ScreenMetrics.Rotation}";
            this.RefreshRate.Text = $"RefreshRate: { ScreenMetrics.RefreshRate}";
        }
    }
}