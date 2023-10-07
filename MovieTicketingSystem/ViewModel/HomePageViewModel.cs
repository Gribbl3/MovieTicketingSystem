using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class HomePageViewModel
{
    public ICommand BuyTicketCommand => new Command(BuyTicket);
    public ICommand SignInCommand => new Command(SignIn);

    private void SignIn(object obj)
    {
        throw new NotImplementedException();
    }

    private void BuyTicket(object obj)
    {
        throw new NotImplementedException();
    }
}
