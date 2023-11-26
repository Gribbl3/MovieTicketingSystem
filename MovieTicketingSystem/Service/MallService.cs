using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class MallService : BaseModel
{
    private readonly string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");

    public async Task<ObservableCollection<Mall>> GetMallsAsync()
    {
        //check if file pathexists
        if (!File.Exists(filePath))
        {
            return new ObservableCollection<Mall>();
        }

        //get json file from file path then deserialize
        var json = await File.ReadAllTextAsync(filePath);
        var mallCollection = JsonSerializer.Deserialize<ObservableCollection<Mall>>(json);
        return mallCollection;
    }
    public async Task<ObservableCollection<Mall>> GetActiveMallsAsync()
    {
        var mallCollection = await GetMallsAsync();
        //filter
        mallCollection = new ObservableCollection<Mall>(mallCollection.Where(m => !m.IsDeleted));
        return mallCollection;
    }

    public async Task<ObservableCollection<Mall>> GetDeletedMallsAsync()
    {
        var mallCollection = await GetMallsAsync();

        //filter
        mallCollection = new ObservableCollection<Mall>(mallCollection.Where(m => m.IsDeleted));
        return mallCollection;
    }
    public async Task<bool> AddUpdateMallAsync(ObservableCollection<Mall> MallCollection)
    {
        if (MallCollection == null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize<ObservableCollection<Mall>>(MallCollection);
        await File.WriteAllTextAsync(filePath, json);

        return true;
    }

    public async Task<ObservableCollection<Mall>> DeleteMallAsync(int id, ObservableCollection<Mall> MallCollection)
    {
        var mallToBeDeleted = MallCollection.FirstOrDefault(m => m.Id == id);
        if (mallToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Mall not found", "OK");
            return MallCollection;
        }

        if (mallToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Mall already deleted", "OK");
            return MallCollection;
        }

        mallToBeDeleted.IsDeleted = true;
        await AddUpdateMallAsync(MallCollection);
        MallCollection.Remove(mallToBeDeleted);
        await Shell.Current.DisplayAlert("Success", "Mall deleted", "OK");

        return MallCollection;
    }

    public async Task<ObservableCollection<Mall>> UpdateMallAsync(Mall SelectedMallForEdit, ObservableCollection<Mall> MallCollection)
    {
        var index = MallCollection.IndexOf((Mall)MallCollection.Select(m => m.Id == SelectedMallForEdit.Id));

        return MallCollection;
    }



}
