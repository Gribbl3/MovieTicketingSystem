using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;


public class SeatReservationPopupViewModel : BaseViewModel
{
    private User _user;
    private Cinema _cinema;

    public Movie Movie { get; set; }

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            OnPropertyChanged();
        }
    }

    public void PerformUpdates(User User, Movie Movie, Cinema Cinema)
    {
        this.User = User;
        this.Movie = Movie;
        this.Cinema = Cinema;
    }

    public ICommand UpdateSeatAvailabilityCommand => new Command<Seat>(UpdateSeatAvailability);

    private void UpdateSeatAvailability(Seat Seat)
    {
        if (Seat == null || Seat.IsReserved)
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

    public async void GoToTicketSummary()
    {
        //update selected movie cinema seats

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(User), User },
            { nameof(Movie), Movie },
            { nameof(Cinema), Cinema }
        };
        await Shell.Current.GoToAsync($"{nameof(TicketSummary)}", navigationParameter);
    }
}
