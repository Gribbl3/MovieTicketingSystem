using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class CinemaPage : ContentPage
{
    public CinemaPage(CinemaPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}