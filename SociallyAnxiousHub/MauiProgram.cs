using SociallyAnxiousHub.Services;

namespace SociallyAnxiousHub
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register InstagramRapidApiService
            builder.Services.AddHttpClient<IInstagramService, InstagramRapidApiService>(client =>
            {
                client.BaseAddress = new Uri("https://instagram-data.p.rapidapi.com");
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}