using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class Customer : ContentPage
{
    CustomerViewModel viewModel;
    public Customer(CustomerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Title = "User Homepage";
        viewModel.PopulateUpcomingMovies();
        viewModel.PopulateNowShowingMovies();
    }
}