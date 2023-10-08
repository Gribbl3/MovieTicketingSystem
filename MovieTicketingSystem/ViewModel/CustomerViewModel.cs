using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(FirstName), "name")]
public class CustomerViewModel : BaseViewModel
{

    private readonly IMovieService _movieService;
    private MovieResult _currentItem;
    private string _firstName;
    private List<MovieResult> _moviesFromFile;

    public CustomerViewModel(IMovieService movieService)
    {
        _movieService = movieService;
        MoviesFromFile = _movieService.GetMovieFromFile().Result;
    }

    public List<MovieResult> MoviesFromFile
    {
        get => _moviesFromFile;
        set
        {
            _moviesFromFile = value;
            OnPropertyChanged();
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    public MovieResult CurrentItem
    {
        get => _currentItem;
        set
        {
            _currentItem = value;
            OnPropertyChanged();
        }
    }
}
