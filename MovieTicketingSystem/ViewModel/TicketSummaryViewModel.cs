using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Cinema), nameof(Cinema))]
[QueryProperty(nameof(User), nameof(User))]
[QueryProperty(nameof(Movie), nameof(Movie))]
public class TicketSummaryViewModel : BaseViewModel
{
    private readonly string ticketFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Ticket.json");

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


    public ICommand BuyTicketCommand => new Command(BuyTicket);

    private async void BuyTicket()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Ticket), Ticket }
        };

        SaveToJson();
        await Shell.Current.GoToAsync($"//{nameof(GeneratedTicket)}", navigationParameter);
    }

    private async void SaveToJson()
    {
        foreach (var seat in SelectedSeats)
        {
            Ticket.Add(new Ticket
            {
                MovieId = Movie.Id,
                UserId = User.Id,
                SeatId = seat.Id
            });
        }
        var json = JsonSerializer.Serialize(Ticket);
        await File.WriteAllTextAsync(ticketFilePath, json);
        await Shell.Current.DisplayAlert("Ticket", "Ticket has been saved", "OK");
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
        SelectedSeatsDisplay = string.Join(", ", SelectedSeats.Select(seat => $"{seat.SeatRow}-{seat.SeatColumn}"));
    }

    private void SetMovieGenreDisplay()
    {
        MovieGenreDisplay = string.Join("| ", Movie.SelectedGenre);
    }



}
