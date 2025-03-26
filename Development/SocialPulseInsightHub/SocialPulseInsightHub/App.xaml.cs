namespace SocialPulseInsightHub
{
    public partial class App : Application
    {
        public App(SocialMediaService socialMediaService)
        {
            //InitializeComponent();
            MainPage = new DashboardPage(socialMediaService);
        }
    }
}
