using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Movie), nameof(Movie))]
public class TicketPageViewModel : BaseViewModel
{
    private readonly string mallFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");
    private readonly string cinemaFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");


    private Movie _movie;
    public Movie Movie
    {
        get => _movie;
        set
        {
            _movie = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Mall> _mallCollection = new();
    public ObservableCollection<Mall> MallCollection
    {
        get => _mallCollection;
        set
        {
            _mallCollection = value;
            OnPropertyChanged();
        }
    }

    private bool _isCinemaSelectionEnabled = false;
    public bool IsCinemaSelectionEnabled
    {
        get => _isCinemaSelectionEnabled;
        set
        {
            _isCinemaSelectionEnabled = value;
            OnPropertyChanged();
        }
    }

    private bool _isCinemaSelected = false;
    public bool IsCinemaSelected
    {
        get => _isCinemaSelected;
        set
        {
            _isCinemaSelected = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Cinema> _cinemaCollection = new();
    public ObservableCollection<Cinema> CinemaCollection
    {
        get => _cinemaCollection;
        set
        {
            _cinemaCollection = value;
            OnPropertyChanged();
        }
    }

    public TicketPageViewModel()
    {
        MallCollection = JsonSerializer.Deserialize<ObservableCollection<Mall>>(File.ReadAllText(mallFilePath));
    }

    public ICommand GetCinemaCommand => new Command<Mall>(GetCinemaCollectionFromJson);
    public ICommand GetSeatCommand => new Command<Cinema>(GetSeatCollectionFromCinema);

    private void GetCinemaCollectionFromJson(Mall Mall)
    {
        //get cinema collection from mall
        string json = File.ReadAllText(cinemaFilePath);
        CinemaCollection = JsonSerializer.Deserialize<ObservableCollection<Cinema>>(json);

        //filter cinema collection by mall
        CinemaCollection = new ObservableCollection<Cinema>(CinemaCollection.Where(c => c.Mall.Id == Mall.Id));

        //enable cinema selection
        IsCinemaSelectionEnabled = true;

        //set cinema selected to false to hide seat selection button
        IsCinemaSelected = false;
    }

    private void GetSeatCollectionFromCinema(Cinema Cinema)
    {
        //set cinema selected to true to show seat selection button
        IsCinemaSelected = true;
    }

}
