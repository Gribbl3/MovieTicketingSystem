using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class UserService
{
    private readonly string adminFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "MovieAdmin.json");
    private readonly string customerFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "MovieCustomer.json");
    public async Task<bool> AddUserAsync(User user)
    {
        if (user == null)
        {
            return false;
        }

        string filePath = user.IsAdmin ? adminFilePath : customerFilePath;
        ObservableCollection<User> users = await GetUsersAsync(user.IsAdmin);
        users.Add(user);

        var json = JsonSerializer.Serialize<ObservableCollection<User>>(users);
        await File.WriteAllTextAsync(filePath, json);
        return true;
    }

    public async Task<ObservableCollection<User>> GetUsersAsync(bool isAdmin)
    {
        string filePath = isAdmin ? adminFilePath : customerFilePath;

        if (!File.Exists(filePath))
        {
            return new ObservableCollection<User>();
        }

        var json = await File.ReadAllTextAsync(filePath);
        var userCollection = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        return userCollection;
    }

    public async Task<User> GetUserByIdAsync(int id, bool isAdmin)
    {
        string filePath = isAdmin ? adminFilePath : customerFilePath;
        var users = await GetUsersAsync(isAdmin);
        return users.FirstOrDefault(user => user.Id == id);
    }
}
