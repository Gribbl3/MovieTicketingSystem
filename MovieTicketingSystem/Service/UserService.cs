using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieTicketingSystem.Service;

public class UserService : IUserService
{
    private readonly string mainDir = FileSystem.Current.AppDataDirectory;
    private readonly string adminFileName = "MovieAdmin.json";
    private readonly string customerFileName = "MovieCustomer.json";
    public Task<bool> AddUser(User user)
    {
        if (user == null) return Task.FromResult(false);

        //determines the filepath based on the user type
        string filePath = Path.Combine(mainDir, user.IsAdmin ? adminFileName : customerFileName);

        //if the file exists, read the json and deserialize it to a collection of users
        ObservableCollection<User> users = GetUsers(user.IsAdmin).Result;
        users.Add(user);

        var json = JsonSerializer.Serialize<ObservableCollection<User>>(users);
        File.WriteAllText(filePath, json);
        return Task.FromResult(true);
    }

    public Task<ObservableCollection<User>> GetUsers(bool isAdmin)
    {
        //determines the filepath based on the user type
        string filePath = Path.Combine(mainDir, isAdmin ? adminFileName : customerFileName);

        //if the file doesn't exist, return an empty collection
        if (!File.Exists(filePath)) return Task.FromResult(new ObservableCollection<User>());

        //if the file exists, read the json and deserialize it to a collection of users
        var json = File.ReadAllText(filePath);
        var userCollection = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
        return Task.FromResult(userCollection);
    }
}
