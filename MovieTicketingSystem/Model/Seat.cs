namespace MovieTicketingSystem.Model;

public class Seat : BaseModel
{
    public int Id { get; set; }
    public int CinemaId { get; set; }
    private int _column;
    private char _row;
    private bool _isAvailableSeat;
    private bool _isReserved;

    public char Row
    {
        get => _row;
        set
        {
            _row = value;
            OnPropertyChanged();
        }
    }
    public int Column
    {
        get => _column;
        set
        {
            _column = value;
            OnPropertyChanged();
        }
    }

    public bool IsAvailableSeat
    {
        get => _isAvailableSeat;
        set
        {
            _isAvailableSeat = value;
            OnPropertyChanged();
        }
    }

    public bool IsReserved
    {
        get => _isReserved;
        set
        {
            _isReserved = value;
            OnPropertyChanged();
        }
    }

    public string SeatNumber
    {
        get => $"{Row}-{Column}";
    }

}

