using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(FirstName), "name")]
public class AdminViewModel : BaseViewModel
{
    private MovieService _movieService;
    private ObservableCollection<Movie> _movieCollection;
    private string _firstName;
    public AdminViewModel(MovieService movieService)
    {
        _movieService = movieService;
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
            LoadMovies();
        }
    }

    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }

    private async void LoadMovies()
    {
        MovieCollection = await _movieService.GetMoviesAsync();
    }


}
