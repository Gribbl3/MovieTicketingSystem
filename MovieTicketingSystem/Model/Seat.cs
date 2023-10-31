namespace MovieTicketingSystem.Model;

public class Seat : BaseModel
{
    //one cinema has many seats
    //one mall has many cinemas
    private int _seatRow; //SeatRows - 10
    private int _seatColumn; //SeatColumns - 10
    private bool _isAvailableSeat; //IsAvailableSeat - true or false

    public int SeatRow
    {
        get => _seatRow;
        set
        {
            _seatRow = value;
            OnPropertyChanged();
        }
    }
    public int SeatColumn
    {
        get => _seatColumn;
        set
        {
            _seatColumn = value;
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
}

