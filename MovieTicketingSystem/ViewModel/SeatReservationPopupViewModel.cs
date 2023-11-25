using MovieTicketingSystem.Model;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;


public class SeatReservationPopupViewModel : BaseViewModel
{
    private bool _isInitialized = false;
    private Cinema _cinema;
    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            OnPropertyChanged();
        }
    }

    private User _user;
    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public void PerformUpdates(Cinema Cinema, User User)
    {
        this.Cinema = Cinema;
        this.User = User;
    }

    public ICommand UpdateSeatAvailabilityCommand => new Command<Seat>(UpdateSeatAvailability);

    private void UpdateSeatAvailability(Seat Seat)
    {
        //get selected seats from collection view
        //update seat availability
        if (Seat == null)
        {
            return;
        }

        if (Seat.IsAvailableSeat)
        {
            Seat.IsAvailableSeat = false;
            return;
        }

        Seat.IsAvailableSeat = true;
    }
}
