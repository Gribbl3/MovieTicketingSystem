using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
public class CinemaPageViewModel : BaseViewModel
{
    private readonly string cinemaDir = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas");
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

    public ICommand AddCinemaCommand => new Command(GoToAddCinemaPage);
    public ICommand DeleteCinemaCommand => new Command(DeleteCinema);
    public ICommand EditCinemaCommand => new Command(GoToEditCinemaPage);

    /// <summary>
    /// Goes to AddCinemaPage with CinemaCollection as navigation parameter
    /// </summary>
    private async void GoToAddCinemaPage()
    {
        string cinemaCollectionJson = JsonSerializer.Serialize(CinemaCollection);
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(CinemaCollection), CinemaCollection }
        };
        await Shell.Current.GoToAsync($"{nameof(AddCinema)}", navigationParameter);
    }

    /// <summary>
    /// deletes cinema json file and removes it from CinemaCollection
    /// </summary>
    private async void DeleteCinema()
    {
        string result = await Shell.Current.DisplayPromptAsync("Delete Cinema", "Enter cinema id to delete");
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
            return;

        string[] cinemaFiles = Directory.GetFiles(cinemaDir);

        for (int index = 0; index < cinemaFiles.Length; index++)
        {
            string json = await File.ReadAllTextAsync(cinemaFiles[index]);
            Cinema Cinema = JsonSerializer.Deserialize<Cinema>(json);
            if (id == Cinema.Id)
            {
                File.Delete(cinemaFiles[index]);
                CinemaCollection.Remove(CinemaCollection.FirstOrDefault(cinema => cinema.Id == id));
                return;
            }
        }

        await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");
    }

    private async void GoToEditCinemaPage()
    {
        await Shell.Current.DisplayAlert("Edit Cinema", "Edit cinema feature is not yet available", "OK");
    }

    private async Task CreateCinemasFolder()
    {
        try
        {
            if (!Directory.Exists(cinemaDir))
            {
                Directory.CreateDirectory(cinemaDir);
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
        string[] cinemaFiles = Directory.GetFiles(cinemaDir);
        foreach (string cinemaFile in cinemaFiles)
        {
            string json = await File.ReadAllTextAsync(cinemaFile);
            Cinema cinema = JsonSerializer.Deserialize<Cinema>(json);
            CinemaCollection.Add(cinema);
        }
    }
}
