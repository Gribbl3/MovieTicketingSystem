namespace MovieTicketingSystem.Model;

public class Ticket : BaseModel
{
    public int Id { get; set; }
    private User _user;
    private Movie _movie;
    private Seat _seat;

    private bool _isCancelled;
    private DateTime _dateBooked;

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
            OnPropertyChanged();
        }
    }

    public Seat Seat
    {
        get => _seat;
        set
        {
            _seat = value;
            OnPropertyChanged();
        }
    }

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
}

