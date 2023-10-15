using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class Login : ContentPage
{
    public Login(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "Login";
    }
}