using MovieTicketingSystem.Model;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Ticket), nameof(Ticket))]
public class GeneratedTicketViewModel : BaseViewModel
{
    private Ticket _ticket;
    public Ticket Ticket
    {
        get => _ticket;
        set
        {
            _ticket = value;
            OnPropertyChanged();
        }
    }
}
