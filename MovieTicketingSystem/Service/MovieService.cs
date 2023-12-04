using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public class MovieService : BaseService<Movie>
{
    public MovieService() : base("Movies.json")
    {
    }

    public async Task<ObservableCollection<Movie>> GetMoviesAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<ObservableCollection<Movie>> GetActiveMoviesAsync()
    {
        var movieCollection = await GetItemsAsync();
        return new ObservableCollection<Movie>(movieCollection.Where(m => !m.IsDeleted));
    }

    public async Task<ObservableCollection<Movie>> GetDeletedMoviesAsync()
    {
        var movieCollection = await GetItemsAsync();
        return new ObservableCollection<Movie>(movieCollection.Where(m => m.IsDeleted));
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movieCollection = await GetMoviesAsync();
        return movieCollection.FirstOrDefault(m => m.Id == id);
    }

    public async Task<(bool, ObservableCollection<Movie>)> AddMovieAsync(Movie newMovie)
    {
        var movieCollection = await GetMoviesAsync();
        if (newMovie == null || movieCollection == null)
        {
            return (false, movieCollection);
        }

        newMovie.Id = movieCollection.Count + 1;
        movieCollection.Add(newMovie);
        bool isSaved = await SaveToJsonAsync(movieCollection);

        return (isSaved, movieCollection);
    }

    public async Task<ObservableCollection<Movie>> DeleteMovieAsync(int id)
    {
        var movieCollection = await GetMoviesAsync();
        var movieToBeDeleted = movieCollection.FirstOrDefault(m => m.Id == id);
        if (movieToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Movie not found", "OK");
            return movieCollection;
        }

        if (movieToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Movie already deleted", "OK");
            return movieCollection;
        }

        movieToBeDeleted.IsDeleted = true;
        bool isSaved = await SaveToJsonAsync(movieCollection);
        if (!isSaved)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to delete movie", "OK");
        }

        return movieCollection;
    }

    public async Task<(bool, ObservableCollection<Movie>)> UpdateMovieAsync(Movie movie, ObservableCollection<Movie> movieCollection)
    {
        if (movie == null || movieCollection == null)
        {
            return (false, movieCollection);
        }

        int index = movieCollection.ToList().FindIndex(m => m.Id == movie.Id);
        if (index == -1)
        {
            await Shell.Current.DisplayAlert("Error", "Movie not found", "OK");
            return (false, movieCollection);
        }

        movieCollection[index] = movie;

        bool isSaved = await SaveToJsonAsync(movieCollection);
        if (!isSaved)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to edit movie", "OK");
        }

        await Shell.Current.DisplayAlert("Success", "Movie updated", "OK");

        return (isSaved, movieCollection);
    }

    public async Task<ObservableCollection<Movie>> GetNowShowingMoviesAsync()
    {
        var movieCollection = await GetActiveMoviesAsync();
        return new ObservableCollection<Movie>(movieCollection.Where(m => m.IsNowShowing));
    }

    public async Task<ObservableCollection<Movie>> GetUpcomingMoviesAsync()
    {
        var movieCollection = await GetActiveMoviesAsync();
        return new ObservableCollection<Movie>(movieCollection.Where(m => m.IsUpcoming));
    }
}
