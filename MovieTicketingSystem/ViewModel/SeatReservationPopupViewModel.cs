using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;


public class SeatReservationPopupViewModel : BaseViewModel
{

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

    public Movie Movie { get; set; }


    public void PerformUpdates(Cinema Cinema, User User, Movie Movie)
    {
        this.Cinema = Cinema;
        this.User = User;
        this.Movie = Movie;
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
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Cinema), Cinema },
            { nameof(User), User },
            { nameof(Movie), Movie },
        };
        await Shell.Current.GoToAsync($"{nameof(TicketSummary)}", navigationParameter);
    }
}
