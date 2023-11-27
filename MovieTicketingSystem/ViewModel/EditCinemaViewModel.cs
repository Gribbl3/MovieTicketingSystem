using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
[QueryProperty(nameof(Cinema), nameof(Cinema))]
public class EditCinemaViewModel : BaseViewModel
{
    private readonly CinemaService cinemaService;
    private readonly MallService mallService;

    private ObservableCollection<Mall> _mallCollection = new();
    private ObservableCollection<Cinema> _cinemaCollection = new();
    private Cinema _cinema = new();
    private Mall _selectedMallItem;
    private int _seatCapacity;

    private bool isInitialized = false;

    public ObservableCollection<Cinema> CinemaCollection
    {
        get => _cinemaCollection;
        set
        {
            _cinemaCollection = value;
            OnPropertyChanged();
        }
    }

    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            if (!isInitialized)
            {
                SeatCapacity = Cinema.SeatCapacity;
                SelectedMallItem = Cinema.Mall;
                isInitialized = true;
            }
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

    public ObservableCollection<Mall> MallCollection
    {
        get => _mallCollection;
        set
        {
            _mallCollection = value;
            OnPropertyChanged();
        }
    }

    public EditCinemaViewModel(MallService mallService, CinemaService cinemaService)
    {
        this.mallService = mallService;
        this.cinemaService = cinemaService;
        PopulateMall();
    }

    public ICommand SaveEditCommand => new Command(SaveEdit);
    public ICommand ResetCommand => new Command(Reset);

    private async void SaveEdit()
    {
        bool isValidEdit = ValidateEdit();
        if (!isValidEdit)
            return;

        for (int index = 0; index < CinemaCollection.Count; index++)
        {
            if (CinemaCollection[index].Id == Cinema.Id)
            {
                CinemaCollection[index] = Cinema;
                await cinemaService.SaveToJsonAsync(CinemaCollection);
                await Shell.Current.DisplayAlert("Success", "Cinema edited successfully", "OK");

                var navigationParameter = new Dictionary<string, object>
                {
                    { nameof(CinemaCollection), CinemaCollection }
                };

                await Shell.Current.GoToAsync($"..", navigationParameter);
                return;
            }
        }

        await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");

    }

    private void Reset()
    {
        Cinema = new Cinema();
        SelectedMallItem = null;
        SeatCapacity = 0;
    }

    private bool ValidateEdit()
    {
        return (Cinema.Mall == null);
    }

    private async void PopulateMall()
    {
        MallCollection = await mallService.GetActiveMallsAsync();
    }

    private void GenerateCinemaSeats()
    {
        Cinema.Seats.Clear();

        const int columns = 8;
        int rows = (int)Math.Ceiling((double)Cinema.SeatCapacity / columns);
        for (int row = 1; row <= rows; row++)
        {
            for (int column = 1; column <= columns; column++)
            {
                if ((row - 1) * columns + column <= Cinema.SeatCapacity)
                {
                    Cinema.Seats.Add(new Seat
                    {
                        Row = (char)('A' + row - 1),
                        Column = column,
                        CinemaId = Cinema.Id,
                        IsAvailableSeat = true,
                        IsReserved = false
                    }); ;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
