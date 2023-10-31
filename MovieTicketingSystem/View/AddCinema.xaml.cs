using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class AddCinema : ContentPage
{
    public AddCinema(AddCinemaViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}