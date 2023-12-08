using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class CustomerTransaction : ContentPage
{
    CustomerTransactionViewModel viewModel;
    public CustomerTransaction(CustomerTransactionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.GetUserTransactionAsync();
        viewModel.IsVisible = !(viewModel.Transactions.Count == 0);
        viewModel.IsTextVisible = !viewModel.IsVisible;
        viewModel.GetUserTransactionAsync();
    }
}