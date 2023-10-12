using Microsoft.Extensions.Logging;
using MovieTicketingSystem.Service;
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
        // Registering ViewModels
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<HomePageViewModel>();
        //viewmodel for customer and admin  
        builder.Services.AddTransient<CustomerViewModel>();
        builder.Services.AddTransient<AdminViewModel>();
        builder.Services.AddTransient<AddMovieViewModel>();

        // Registering View
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<Register>();
        builder.Services.AddTransient<Login>();
        builder.Services.AddTransient<Admin>();
        builder.Services.AddTransient<Customer>();
        builder.Services.AddTransient<AddMovie>();
        builder.Services.AddTransient<AddCinema>();

        // Registering Services
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IMovieService, MovieService>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}