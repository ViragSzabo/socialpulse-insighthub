using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
using SPIH.Resources.Database;

namespace SPIH
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IAuthorizationService>();
            builder.Services.AddScoped<DatabaseService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
            return builder.Build();
        }
    }
}