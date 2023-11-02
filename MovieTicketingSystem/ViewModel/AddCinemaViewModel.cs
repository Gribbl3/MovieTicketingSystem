using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
public class AddCinemaViewModel : BaseViewModel
{
    private readonly string mallFolder = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls");
    private readonly string mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas");
    public Cinema Cinema { get; set; } = new();
    public ObservableCollection<Mall> Malls { get; set; } = new();
    public ObservableCollection<Cinema> CinemaCollection { get; set; } = new();
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


    public ICommand SaveCommand => new Command(async () => await AddCinema());
    public ICommand ResetCommand => new Command(ResetCinema);


    /// <summary>
    /// Add or creates cinema json file
    /// </summary>
    /// <returns></returns>
    private async Task AddCinema()
    {
        bool isValidCinema = ValidateCinema();
        if (!isValidCinema)
            return;

        GenerateId();

        string json = JsonSerializer.Serialize(Cinema);
        string cinemaFile = Path.Combine(mainDir, $"{Guid.NewGuid()}.json");
        await File.WriteAllTextAsync(cinemaFile, json);

        CinemaCollection.Add(Cinema);
        await Shell.Current.DisplayAlert("Success", "Cinema added successfully", "OK");

        //Go back to cinema page
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(CinemaCollection), CinemaCollection }
        };
        await Shell.Current.GoToAsync($"..", navigationParameter);
    }

    /// <summary>
    /// Generates cinema id
    /// </summary>
    private void GenerateId()
    {
        Cinema.Id = CinemaCollection.Count + 1;
    }

    /// <summary>
    /// Resets all cinema properties to default
    /// </summary>
    private void ResetCinema()
    {
        Cinema = new Cinema();

        //update frontend that cinema is reset
        OnPropertyChanged(nameof(Cinema));
        SeatCapacity = 0;
        SelectedMallItem = null;
    }

    /// <summary>
    /// Validates user input for cinema
    /// </summary>
    /// <returns>bool</returns>
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

    /// <summary>
    ///  Get Malls from json files
    /// </summary>
    public async void GetMallsFromJson()
    {
        string[] mallFiles = Directory.GetFiles(mallFolder);
        foreach (string mallFile in mallFiles)
        {
            string json = await File.ReadAllTextAsync(mallFile);
            Mall mall = JsonSerializer.Deserialize<Mall>(json);
            Malls.Add(mall);
        }
    }

    /// <summary>
    /// Generate cinema seats after user input seat capacity
    /// </summary>
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
