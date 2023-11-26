namespace MovieTicketingSystem.Model;

public class Ticket : BaseModel
{
    public int Id { get; set; }

    private int _userId, _movieId, _seatId;
    private bool _isCancelled;
    private DateTime _dateBooked;
    public int UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            OnPropertyChanged();
        }
    }
    public int MovieId
    {
        get => _movieId;
        set
        {
            _movieId = value;
            OnPropertyChanged();
        }
    }
    public int SeatId
    {
        get => _seatId;
        set
        {
            _seatId = value;
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

