using System;
using System.Collections.Generic;
using System.IO;
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

			await DisplayAlert("File Location", file.Path, "OK");
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
		}
	}
}
