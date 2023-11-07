using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Service;

public class JsonFileService
{
    public async Task AddToJsonFile<T>(ObservableCollection<T> collection, string filepath)
    {
        string json = JsonSerializer.Serialize(collection);
        await File.WriteAllTextAsync(filepath, json);
    }

    public async Task<ObservableCollection<T>> GetFromJsonFile<T>(string filepath)
    {
        if (!File.Exists(filepath))
        {
            return new ObservableCollection<T>();
        }

        string json = await File.ReadAllTextAsync(filepath);
        return JsonSerializer.Deserialize<ObservableCollection<T>>(json);
    }
}
