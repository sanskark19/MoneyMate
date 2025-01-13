using Microsoft.Extensions.Logging;

namespace Coursework;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        
        // Add Blazor Bootstrap service
        builder.Services.AddBlazorBootstrap();
        
        // Register the DatabaseService as a singleton
        builder.Services.AddSingleton<DatabaseService.Services.DatabaseServices>();
        builder.Services.AddSingleton<DatabaseService.Services.dbTransaction>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}