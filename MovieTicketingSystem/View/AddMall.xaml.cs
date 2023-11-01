using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class AddMall : ContentPage
{
    AddMallViewModel _viewModel;
    public AddMall(AddMallViewModel viewModel)
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