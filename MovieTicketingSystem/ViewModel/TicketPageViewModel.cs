using CommunityToolkit.Maui.Core;
using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Movie), nameof(Movie))]
[QueryProperty(nameof(User), nameof(User))]
public class TicketPageViewModel : BaseViewModel
{
    //private readonly string mallFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");
    //private readonly string cinemaFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");
    private readonly IPopupService popupService;


    private Movie _movie;
    private User _user;
    private Cinema _cinema;
    private ObservableCollection<Mall> _mallCollection = new();
    private ObservableCollection<Cinema> _cinemaCollection = new();
    private bool _isCinemaSelectionEnabled = false;
    private bool _isCinemaSelected = false;


    public Movie Movie
    {
        get => _movie;
        set
        {
            _movie = value;
            PopulateMalls();
            OnPropertyChanged();
        }
    }

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Mall> MallCollection
    {
        get => _mallCollection;
        set
        {
            _mallCollection = value;
            OnPropertyChanged();
        }
    }
    public bool IsCinemaSelectionEnabled
    {
        get => _isCinemaSelectionEnabled;
        set
        {
            _isCinemaSelectionEnabled = value;
            OnPropertyChanged();
        }
    }
    public bool IsCinemaSelected
    {
        get => _isCinemaSelected;
        set
        {
            _isCinemaSelected = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<Cinema> CinemaCollection
    {
        get => _cinemaCollection;
        set
        {
            _cinemaCollection = value;
            OnPropertyChanged();
        }
    }


    public TicketPageViewModel(IPopupService popupService)
    {
        this.popupService = popupService;
    }

    public ICommand GetCinemaCommand => new Command<Mall>(GetCinemaCollection);
    public ICommand GetSeatCommand => new Command<Cinema>(GetSeatCollectionFromCinema);
    public ICommand DisplayPopupCommand => new Command(DisplayPopup);

    private void PopulateMalls()
    {
        MallCollection = new ObservableCollection<Mall>(
        Movie.Cinemas
            .Select(c => c.Mall)             // Select the Mall property from each Cinema
            .GroupBy(mall => mall.Id)        // Group by Mall Id
            .Select(group => group.First())  // Take the first mall in each group
            .ToList()                         // Convert the result to a list
        );
    }
    private void GetCinemaCollection(Mall Mall)
    {
        CinemaCollection = new ObservableCollection<Cinema>(Movie.Cinemas.Where(cinema => cinema.Mall.Id == Mall.Id));

        IsCinemaSelectionEnabled = true;

        IsCinemaSelected = false;
    }

    private void GetSeatCollectionFromCinema(Cinema Cinema)
    {
        //set cinema selected to true to show seat selection button
        IsCinemaSelected = true;
        this.Cinema = Cinema;
    }

    private void DisplayPopup()
    {
        this.popupService.ShowPopup<SeatReservationPopupViewModel>(onPresenting: viewModel => viewModel.PerformUpdates(User, Movie, Cinema));
    }
}


