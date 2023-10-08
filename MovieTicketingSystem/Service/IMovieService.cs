using MovieTicketingSystem.Model;

namespace MovieTicketingSystem.Service;

public interface IMovieService
{
    public Task<List<MovieResult>> GetMovies();
    public Task<bool> SaveMovieToFile(MovieResult movieResult);
    public Task<List<MovieResult>> GetMovieFromFile();
}
