using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(User), nameof(User))]
public class CustomerViewModel : BaseViewModel
{
    private readonly string movieFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies.json");


    public DateTime DateNow { get; set; } = DateTime.Now;

    private ObservableCollection<Movie> _movieCollection;
    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }

    private User _user;
    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }


    public CustomerViewModel()
    {
        MovieCollection = JsonSerializer.Deserialize<ObservableCollection<Movie>>(File.ReadAllText(movieFilePath));
    }

    public ICommand GoToTicketPageCommand => new Command<Movie>(GoToTicketPage);

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
