using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(FirstName), "name")]
public class AdminViewModel : BaseViewModel
{
    private readonly IMovieService _movieService;
    private string _firstName;
    private MovieResult _currentItem;
    private List<MovieResult> _movies;

    public AdminViewModel(IMovieService movieService)
    {
        _movieService = movieService;
        Movies = _movieService.GetMovies().Result;
        _currentItem = null;
    }
    public ICommand SaveToFileCommand => new Command<MovieResult>(SaveToFile);

    public MovieResult CurrentItem
    {
        get => _currentItem;
        set
        {
            _currentItem = value;
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
    public List<MovieResult> Movies
    {
        get => _movies;
        set
        {
            _movies = value;
            OnPropertyChanged();
        }
    }

    private void SaveToFile(MovieResult result)
    {
        if (result == null)
        {
            Shell.Current.DisplayAlert("Save to File", "Please select a movie", "OK");
            return;
        }

        bool isSaved = _movieService.SaveMovieToFile(result).Result;
        if (isSaved)
        {
            Shell.Current.DisplayAlert("Save to File", $"Movie {result.OriginalTitle} saved to file", "OK");
            return;
        }

        Shell.Current.DisplayAlert("Save to File", $"Movie {result.OriginalTitle} not saved to file", "OK");
    }


}
