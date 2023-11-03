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
    public ICommand DeleteMovieCommand => new Command(DeleteMovie);
    public ICommand EditMovieCommand => new Command(EditMovie);


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
    private async void AddMovieAsync()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(MovieCollection), MovieCollection }
        };
        await Shell.Current.GoToAsync($"{nameof(AddMovie)}", navigationParameter);
    }

    /// <summary>
    /// Delete movie from json file
    /// </summary>
    private async void DeleteMovie()
    {
        string result = await Shell.Current.DisplayPromptAsync("Delete Movie", "Enter movie id to delete", placeholder: "Enter movie id", keyboard: Keyboard.Numeric);
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
        {
            await Shell.Current.DisplayAlert("Error", "Invalid input", "OK");
            return;
        }

        foreach (Movie movie in MovieCollection)
        {
            if (movie.Id == id)
            {
                MovieCollection.Remove(movie);
                string movieJson = JsonSerializer.Serialize(MovieCollection);
                await File.WriteAllTextAsync(movieFilePath, movieJson);
                await Shell.Current.DisplayAlert("Success", "Movie deleted successfully", "OK");
                return;
            }
        }

        await Shell.Current.DisplayAlert("Error", "Movie not found", "OK");
    }


    private async void EditMovie()
    {
        string result = await Shell.Current.DisplayPromptAsync("Edit Movie", "Enter movie id to edit", placeholder: "Enter movie id");
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
        {
            return;
        }

        foreach (Movie movie in MovieCollection)
        {
            if (movie.Id == id)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { nameof(Movie), movie },
                    { nameof(MovieCollection), MovieCollection }
                };
                await Shell.Current.GoToAsync($"{nameof(EditMovie)}", navigationParameter);
                return;
            }
        }
    }

}
