using MovieTicketingSystem.Model;

namespace MovieTicketingSystem.ViewModel;

public class TicketSummaryPopupViewModel : BaseViewModel
{
    private Cinema cinema;
    private User user;
    private Movie movie;

    public Cinema Cinema
    {
        get => cinema;
        set
        {
            cinema = value;
            OnPropertyChanged();
        }
    }

    public User User
    {
        get => user;
        set
        {
            user = value;
            OnPropertyChanged();
        }
    }

    public Movie Movie
    {
        get => movie;
        set
        {
            movie = value;
            OnPropertyChanged();
        }
    }

    public void PerformUpdates(Cinema Cinema, User User, Movie Movie)
    {
        this.Cinema = Cinema;
        this.User = User;
        this.Movie = Movie;
    }
}
