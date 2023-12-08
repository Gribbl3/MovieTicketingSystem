using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class TicketPage : ContentPage
{
    public TicketPage(TicketPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Shell.SetTabBarIsVisible(Application.Current.MainPage, false);// set false in second page, set true in first page
    }
}