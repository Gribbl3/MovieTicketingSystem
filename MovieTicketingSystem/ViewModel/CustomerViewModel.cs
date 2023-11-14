using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Customer), nameof(Customer))]
public class CustomerViewModel : BaseViewModel
{
    private JsonFileService _service;
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

    public CustomerViewModel(JsonFileService service)
    {
        _service = service;
        MovieCollection = JsonSerializer.Deserialize<ObservableCollection<Movie>>(File.ReadAllText(movieFilePath));
    }

    public ICommand GoToTicketPageCommand => new Command<Movie>(GoToTicketPage);

    private async void GoToTicketPage(Movie Movie)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Movie), Movie }
        };
        await Shell.Current.GoToAsync($"{nameof(TicketPage)}",navigationParameter);
    }
}
