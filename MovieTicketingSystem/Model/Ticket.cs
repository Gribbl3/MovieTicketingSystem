namespace MovieTicketingSystem.Model;

public class Ticket : BaseModel
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    private bool _isCancelled;
    private DateTime _dateBooked;

    public DateTime DateBooked
    {
        get => _dateBooked;
        set
        {
            _dateBooked = value;
            OnPropertyChanged();
        }
    }

    public bool IsCancelled
    {
        get => _isCancelled;
        set
        {
            _isCancelled = value;
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

    private Seat _seat;
    public Seat Seat
    {
        get => _seat;
        set
        {
            _seat = value;
            OnPropertyChanged();
        }
    }
}

