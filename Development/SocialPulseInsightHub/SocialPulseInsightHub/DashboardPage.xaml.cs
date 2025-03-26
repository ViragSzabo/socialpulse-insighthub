namespace SocialPulseInsightHub
{
    public partial class DashboardPage(SocialMediaService socialMediaService) : ContentPage
    {
        private readonly SocialMediaService _socialMediaService = socialMediaService;

        private async void OnGetDataClicked(object sender, EventArgs e)
        {
            var data = await _socialMediaService.GetSocialMediaDataAsync("Instagram");
            await DisplayAlert("Social Media Data", data, "OK");
        }
    }
}