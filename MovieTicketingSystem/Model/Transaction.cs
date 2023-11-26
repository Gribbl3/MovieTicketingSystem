namespace MovieTicketingSystem.Model;

public class Transaction : BaseModel
{
    public int Id { get; set; }
    private int _ticketId;
    public int TicketId
    {
        get => _ticketId;
        set
        {
            _ticketId = value;
            OnPropertyChanged();
        }
    }

    private bool _isPaid;
    public bool IsPaid
    {
        get => _isPaid;
        set
        {
            _isPaid = value;
            OnPropertyChanged();
        }
    }

    private DateTime _datePaid;
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

