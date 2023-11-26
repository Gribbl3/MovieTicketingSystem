using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class CinemaService
{
    private readonly string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Cinemas.json");

    public async Task<bool> AddUpdateCinemaAsync(ObservableCollection<Cinema> CinemaCollection)
    {
        if (CinemaCollection == null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize<ObservableCollection<Cinema>>(CinemaCollection);
        await File.WriteAllTextAsync(filePath, json);

        return true;
    }

    public async Task<ObservableCollection<Cinema>> GetCinemasAsync()
    {
        //check if file pathexists
        if (!File.Exists(filePath))
        {
            return new ObservableCollection<Cinema>();
        }

        //get json file from file path then deserialize
        var json = await File.ReadAllTextAsync(filePath);
        var cinemaCollection = JsonSerializer.Deserialize<ObservableCollection<Cinema>>(json);
        return cinemaCollection;
    }

}
