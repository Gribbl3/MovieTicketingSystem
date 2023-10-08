using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class Admin : ContentPage
{
    public Admin(AdminViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "Admin Homepage";
    }
}