using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(MovieCollection), nameof(MovieCollection))]
public class MoviePageViewModel : BaseViewModel
{
    private readonly string movieFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies.json");


    private bool _isVisible = false;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Movie> _movieCollection = new();
    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }


    public MoviePageViewModel()
    {
        GetMoviesFromJson();
    }


    public ICommand AddMovieCommand => new Command(AddMovieAsync);
    public ICommand DeleteMovieCommand => new Command<int>(DeleteMovie);
    public ICommand EditMovieCommand => new Command<Movie>(EditMovie);


    /// <summary>
    /// Get movies from json file
    /// </summary>
    private async void GetMoviesFromJson()
    {
        //check if file exists, return if not
        if (!File.Exists(movieFilePath))
        {
            return;
        }
        //if file exists, read all text from file
        var json = await File.ReadAllTextAsync(movieFilePath);
        MovieCollection = JsonSerializer.Deserialize<ObservableCollection<Movie>>(json);
    }

    /// <summary>
    /// Add movie to json file
    /// </summary>
    /// <returns></returns>
    private async void AddMovieAsync()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(MovieCollection), MovieCollection }
        };
        await Shell.Current.GoToAsync($"{nameof(AddMovie)}", navigationParameter);
    }

    private async void DeleteMovie(int id)
    {
        await Shell.Current.DisplayAlert("Delete Movie", "Not implemented yet", "OK");
    }


    private async void EditMovie(Movie movie)
    {
        await Shell.Current.DisplayAlert("Edit Movie", "Not implemented yet", "OK");
    }

}
