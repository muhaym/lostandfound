using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Connectivity;
using Plugin.Media;
using Xamarin.Forms;

namespace lostandfound
{
	public partial class FoundPage : ContentPage
	{
		public FoundPage()
		{
			InitializeComponent();
		}

		Stream memstream;
		async void BrowsePhoto(object sender, System.EventArgs e)
		{
			await CrossMedia.Current.Initialize();
			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				await DisplayAlert("Error", ":( Cannot Access Gallery", "OK");
				return;
			}

			var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions() { CompressionQuality = 86, CustomPhotoSize = 80 });

			if (file == null)
				return;

			//await DisplayAlert("File Location", file.Path, "OK");
			Intro.IsVisible = false;
			TakePhotoButton.IsVisible = false;
			GetResultButton.IsVisible = true;
			memstream = file.GetStream();
			selectedimage.Source = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});

		}

		async void GetResult(object sender, System.EventArgs e)
		{
			loading.IsVisible = true;
			DataGrid.IsVisible = false;
			if (await CrossConnectivity.Current.IsRemoteReachable("http://52.172.50.65/", msTimeout: 5000))
			{
				if (memstream != null)
				{
					if (memstream.CanSeek)
					{
						memstream.Position = 0;
					}
					var result = await PictureUpload.foundPost(memstream);
					if (result.status == 100)
					{
						await DisplayAlert("Success", "Submitted your Image to found database successfully, Thankyou!", "OK");
						await Navigation.PopToRootAsync(true);
					}
					else
					{
						await DisplayAlert("Error", result.message, "OK");
					}
				}
				else
				{
					await DisplayAlert("Error", "File can't be accessed", "OK");
				}
			}
			else
			{
				await DisplayAlert("Error", "Cannot access Internet", "OK");
			}
			loading.IsVisible = false;
			DataGrid.IsVisible = true;
		}
	}
}
