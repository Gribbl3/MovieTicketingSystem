namespace MovieTicketingSystem.Model;

//filter the data from the API
public class MovieResult
{
    public bool Adult { get; set; }
    public string OriginalTitle { get; set; }
    public string Overview { get; set; }
    public string PosterPath { get; set; }
    public string ReleaseDate { get; set; }
    public double VoteAverage { get; set; }
}
