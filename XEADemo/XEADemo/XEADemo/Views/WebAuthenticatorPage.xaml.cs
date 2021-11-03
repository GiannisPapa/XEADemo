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
    public partial class WebAuthenticatorPage : ContentPage
    {
        private string authenticationUrl = "https://xamarin-essentials-auth-sample.azurewebsites.net/mobileauth/";

        public WebAuthenticatorPage()
        {
            InitializeComponent();
        }

        async void MicrosoftButton_Clicked(object sender, EventArgs e)
        {
            await OnAuthenticate("Microsoft");
        }

        async void GoogleButton_Clicked(object sender, EventArgs e)
        {
            await OnAuthenticate("Google");
        }

        async void FacebookButton_Clicked(object sender, EventArgs e)
        {
            await OnAuthenticate("Facebook");
        }

        async void AppleButton_Clicked(object sender, EventArgs e)
        {
            await OnAuthenticate("Apple");
        }

        async Task OnAuthenticate(string scheme)
        {
            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple")
                    && DeviceInfo.Platform == DevicePlatform.iOS
                    && DeviceInfo.Version.Major >= 13)
                {
                    // Make sure to enable Apple Sign In in both the
                    // entitlements and the provisioning profile.
                    var options = new AppleSignInAuthenticator.Options
                    {
                        IncludeEmailScope = true,
                        IncludeFullNameScope = true,
                    };
                    r = await AppleSignInAuthenticator.AuthenticateAsync(options);
                }
                else
                {
                    var authUrl = new Uri(authenticationUrl + scheme);
                    var callbackUrl = new Uri("myapp://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                this.AuthTokenLabel.Text = string.Empty;
                if (r.Properties.TryGetValue("name", out var name) && !string.IsNullOrEmpty(name))
                    this.AuthTokenLabel.Text += $"Name: {name}{Environment.NewLine}";
                if (r.Properties.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
                    this.AuthTokenLabel.Text += $"Email: {email}{Environment.NewLine}";
                this.AuthTokenLabel.Text += r?.AccessToken ?? r?.IdToken;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Login canceled.");

                this.AuthTokenLabel.Text = string.Empty;
                await DisplayAlert("Error", "Login canceled.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message}");

                this.AuthTokenLabel.Text = string.Empty;
                await DisplayAlert("Failed", $"Error Message: {ex.Message}", "OK");
            }
        }
    }
}