using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class AddMovie : ContentPage
{
    public AddMovie(AddMovieViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}