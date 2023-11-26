namespace MovieTicketingSystem.Model;

public class Seat : BaseModel
{
    private char _row;
    private int _column;
    private bool _isAvailableSeat; //IsAvailableSeat - true or false
    public int CinemaId { get; set; }
    public int Id { get; set; }
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

}

