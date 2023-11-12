using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Model;

//class is dependent to Seat and Mall class
public class Cinema : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Mall Mall { get; set; } = new(); //Mall - SM
    public ObservableCollection<Seat> Seats { get; set; } = new();
    public bool IsSelected { get; set; } //used when adding movies, to know if the cinema is already selected
    private int seatCapacity; //Seat - 100

    public int SeatCapacity
    {
        get => seatCapacity;
        set
        {
            seatCapacity = value;
            OnPropertyChanged();
        }
    }
}

