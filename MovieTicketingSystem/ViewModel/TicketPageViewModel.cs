using CommunityToolkit.Maui.Core;
using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Movie), nameof(Movie))]
[QueryProperty(nameof(User), nameof(User))]
public class TicketPageViewModel : BaseViewModel
{
    //private readonly string mallFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");
    //private readonly string cinemaFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");
    private readonly MallService mallService;
    private readonly CinemaService cinemaService;
    private readonly IPopupService popupService;


    private Movie _movie;
    private Cinema _cinema;
    private User _user;
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
    public User User
    {
        get => _user;
        set
        {
            _user = value;
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


    public TicketPageViewModel(IPopupService popupService, MallService mallService, CinemaService cinemaService)
    {
        this.mallService = mallService;
        this.cinemaService = cinemaService;
        this.popupService = popupService;
        PopulateMallCollection();
    }

    public ICommand GetCinemaCommand => new Command<Mall>(GetCinemaCollectionFromJson);
    public ICommand GetSeatCommand => new Command<Cinema>(GetSeatCollectionFromCinema);
    public ICommand DisplayPopupCommand => new Command(DisplayPopup);

    private async void PopulateMallCollection()
    {
        MallCollection = await mallService.GetMallsAsync();
        //filter deleted malls
        MallCollection = new ObservableCollection<Mall>(MallCollection.Where(m => !m.IsDeleted));
    }

    private async void GetCinemaCollectionFromJson(Mall Mall)
    {
        CinemaCollection = await cinemaService.GetCinemasAsync();
        //filter cinema collection by mall
        //CinemaCollection = new ObservableCollection<Cinema>(CinemaCollection.Where(c => c.Mall.Id == Mall.Id));

        List<int> movieCinemaIds = Movie.Cinemas.Select(c => c.Id).ToList();
        CinemaCollection = new ObservableCollection<Cinema>(CinemaCollection.Where(c => movieCinemaIds.Contains(c.Id)));

        //enable cinema selection
        IsCinemaSelectionEnabled = true;

        //set cinema selected to false to hide seat selection button
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
        this.popupService.ShowPopup<SeatReservationPopupViewModel>(onPresenting: viewModel => viewModel.PerformUpdates(Cinema, User, Movie));
    }
}


