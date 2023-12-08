using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
public class AddCinemaViewModel : BaseViewModel
{
    private readonly CinemaService cinemaService;
    private readonly MallService mallService;
    public Cinema Cinema { get; set; } = new();
    public ObservableCollection<Cinema> CinemaCollection { get; set; } = new();
    private ObservableCollection<Mall> _mallCollection = new();
    private Mall _selectedMallItem;
    private int _seatCapacity;


    public ObservableCollection<Mall> MallCollection
    {
        get => _mallCollection;
        set
        {
            _mallCollection = value;
            OnPropertyChanged();
        }
    }
    public Mall SelectedMallItem
    {
        get => _selectedMallItem;
        set
        {
            _selectedMallItem = value;
            OnPropertyChanged();
        }
    }
    public int SeatCapacity
    {
        get => _seatCapacity;
        set
        {
            Cinema.SeatCapacity = _seatCapacity = value;
            GenerateCinemaSeats();
            OnPropertyChanged();
        }
    }


    public AddCinemaViewModel(MallService mallService, CinemaService cinemaService)
    {
        this.mallService = mallService;
        this.cinemaService = cinemaService;
        GetActiveMalls();
    }


    public ICommand SaveCommand => new Command(async () => await AddCinema());
    public ICommand ResetCommand => new Command(ResetCinema);


    private async Task AddCinema()
    {
        bool isValidCinema = ValidateCinema();
        if (!isValidCinema)
        {
            return;
        }

        var (isSaved, updatedCollection) = await cinemaService.AddCinemaAsync(Cinema);
        CinemaCollection = updatedCollection;

        if (isSaved)
        {
            await Shell.Current.DisplayAlert("Success", "Cinema added successfully", "OK");
            //Go back to cinema page
            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(CinemaCollection), CinemaCollection }
            };
            await Shell.Current.GoToAsync($"..", navigationParameter);
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Failed to add cinema", "OK");
        }
    }

    private void ResetCinema()
    {
        Cinema = new Cinema();

        //update frontend that cinema is reset
        OnPropertyChanged(nameof(Cinema));
        SeatCapacity = 0;
        SelectedMallItem = null;
    }

    private bool ValidateCinema()
    {
        if (SelectedMallItem == null)
        {
            Shell.Current.DisplayAlert("Error", "Please select mall", "OK");
            return false;
        }
        if (Cinema.SeatCapacity == 0)
        {
            Shell.Current.DisplayAlert("Error", "Please enter cinema seat capacity", "OK");
            return false;
        }

        Cinema.Mall = SelectedMallItem;
        return true;
    }

    public async void GetActiveMalls()
    {
        MallCollection = await mallService.GetActiveMallsAsync();
    }

    private void GenerateCinemaSeats()
    {
        Cinema.Seats.Clear();
        int id = 1;
        const int columns = 8;
        int rows = (int)Math.Ceiling((double)Cinema.SeatCapacity / columns);
        for (int row = 1; row <= rows; row++)
        {
            for (int column = 1; column <= columns; column++)
            {
                if ((row - 1) * columns + column <= Cinema.SeatCapacity)
                {
                    char seatRow = (char)('A' + (row - 1));
                    Cinema.Seats.Add(new Seat
                    {
                        Id = id++,
                        Row = seatRow,
                        Column = column,
                        CinemaId = Cinema.Id,
                        IsAvailableSeat = true,
                        IsReserved = false
                    });
                }
                else
                {
                    break;
                }
            }
        }
    }
}
