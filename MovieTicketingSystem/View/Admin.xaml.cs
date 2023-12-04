using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class Admin : ContentPage
{
    AdminViewModel viewModel;
    public Admin(AdminViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "Admin Homepage";
        viewModel.GetAllUserAsync();
    }
}