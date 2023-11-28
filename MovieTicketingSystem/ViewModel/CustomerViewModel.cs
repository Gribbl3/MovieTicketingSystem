using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(User), nameof(User))]
public class CustomerViewModel : BaseViewModel
{
    private readonly MovieService movieService;
    private readonly SharedDataService sharedDataService;


    //private readonly string movieFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies.json");

    private ObservableCollection<Movie> _movieCollection;
    private User _user;
    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            sharedDataService.UserId = User.Id;
            OnPropertyChanged();
        }
    }


    public CustomerViewModel(MovieService movieService, SharedDataService sharedDataService)
    {
        this.movieService = movieService;
        this.sharedDataService = sharedDataService;
    }

    public ICommand GoToTicketPageCommand => new Command<Movie>(GoToTicketPage);

    private async void GetMoviesAsync()
    {
        MovieCollection = await movieService.GetActiveMoviesAsync();
    }

    private async void GoToTicketPage(Movie Movie)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Movie), Movie },
            { nameof(User), User }
        };
        await Shell.Current.GoToAsync($"{nameof(TicketPage)}", navigationParameter);
    }
}
