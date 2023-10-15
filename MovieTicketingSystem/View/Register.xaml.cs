using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class Register : ContentPage
{
    public Register(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "Register";
    }
}