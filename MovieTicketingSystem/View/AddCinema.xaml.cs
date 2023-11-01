using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class AddCinema : ContentPage
{
    AddCinemaViewModel _viewModel;
    public AddCinema(AddCinemaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetMallsFromJson();
    }
}