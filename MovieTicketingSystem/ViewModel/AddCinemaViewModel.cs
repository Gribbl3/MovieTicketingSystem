using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class AddCinemaViewModel : BaseViewModel
{
    private readonly string _mallFolder = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls");
    private readonly string mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas");
    public Cinema Cinema { get; set; } = new();
    public ObservableCollection<Mall> Malls { get; set; } = new();
    private Mall _selectedMallItem;
    private int _seatCapacity;


    public AddCinemaViewModel()
    {
        GetMallsFromJson();
    }

    public ICommand SaveCommand => new Command(async () => await AddCinema());

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


    private async Task AddCinema()
    {
        bool isValidCinema = ValidateCinema();
        if (!isValidCinema)
            return;

        await CreateCinemasFolder();

        string json = JsonSerializer.Serialize(Cinema);
        string cinemaFile = Path.Combine(mainDir, $"{Guid.NewGuid()}.json");
        await File.WriteAllTextAsync(cinemaFile, json);

        await Shell.Current.DisplayAlert("Success", "Cinema added successfully", "OK");
    }

    public ICommand ResetCommand => new Command(ResetCinema);

    private void ResetCinema(object obj)
    {
        throw new NotImplementedException();
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
    private async Task CreateCinemasFolder()
    {
        try
        {
            if (!Directory.Exists(mainDir))
            {
                Directory.CreateDirectory(mainDir);
                return;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
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
        return true;
    }
    private async void GetMallsFromJson()
    {
        string[] mallFiles = Directory.GetFiles(_mallFolder);
        foreach (string mallFile in mallFiles)
        {
            string json = await File.ReadAllTextAsync(mallFile);
            Mall mall = JsonSerializer.Deserialize<Mall>(json);
            Malls.Add(mall);
        }
    }

    private void GenerateCinemaSeats()
    {
        Cinema.Seats.Clear();

        const int columns = 8;
        int rows = (int)Math.Ceiling((double)Cinema.SeatCapacity / columns);
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (row * columns + column < Cinema.SeatCapacity)
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
