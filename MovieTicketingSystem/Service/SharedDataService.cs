namespace MovieTicketingSystem.Service;

public class SharedDataService
{
    private int _userId;
    public int UserId
    {
        get => _userId;
        set
        {
            _userId = value;
        }
    }
}
