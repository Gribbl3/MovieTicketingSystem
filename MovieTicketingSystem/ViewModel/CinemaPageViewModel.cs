using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
public class CinemaPageViewModel : BaseViewModel
{
    private readonly string cinemaFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");
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

    public CinemaPageViewModel()
    {
        GetCinemasFromJson();
    }

    public ICommand AddCinemaCommand => new Command(GoToAddCinemaPage);
    public ICommand DeleteCinemaCommand => new Command(DeleteCinema);
    public ICommand EditCinemaCommand => new Command(GoToEditCinemaPage);

    /// <summary>
    /// Goes to AddCinemaPage with CinemaCollection as navigation parameter
    /// </summary>
    private async void GoToAddCinemaPage()
    {
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

        foreach (Cinema cinema in CinemaCollection)
        {
            if (cinema.Id == id)
            {
                CinemaCollection.Remove(cinema);
                string cinemaJson = JsonSerializer.Serialize(CinemaCollection);
                await File.WriteAllTextAsync(cinemaFilePath, cinemaJson);
                await Shell.Current.DisplayAlert("Success", "Cinema deleted successfully", "OK");
                return;
            }
        }

        await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");
    }

    private async void GoToEditCinemaPage()
    {
        string result = await Shell.Current.DisplayPromptAsync("Edit Cinema", "Enter cinema id to edit");
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
            return;

        foreach (Cinema cinema in CinemaCollection)
        {
            if (cinema.Id == id)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { nameof(Cinema), cinema },
                    { nameof(CinemaCollection), CinemaCollection}
                };
                await Shell.Current.GoToAsync($"{nameof(EditCinema)}", navigationParameter);
                return;
            }
        }
    }


    private async void GetCinemasFromJson()
    {
        if (!File.Exists(cinemaFilePath))
        {
            CinemaCollection = new ObservableCollection<Cinema>();
            return;
        }

        string json = await File.ReadAllTextAsync(cinemaFilePath);
        CinemaCollection = JsonSerializer.Deserialize<ObservableCollection<Cinema>>(json);
    }
}
