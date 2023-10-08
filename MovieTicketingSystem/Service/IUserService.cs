using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public interface IUserService
{
    public Task<bool> AddUser(User user);
    public Task<ObservableCollection<User>> GetUsers(bool isAdmin);
}
