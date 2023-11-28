using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class CustomerTransaction : ContentPage
{
    public CustomerTransaction(CustomerTransactionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}