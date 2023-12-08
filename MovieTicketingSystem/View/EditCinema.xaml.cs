using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class EditCinema : ContentPage
{
    public EditCinema(EditCinemaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}