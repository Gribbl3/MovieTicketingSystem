using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.ViewModel;

public class AdminViewModel : BaseViewModel
{
    private readonly UserService userService;


    private ObservableCollection<User> _customerCollection = new();

    public ObservableCollection<User> CustomerCollection
    {
        get => _customerCollection;
        set
        {
            _customerCollection = value;
            OnPropertyChanged();
        }
    }


    public AdminViewModel(UserService userService)
    {
        this.userService = userService;
        ShowAllUser();
    }

    private async void ShowAllUser()
    {
        CustomerCollection = await userService.GetUsersAsync(false);
    }
}
