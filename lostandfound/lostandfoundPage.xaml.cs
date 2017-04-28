using Xamarin.Forms;

namespace lostandfound
{
	public partial class lostandfoundPage : ContentPage
	{
		async void IFoundHandle(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new FoundPage());
		}

		async void ILostHandle(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new LostPage());
		}

		public lostandfoundPage()
		{
			InitializeComponent();
		}
	}
}
