using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class CinemaPageViewModel
{
    private readonly string mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas");
    public ObservableCollection<Cinema> CinemaCollection { get; set; } = new();
    public ICommand AddCinema => new Command(GoToAddCinemaPage);

    private async void GoToAddCinemaPage()
    {
        await Shell.Current.GoToAsync(nameof(AddCinema));
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

    public async Task GetCinemasFromJson()
    {
        CinemaCollection.Clear();

        await CreateCinemasFolder();
        string[] cinemaFiles = Directory.GetFiles(mainDir);
        foreach (string cinemaFile in cinemaFiles)
        {
            string json = await File.ReadAllTextAsync(cinemaFile);
            Cinema cinema = JsonSerializer.Deserialize<Cinema>(json);
            CinemaCollection.Add(cinema);
        }
    }
}
