using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Model;

//class is dependent to Seat and Mall class
public class Cinema : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ObservableCollection<Seat> Seats { get; set; } = new();
    public Mall Mall { get; set; } = new();
    private int seatCapacity; //Seat - 100
    private bool _isSelected, _isDeleted; //IsDeleted - hide or show
    public int SeatCapacity
    {
        get => seatCapacity;
        set
        {
            seatCapacity = value;
            OnPropertyChanged();
        }
    }
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }
    public bool IsDeleted
    {
        get => _isDeleted;
        set
        {
            _isDeleted = value;
            OnPropertyChanged();
        }
    }
}

