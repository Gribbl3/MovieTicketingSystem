namespace MovieTicketingSystem.Model;

public class Ticket : BaseModel
{
    private int _id;
    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }

    private int _userId;
    public int UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            OnPropertyChanged();
        }
    }

    private int _cinemaId;
    public int CinemaId
    {
        get => _cinemaId;
        set
        {
            _cinemaId = value;
            OnPropertyChanged();
        }
    }

    private int _movieId;
    public int MovieId
    {
        get => _movieId;
        set
        {
            _movieId = value;
            OnPropertyChanged();
        }
    }


}

