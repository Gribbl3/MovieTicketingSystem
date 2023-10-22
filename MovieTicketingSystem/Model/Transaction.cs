namespace MovieTicketingSystem.Model;

public class Transaction : BaseModel
{
    public Ticket Ticket { get; set; }
}

