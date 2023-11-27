using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public class MallService : BaseService<Mall>
{
    public MallService() : base("Malls.json") { }
    public async Task<ObservableCollection<Mall>> GetMallsAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<ObservableCollection<Mall>> GetActiveMallsAsync()
    {
        var mallCollection = await GetItemsAsync();
        return new ObservableCollection<Mall>(mallCollection.Where(m => !m.IsDeleted));
    }

    public async Task<ObservableCollection<Mall>> GetDeletedMallsAsync()
    {
        var mallCollection = await GetItemsAsync();
        return new ObservableCollection<Mall>(mallCollection.Where(m => m.IsDeleted));
    }

    public async Task<Mall> GetMallByIdAsync(int id)
    {
        var mallCollection = await GetMallsAsync();
        return mallCollection.FirstOrDefault(m => m.Id == id);
    }

    public async Task<(bool, ObservableCollection<Mall>)> AddMallAsync(Mall newMall, ObservableCollection<Mall> MallCollection)
    {
        if (newMall == null || MallCollection == null)
        {
            return (false, MallCollection);
        }

        newMall.Id = MallCollection.Count + 1;
        MallCollection.Add(newMall);
        bool isSaved = await SaveToJsonAsync(MallCollection);

        return (isSaved, MallCollection);
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
        await SaveToJsonAsync(MallCollection);
        MallCollection.Remove(mallToBeDeleted);
        await Shell.Current.DisplayAlert("Success", "Mall deleted", "OK");

        return MallCollection;
    }

    public async Task<ObservableCollection<Mall>> UpdateMallAsync(Mall SelectedMallForEdit, ObservableCollection<Mall> MallCollection)
    {
        int index = MallCollection.ToList().FindIndex(m => m.Id == SelectedMallForEdit.Id);
        if (index == -1)
        {
            await Shell.Current.DisplayAlert("Error", "Mall not found", "OK");
            return MallCollection;
        }

        MallCollection[index] = SelectedMallForEdit;
        await SaveToJsonAsync(MallCollection);
        await Shell.Current.DisplayAlert("Success", "Mall updated", "OK");
        return MallCollection;
    }

}
