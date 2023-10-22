namespace MovieTicketingSystem.Model;

public class Booking : BaseModel
{
    public User User { get; set; } = new(); //User - John Doe
    private int _ticketQuantity;

    public int TicketQuantity
    {
        get => _ticketQuantity;
        set
        {
            _ticketQuantity = value;
            OnPropertyChanged();
        }
    }
}

