
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClipboardPage : ContentPage
    {
        public ClipboardPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                Clipboard.ClipboardContentChanged += OnClipboardContentChanged;
            }
            catch (FeatureNotSupportedException)
            {
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                Clipboard.ClipboardContentChanged -= OnClipboardContentChanged;
            }
            catch (FeatureNotSupportedException)
            {
            }
        }

        void OnClipboardContentChanged(object sender, EventArgs args)
        {
            this.LastCopied.Text = $"Last copied text at {DateTime.UtcNow:T}";
        }

        private async void ButtonCopy_Clicked(object sender, System.EventArgs e)
        {
            await Clipboard.SetTextAsync(this.FieldValue.Text);
        }

        private async void ButtonPaste_Clicked(object sender, System.EventArgs e)
        {
            var text = await Clipboard.GetTextAsync();
            if (!string.IsNullOrWhiteSpace(text))
            {
                this.FieldValue.Text = text;
            }
        }

        private async void ButtonStatus_Clicked(object sender, System.EventArgs e)
        {
           await DisplayAlert("Title", $"Has text: {Clipboard.HasText}", "Cancel");
        }
    }
}