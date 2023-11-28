namespace MovieTicketingSystem.Model;

public class Transaction : BaseModel
{
    public int Id { get; set; }
    private int _ticketId;
    private bool _isPaid;
    private DateTime _datePaid;

    public int TicketId
    {
        get => _ticketId;
        set
        {
            _ticketId = value;
            OnPropertyChanged();
        }
    }
    public bool IsPaid
    {
        get => _isPaid;
        set
        {
            _isPaid = value;
            OnPropertyChanged();
        }
    }
    public DateTime DatePaid
    {
        get => _datePaid;
        set
        {
            if (!IsPaid)
                return;

            _datePaid = value;
            OnPropertyChanged();
        }
    }
}

