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

            // Register the SpotifyService for dependency injection
            builder.Services.AddSingleton<SpotifyService>();

            // Register the DatabaseService for local data storage
            builder.Services.AddSingleton<DatabaseService>();

            // Register the ViewModels and Pages for navigation
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PlayListViewModel>();
            builder.Services.AddTransient<PlayListPage>();
            builder.Services.AddTransient<MemoryBoardViewModel>();
            builder.Services.AddTransient<MemoryBoardPage>();

            // Define the path for the SQLite database
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "socialhub.db3");
            builder.Services.AddSingleton(new DatabaseService(dbPath));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}