using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XEADemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilePickerPage : ContentPage
    {
        public FilePickerPage()
        {
            InitializeComponent();
        }

        async void PickFileButton_Clicked(object sender, EventArgs e)
        {
            await PickAndShow(PickOptions.Default);
        }

        async void PickImageButton_Clicked(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "Please select an image",
                FileTypes = FilePickerFileType.Images,
            };

            await PickAndShow(options);
        }

        async void PickPdfButton_Clicked(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "Please select a pdf",
                FileTypes = FilePickerFileType.Pdf,
            };

            await PickAndShow(options);
        }

        async void PickCustomTypeButton_Clicked(object sender, EventArgs e)
        {
            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } },
                    { DevicePlatform.UWP, new[] { ".cbr", ".cbz" } },
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
                });

            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };

            await PickAndShow(options);
        }

        async void PickAndSendButton_Clicked(object sender, EventArgs e)
        {
            // pick a file
            var result = await PickAndShow(PickOptions.Images);
            if (result == null)
                return;

            // copy it locally
            var copyPath = Path.Combine(FileSystem.CacheDirectory, result.FileName);
            using (var destination = File.Create(copyPath))
            using (var source = await result.OpenReadAsync())
                await source.CopyToAsync(destination);

            // send it via an email
            await Email.ComposeAsync(new EmailMessage
            {
                Subject = "Test Subject",
                Body = "This is the body. There should be an image attached.",
                Attachments =
                {
                    new EmailAttachment(copyPath)
                }
            });
        }

        async void PickMultipleFilesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var resultList = await FilePicker.PickMultipleAsync();

                if (resultList != null && resultList.Any())
                {
                    LabelText.Text = "File Names: " + string.Join(", ", resultList.Select(result => result.FileName));

                    // only showing the first file's content
                    var firstResult = resultList.First();

                    if (firstResult.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        firstResult.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await firstResult.OpenReadAsync();
                        MImage.Source = ImageSource.FromStream(() => stream);
                        MImage.IsVisible = true;
                    }
                    else
                    {
                        MImage.IsVisible = false;
                    }
                }
                else
                {
                    LabelText.Text = $"Pick cancelled.";
                    MImage.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                LabelText.Text = ex.ToString();
                MImage.IsVisible = false;
            }
        }

        async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    var size = await GetStreamSizeAsync(result);

                    LabelText.Text = $"File Name: {result.FileName} ({size:0.00} KB)";

                    var ext = Path.GetExtension(result.FileName).ToLowerInvariant();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        var stream = await result.OpenReadAsync();

                        MImage.Source = ImageSource.FromStream(() => stream);
                        MImage.IsVisible = true;
                    }
                    else
                    {
                        MImage.IsVisible = false;
                    }
                }
                else
                {
                    LabelText.Text = $"Pick cancelled.";
                }

                return result;
            }
            catch (Exception ex)
            {
                LabelText.Text = ex.ToString();
                MImage.IsVisible = false;
                return null;
            }
        }

        async Task<double> GetStreamSizeAsync(FileResult result)
        {
            try
            {
                using (var stream = await result.OpenReadAsync())
                {
                    return stream.Length / 1024.0;
                }
            }
            catch
            {
                return 0.0;
            }
        }
    }
}