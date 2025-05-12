using SociallyAnxious.Pages;

namespace SociallyAnxious
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(InsightsPage), typeof(InsightsPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

        }
    }
}