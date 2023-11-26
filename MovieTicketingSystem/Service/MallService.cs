﻿using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class MallService : BaseModel
{
    private readonly string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");



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
}
