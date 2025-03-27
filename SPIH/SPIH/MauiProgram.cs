using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using SPIH.Resources.Database;

namespace SPIH
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Register services
            builder.UseMauiApp<App>()
                   .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

            //Register authorization services
            builder.Services.AddAuthorizationCore();

            // Register other services
            builder.Services.AddScoped<DatabaseService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}