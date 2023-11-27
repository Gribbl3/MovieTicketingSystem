using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public abstract class BaseService<T>
{
    private readonly string filePath;

    public BaseService(string filePath)
    {
        this.filePath = Path.Combine(FileSystem.Current.AppDataDirectory, filePath);
    }

    public async Task<bool> SaveToJsonAsync(ObservableCollection<T> itemCollection)
    {
        if (itemCollection == null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize<ObservableCollection<T>>(itemCollection);
        await File.WriteAllTextAsync(filePath, json);

        return true;
    }

    public async Task<ObservableCollection<T>> GetItemsAsync()
    {
        if (!File.Exists(filePath))
        {
            return new ObservableCollection<T>();
        }

        var json = await File.ReadAllTextAsync(filePath);
        var itemCollection = JsonSerializer.Deserialize<ObservableCollection<T>>(json);
        return itemCollection;
    }

}
