using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class MovieService
{
    private readonly string folderPath;
    private readonly string filePath;
    private ObservableCollection<Movie> _movies = new();

    public MovieService()
    {
        folderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies");
    }

    public async Task<bool> AddMovieAsync(Movie movie)
    {
        if (movie == null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize<Movie>(movie);
        await File.WriteAllTextAsync(filePath, json);

        return true;
    }

    public async Task<ObservableCollection<Movie>> GetMoviesAsync()
    {
        //check if directory exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            return new ObservableCollection<Movie>();
        }

        //directory is existing, continue logic
        //get json files from folder path then deserialize
        var files = Directory.EnumerateFiles(folderPath, "*.json");
        foreach (var file in files)
        {
            var json = await File.ReadAllTextAsync(file);
            var _movie = JsonSerializer.Deserialize<Movie>(json);
            _movies.Add(_movie);
        }
        return _movies;
    }
}
