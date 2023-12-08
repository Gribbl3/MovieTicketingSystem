using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class EditMovie : ContentPage
{
    public EditMovie(EditMovieViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}