using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Cinema), nameof(Cinema))]
[QueryProperty(nameof(User), nameof(User))]
[QueryProperty(nameof(Movie), nameof(Movie))]
public class TicketSummaryViewModel : BaseViewModel
{
    private readonly TicketService ticketService;


    private ObservableCollection<Ticket> _ticket;
    public ObservableCollection<Ticket> Ticket
    {
        get => _ticket;
        set
        {
            _ticket = value;
            OnPropertyChanged();
        }
    }

    private Cinema _cinema;
    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            GetSelectedSeats();
            SetSelectedSeatsDisplay();
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

    private Movie _movie;
    public Movie Movie
    {
        get => _movie;
        set
        {
            _movie = value;
            SetMovieGenreDisplay();
            OnPropertyChanged();
        }
    }

    private string _movieGenreDisplay;
    public string MovieGenreDisplay
    {
        get => _movieGenreDisplay;
        set
        {
            _movieGenreDisplay = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Seat> selectedSeats = new();
    public ObservableCollection<Seat> SelectedSeats
    {
        get => selectedSeats;
        set
        {
            selectedSeats = value;
            OnPropertyChanged();
        }
    }

    private string selectedSeatsDisplay;
    public string SelectedSeatsDisplay
    {
        get => selectedSeatsDisplay;
        set
        {
            selectedSeatsDisplay = value;
            OnPropertyChanged();
        }
    }

    public TicketSummaryViewModel(TicketService ticketService)
    {
        this.ticketService = ticketService;
    }

    public ICommand BuyTicketCommand => new Command(BuyTicket);

    private async void BuyTicket()
    {
        await ticketService.AddTicketAsync(User, Movie);
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Ticket), Ticket }
        };

        await Shell.Current.GoToAsync($"//{nameof(GeneratedTicket)}", navigationParameter);
    }


    private void GetSelectedSeats()
    {
        foreach (var seat in Cinema.Seats)
        {
            if (!seat.IsAvailableSeat)
            {
                SelectedSeats.Add(seat);
            }
        }
    }


    private void SetSelectedSeatsDisplay()
    {
        SelectedSeatsDisplay = string.Join(", ", SelectedSeats.Select(seat => $"{seat.Row}-{seat.Column}"));
    }

    private void SetMovieGenreDisplay()
    {
        MovieGenreDisplay = string.Join("| ", Movie.SelectedGenre);
    }



}
