using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.ViewModel;

public class AdminViewModel : BaseViewModel
{
    private readonly UserService userService;


    private ObservableCollection<User> _customerCollection = new();
    private bool _isVisible = false;
    private bool _isVisibleCollection = false;

    public ObservableCollection<User> CustomerCollection
    {
        get => _customerCollection;
        set
        {
            _customerCollection = value;
            OnPropertyChanged();
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged();
        }
    }

    public bool IsVisibleCollection
    {
        get => _isVisibleCollection;
        set
        {
            _isVisibleCollection = value;
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
        IsVisible = CustomerCollection.Count <= 0;
        IsVisibleCollection = !IsVisible;
    }
}
