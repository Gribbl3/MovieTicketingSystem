using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class CustomerTransaction : ContentPage
{
    public CustomerTransaction(CustomerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}