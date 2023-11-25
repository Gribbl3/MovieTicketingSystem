using MovieTicketingSystem.Model;

namespace MovieTicketingSystem.ViewModel;

public class TicketSummaryViewModel : BaseViewModel
{
    private Cinema _cinema;
    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            OnPropertyChanged();
        }
    }

    private User _user;
    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    private Movie _movie;
    public Movie Movie
    {
        get => _movie;
        set
        {
            _movie = value;
            OnPropertyChanged();
        }
    }


    public TicketSummaryViewModel(Cinema Cinema, User User, Movie Movie)
    {
        this.Cinema = Cinema;
        this.User = User;
        this.Movie = Movie;
    }
}
