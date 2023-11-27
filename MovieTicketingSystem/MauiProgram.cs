using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MovieTicketingSystem.Converters;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem;

public static class MauiProgram
{
    public static object CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons_Regular.ttf", "MaterialIcon");
            });
        // Registering ViewModels
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();

        //viewmodel for admin's page
        //cinema
        builder.Services.AddTransient<CinemaPageViewModel>();
        builder.Services.AddTransient<AddCinemaViewModel>();
        builder.Services.AddTransient<EditCinemaViewModel>();
        //mall
        builder.Services.AddTransient<AddMallViewModel>();

        //movie
        builder.Services.AddTransient<AddMovieViewModel>();
        builder.Services.AddTransient<MoviePageViewModel>();
        builder.Services.AddTransient<EditMovieViewModel>();

        builder.Services.AddTransient<HomePageViewModel>();
        //viewmodel for customer and admin  
        builder.Services.AddTransient<CustomerViewModel>();
        builder.Services.AddTransient<AdminViewModel>();

        //viewmodel for ticket page
        builder.Services.AddTransient<TicketPageViewModel>();
        builder.Services.AddTransient<TicketSummaryViewModel>();


        // Registering View
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<Register>();
        builder.Services.AddTransient<Login>();
        builder.Services.AddTransient<Admin>();
        builder.Services.AddTransient<Customer>();
        builder.Services.AddTransient<AddMovie>();
        builder.Services.AddTransient<AddCinema>();
        builder.Services.AddTransient<AddMall>();
        builder.Services.AddTransient<CinemaPage>();
        builder.Services.AddTransient<EditCinema>();
        builder.Services.AddTransient<MoviePage>();
        builder.Services.AddTransient<EditMovie>();
        builder.Services.AddTransient<TicketPage>();
        builder.Services.AddTransient<TicketSummary>();
        builder.Services.AddTransient<GeneratedTicket>();


        // Registering Services
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<MovieService>();
        builder.Services.AddTransient<MallService>();
        builder.Services.AddTransient<CinemaService>();

        //converter
        builder.Services.AddSingleton<ByteArrayToImageSourceConverter>();

        //popup

        builder.Services.AddTransientPopup<SeatReservationPopup, SeatReservationPopupViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}