using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class CinemaPage : ContentPage
{
    CinemaPageViewModel _viewModel;
    public CinemaPage(CinemaPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetCinemasFromJson();
    }
}