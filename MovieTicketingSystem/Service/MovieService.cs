using MovieTicketingSystem.Model;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

//TMDB API service  
public class MovieService : IMovieService
{
    private const string API_KEY = "f68b0777b789e8a564e19969ebfdd215";
    private const string BASE_URL = "https://api.themoviedb.org/3";
    private const string IMAGE_URL = "https://image.tmdb.org/t/p/w500";

    private readonly string mainDir = FileSystem.Current.AppDataDirectory;
    private readonly string fileName = "SelectedMovies.json";

    private readonly HttpClient _httpClient = new();


    public Task<List<MovieResult>> GetMovies()
    {
        string endpoint = "/movie/popular";
        string fullUrl = $"{BASE_URL}{endpoint}?api_key={API_KEY}";

        var response = _httpClient.GetAsync(fullUrl).Result;
        if (response.IsSuccessStatusCode)
        {
            var json = response.Content.ReadAsStringAsync().Result;
            var movieCollection = JsonSerializer.Deserialize<Movie>(json);

            var filteredResults = new List<MovieResult>();
            foreach (var result in movieCollection.results)
            {
                string absolutePath = $"{IMAGE_URL}{result.poster_path}";
                //filter the data from the API by creating a new MovieResult object
                //and add it to the filteredResults list
                filteredResults.Add(new MovieResult
                {
                    Adult = result.adult,
                    OriginalTitle = result.original_title,
                    Overview = result.overview,
                    PosterPath = absolutePath,
                    ReleaseDate = result.release_date,
                    VoteAverage = result.vote_average
                });
            }
            return Task.FromResult(filteredResults);
        }
        //else return an empty list
        return Task.FromResult(new List<MovieResult>());
    }



    public Task<bool> SaveMovieToFile(MovieResult movieResult)
    {
        if (movieResult == null)
        {
            return Task.FromResult(false);
        }

        string filePath = Path.Combine(mainDir, fileName);

        var movieCollection = GetMovieFromFile().Result;
        movieCollection.Add(movieResult);

        string json = JsonSerializer.Serialize(movieCollection);
        File.WriteAllText(filePath, json);

        return Task.FromResult(true);
    }


    public Task<List<MovieResult>> GetMovieFromFile()
    {
        string filePath = Path.Combine(mainDir, fileName);
        if (!File.Exists(filePath))
        {
            return Task.FromResult(new List<MovieResult>());
        }

        string json = File.ReadAllText(filePath);
        var movieCollection = JsonSerializer.Deserialize<List<MovieResult>>(json);

        return Task.FromResult(movieCollection);
    }
}
