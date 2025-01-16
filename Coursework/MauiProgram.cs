using DatabaseService.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Coursework;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        // Configure the MAUI app
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add Blazor services
        builder.Services.AddMauiBlazorWebView();
        
        // Add MudBlazor services
        builder.Services.AddMudServices();
        // Add Blazor Bootstrap service
        builder.Services.AddBlazorBootstrap();
        
        // Register database services as singletons
        RegisterDatabaseServices(builder.Services);

#if DEBUG
        // Enable developer tools in debug mode
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void RegisterDatabaseServices(IServiceCollection services)
    {
        services.AddSingleton<DatabaseService.Services.DatabaseServices>();
        services.AddSingleton<DatabaseService.Services.dbTransaction>();
        services.AddSingleton<DatabaseService.Services.dbDebt>();
    }
}