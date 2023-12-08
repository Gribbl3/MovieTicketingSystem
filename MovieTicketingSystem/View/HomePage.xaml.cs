using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class HomePage : ContentPage
{
    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "Home";
    }
}