using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class Customer : ContentPage
{
    public Customer(CustomerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "User Homepage";
    }
}