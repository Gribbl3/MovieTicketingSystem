namespace MovieTicketingSystem.Model;

public class Ticket : BaseModel
{
    private readonly string _ticketId;
    private DateTime _date; //Date - 2021-10-10
    private DateTime _time; //Time - 10:00 AM
    public Ticket()
    {
        _ticketId = Guid.NewGuid().ToString();
    }
}

