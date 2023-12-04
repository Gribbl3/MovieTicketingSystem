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
    private readonly CinemaService cinemaService;
    private readonly MovieService movieService;


    private ObservableCollection<Ticket> _ticket;
    private Cinema _cinema;
    private User _user;
    private Movie _movie;
    private string selectedSeatsDisplay;
    private string _movieGenreDisplay;
    private ObservableCollection<Seat> selectedSeats = new();

    public ObservableCollection<Ticket> Ticket
    {
        get => _ticket;
        set
        {
            _ticket = value;
            OnPropertyChanged();
        }
    }

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

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

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

    public string MovieGenreDisplay
    {
        get => _movieGenreDisplay;
        set
        {
            _movieGenreDisplay = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Seat> SelectedSeats
    {
        get => selectedSeats;
        set
        {
            selectedSeats = value;
            OnPropertyChanged();
        }
    }

    public string SelectedSeatsDisplay
    {
        get => selectedSeatsDisplay;
        set
        {
            selectedSeatsDisplay = value;
            OnPropertyChanged();
        }
    }

    public TicketSummaryViewModel(TicketService ticketService, CinemaService cinemaService, MovieService movieService)
    {
        this.ticketService = ticketService;
        this.cinemaService = cinemaService;
        this.movieService = movieService;
    }

    public ICommand BuyTicketCommand => new Command(BuyTicket);

    private async void BuyTicket()
    {
        UpdateSeatsToReserve();
        CloneSeatsToMovieCinema();

        foreach (var seat in selectedSeats)
        {
            await ticketService.AddTicketAsync(User, Movie, seat);
        }

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Ticket), Ticket }
        };

        await Shell.Current.DisplayAlert("Success", "Ticket(s) bought successfully", "OK");
        await Shell.Current.GoToAsync($"//{nameof(Login)}");
    }

    private async void CloneSeatsToMovieCinema()
    {
        int index = GetCinemaIndex();
        Movie.Cinemas[index] = Cinema;

        var movieCollection = await movieService.GetMoviesAsync();
        await movieService.UpdateMovieAsync(Movie, movieCollection);

    }

    private void UpdateSeatsToReserve()
    {
        foreach (var selectedSeat in selectedSeats)
        {
            Cinema.Seats.FirstOrDefault(s => s.Id == selectedSeat.Id).IsReserved = true;
        }
    }

    private int GetCinemaIndex()
    {
        return Movie.Cinemas.ToList().FindIndex(c => c.Id == Cinema.Id);
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
        SelectedSeatsDisplay = string.Join(", ", SelectedSeats.Select(seat => seat.SeatNumber));
    }

    private void SetMovieGenreDisplay()
    {
        MovieGenreDisplay = string.Join(" | ", Movie.SelectedGenre);
    }
}
