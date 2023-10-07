using Microsoft.Extensions.Logging;
using MovieTicketingSystem.View;
using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem;

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
                fonts.AddFont("MaterialIcons_Regular.ttf", "MaterialIcon");
            });

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomePageViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}