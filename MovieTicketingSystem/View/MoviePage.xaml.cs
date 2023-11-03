using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class MoviePage : ContentPage
{
    MoviePageViewModel _viewModel;
    public MoviePage(MoviePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.IsVisible = !(_viewModel.MovieCollection.Count == 0);
    }
}