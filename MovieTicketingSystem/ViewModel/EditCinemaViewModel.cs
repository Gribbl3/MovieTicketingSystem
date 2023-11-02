using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
[QueryProperty(nameof(Cinema), nameof(Cinema))]
public class EditCinemaViewModel : BaseViewModel
{
    private readonly string cinemaFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");
    private readonly string mallFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");

    public ObservableCollection<Mall> MallCollection { get; set; } = new();
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

    private Cinema _cinema = new();
    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            OnPropertyChanged();
        }
    }

    private Mall _selectedMallItem;
    public Mall SelectedMallItem
    {
        get => _selectedMallItem;
        set
        {
            _selectedMallItem = value;
            OnPropertyChanged();
        }
    }

    private int _seatCapacity;
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

    public EditCinemaViewModel()
    {
        GetMallsFromJson();
    }

    public ICommand SaveEditCommand => new Command(SaveEdit);
    public ICommand ResetCommand => new Command(Reset);

    /// <summary>
    /// Saves edited cinema to json file
    /// </summary>
    private async void SaveEdit()
    {
        bool isValidEdit = await ValidateEdit();
        if (!isValidEdit)
            return;

        for (int index = 0; index < CinemaCollection.Count; index++)
        {
            if (CinemaCollection[index].Id == Cinema.Id)
            {
                CinemaCollection[index] = Cinema;
                string cinemaJson = JsonSerializer.Serialize(CinemaCollection);
                await File.WriteAllTextAsync(cinemaFilePath, cinemaJson);
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
    }

    /// <summary>
    /// Validates cinema name, address and seat capacity
    /// </summary>
    /// <returns>bool</returns>
    private async Task<bool> ValidateEdit()
    {
        if (string.IsNullOrEmpty(Cinema.Mall.Name) || string.IsNullOrEmpty(Cinema.Mall.Address) || Cinema.SeatCapacity == 0)
        {
            await Shell.Current.DisplayAlert("Error", "Please fill up all fields", "OK");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Gets mall collection from json file
    /// </summary>
    private async void GetMallsFromJson()
    {
        string json = await File.ReadAllTextAsync(mallFilePath);
        MallCollection = JsonSerializer.Deserialize<ObservableCollection<Mall>>(json);
        OnPropertyChanged(nameof(MallCollection));
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
                        SeatRow = row,
                        SeatColumn = column,
                        IsAvailableSeat = true
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
