using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(MovieCollection), nameof(MovieCollection))]
public class MoviePageViewModel : BaseViewModel
{
    private readonly MovieService movieService;


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
            IsVisible = MovieCollection.Count > 0;
            OnPropertyChanged();
        }
    }


    public MoviePageViewModel(MovieService movieService)
    {
        this.movieService = movieService;
    }


    public ICommand AddMovieCommand => new Command(AddMovieAsync);
    public ICommand DeleteMovieCommand => new Command(DeleteMovie);
    public ICommand EditMovieCommand => new Command(EditMovie);
    public ICommand ShowActiveMoviesCommand => new Command(ShowActiveMovies);
    public ICommand ShowDeletedMoviesCommand => new Command(ShowDeletedMovies);
    public ICommand ShowAllMoviesCommand => new Command(ShowAllCinemas);

    private async void ShowAllCinemas()
    {
        MovieCollection = await movieService.GetMoviesAsync();
    }

    private async void ShowActiveMovies()
    {
        MovieCollection = await movieService.GetActiveMoviesAsync();
    }

    private async void ShowDeletedMovies()
    {
        MovieCollection = await movieService.GetDeletedMoviesAsync();
    }

    private async void AddMovieAsync()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(MovieCollection), MovieCollection }
        };
        await Shell.Current.GoToAsync($"{nameof(AddMovie)}", navigationParameter);
    }

    private async void DeleteMovie()
    {
        string result = await Shell.Current.DisplayPromptAsync("Delete Movie", "Enter movie id to delete", placeholder: "Enter movie id", keyboard: Keyboard.Numeric);
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
        {
            return;
        }

        foreach (Movie movie in MovieCollection)
        {
            if (movie.Id == id)
            {
                movie.IsDeleted = !movie.IsDeleted;
                MovieCollection.Remove(movie);

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
