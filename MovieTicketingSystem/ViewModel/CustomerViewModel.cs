using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;
[QueryProperty(nameof(User), nameof(User))]
public class CustomerViewModel : BaseViewModel
{
    private readonly MovieService movieService;
    private readonly SharedDataService sharedDataService;


    //private readonly string movieFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies.json");

    private User _user;
    private ObservableCollection<Movie> _nowShowingMovieCollection = new();
    private ObservableCollection<Movie> _upcomingMovieCollection = new();

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            sharedDataService.UserId = User.Id;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Movie> NowShowingMovieCollection
    {
        get => _nowShowingMovieCollection;
        set
        {
            _nowShowingMovieCollection = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Movie> UpcomingMovieCollection
    {
        get => _upcomingMovieCollection;
        set
        {
            _upcomingMovieCollection = value;
            OnPropertyChanged();
        }
    }


    public CustomerViewModel(MovieService movieService, SharedDataService sharedDataService)
    {
        this.movieService = movieService;
        this.sharedDataService = sharedDataService;
    }

    public ICommand GoToTicketPageCommand => new Command<Movie>(GoToTicketPage);

    private async void GoToTicketPage(Movie Movie)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Movie), Movie },
            { nameof(User), User }
        };
        await Shell.Current.GoToAsync($"{nameof(TicketPage)}", navigationParameter);
    }

    public async void PopulateNowShowingMovies()
    {
        NowShowingMovieCollection = await movieService.GetNowShowingMoviesAsync();
    }

    public async void PopulateUpcomingMovies()
    {
        UpcomingMovieCollection = await movieService.GetUpcomingMoviesAsync();
    }
}
