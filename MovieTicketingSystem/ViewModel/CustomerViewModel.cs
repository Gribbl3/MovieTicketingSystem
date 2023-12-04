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
    private ObservableCollection<Movie> _searchedMovieCollection = new();

    private bool _isSearchBarFocused = false;


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

    public ObservableCollection<Movie> SearchedMovieCollection
    {
        get => _searchedMovieCollection;
        set
        {
            _searchedMovieCollection = value;
            OnPropertyChanged();
        }
    }

    public bool IsSearchBarFocused
    {
        get => _isSearchBarFocused;
        set
        {
            _isSearchBarFocused = value;
            OnPropertyChanged();
        }
    }



    public CustomerViewModel(MovieService movieService, SharedDataService sharedDataService)
    {
        this.movieService = movieService;
        this.sharedDataService = sharedDataService;
    }

    public ICommand GoToTicketPageCommand => new Command<Movie>(GoToTicketPage);
    public ICommand PerformSearchCommand => new Command<string>(PerformSearch);

    private async void GoToTicketPage(Movie Movie)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Movie), Movie },
            { nameof(User), User }
        };
        await Shell.Current.GoToAsync($"{nameof(TicketPage)}", navigationParameter);
    }

    private async void PerformSearch(string query)
    {
        IsSearchBarFocused = true;
        if (string.IsNullOrWhiteSpace(query))
        {
            SearchedMovieCollection.Clear();
            return;
        }

        SearchedMovieCollection = await movieService.SearchMoviesAsync(query);
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
