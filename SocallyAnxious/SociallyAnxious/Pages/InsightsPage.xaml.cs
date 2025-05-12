namespace SociallyAnxious.Pages
{
    public partial class InsightsPage : ContentPage
    {
        public InsightsPage()
        {
            InitializeComponent();
        }

        private async void OnHomePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}