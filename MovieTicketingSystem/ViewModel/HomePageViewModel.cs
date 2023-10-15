using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class HomePageViewModel
{
    public ICommand BuyTicketCommand => new Command(BuyTicket);
    public ICommand SignInCommand => new Command(SignIn);
    public ICommand ContactCustomerServiceCommand => new Command(ContactCustomerService);

    private void ContactCustomerService(object obj)
    {
        Shell.Current.DisplayAlert("Contact Customer Service", "Please call 1-800-123-4567", "OK");
    }

    private void SignIn(object obj)
    {
        Shell.Current.DisplayAlert("Sign In", "Please sign in to buy tickets", "OK");
    }

    private void BuyTicket(object obj)
    {
        Shell.Current.DisplayAlert("Buy Ticket", "Please sign in to buy tickets", "OK");
    }
}
