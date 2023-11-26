using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
public class AddCinemaViewModel : BaseViewModel
{
    private readonly string mallFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");
    private readonly string cinemaFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");
    public Cinema Cinema { get; set; } = new();
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
        GenerateName();
        CinemaCollection.Add(Cinema);
        string json = JsonSerializer.Serialize<ObservableCollection<Cinema>>(CinemaCollection);
        await File.WriteAllTextAsync(cinemaFilePath, json);

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
        int newId = 1;
        bool isFound = false;

        //or i can also store all the ids in a list and get the max id and add 1 to it


        while (!isFound)
        {
            bool isMatchFound = false;
            foreach (Cinema cinema in CinemaCollection)
            {
                int id = cinema.Id;
                if (id == newId)
                {
                    isMatchFound = true;
                    newId++;
                    break;
                }
            }

            if (!isMatchFound)
            {
                isFound = true;
            }

        }

        Cinema.Id = newId;

    }

    /// <summary>
    /// Generates cinema name based on mall name and cinema count on that mall
    /// </summary>
    private void GenerateName()
    {
        const string NAME = "CINEMA";

        //check the current cinema collection number of cinemas in the mall
        //int cinemaCount = CinemaCollection.Where(c => c.Mall.Id == SelectedMallItem.Id).Count();
        //Cinema.Name = $"{SelectedMallItem.Name}- {NAME} - {cinemaCount + 1}";
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
        Cinema.MallId = SelectedMallItem.Id;
        return true;
    }

    /// <summary>
    ///  Get Malls from json files
    /// </summary>
    public async void GetMallsFromJson()
    {
        if (!File.Exists(mallFilePath))
        {
            //if file does not exist, display error message and let user go back to previous page
            await Shell.Current.DisplayAlert("Error", "Mall file not found, Add a Mall to proceed", "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }
        string json = await File.ReadAllTextAsync(mallFilePath);
        MallCollection = JsonSerializer.Deserialize<ObservableCollection<Mall>>(json);
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
