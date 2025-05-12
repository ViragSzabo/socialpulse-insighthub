namespace SociallyAnxious.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent(); 
        }

        private async void OnInstagramClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InsightsPage());
        }

        private async void OnTikTokClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InsightsPage());
        }
    }
}