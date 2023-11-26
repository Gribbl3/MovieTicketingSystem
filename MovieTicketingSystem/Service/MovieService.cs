using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class MovieService
{
    private readonly string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies.json");

    public async Task<bool> AddUpdateMovieAsync(ObservableCollection<Movie> MovieCollection)
    {
        if (MovieCollection == null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize<ObservableCollection<Movie>>(MovieCollection);
        await File.WriteAllTextAsync(filePath, json);

        return true;
    }

    public async Task<ObservableCollection<Movie>> GetMoviesAsync()
    {
        //check if file pathexists
        if (!File.Exists(filePath))
        {
            return new ObservableCollection<Movie>();
        }

        //get json file from file path then deserialize
        var json = await File.ReadAllTextAsync(filePath);
        var movieCollection = JsonSerializer.Deserialize<ObservableCollection<Movie>>(json);
        return movieCollection;
    }
}
