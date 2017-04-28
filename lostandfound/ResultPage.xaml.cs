using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace lostandfound
{
	public partial class ResultPage : ContentPage
	{
		public ResultPage()
		{
			InitializeComponent();
		}

		async public void Claim(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			var res = await PictureUpload.claimItem(((Matched)mi.CommandParameter).id);
			if (res.status == 100)
			{
				await DisplayAlert("Success", "Successfully Claimed this item" , "OK");
				await Navigation.PopToRootAsync(true);
			}
			else
			{
				await DisplayAlert("Error", result.message , "OK");
			}
		}

		RootObject result;
		public List<Matched> matchedList { get; set; }
		public ResultPage(RootObject result)
		{
            InitializeComponent();
			this.result = result;
			//If we have prediction from rest Api client
			if (result.matched!=null)
			{
				if (result.matched.Count > 0)
				{
					for (int i = 0; i < result.matched.Count;i++)
					{
						result.matched[i].image = "http://52.172.50.65/images/" + result.matched[i].image;
					}
					matchedList = result.matched;
					resultList.ItemsSource = matchedList;
				}
			}
			else
			{
				DisplayAlert("Error", "Something wrong occured, try again", "OK");
				//Goes back to First Page
				Navigation.PushModalAsync(new LostPage(), true);
			}
		}

	}
}
