using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class LoginViewModel : BaseViewModel
{
    private readonly UserService userService;

    private ObservableCollection<User> userCollection;
    private User _user = new();

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }


    public LoginViewModel(UserService userService)
    {
        this.userService = userService;
    }


    private async void Register()
    {
        await Shell.Current.GoToAsync("Register");
    }

    public ICommand SignInCommand => new Command<string>(SignIn);
    public ICommand RegisterCommand => new Command(Register);


    private async void SignIn(string userType)
    {
        User.IsAdmin = userType == "Admin";


        switch (userType)
        {
            case "Admin":
                userCollection = await userService.GetUsersAsync(User.IsAdmin);
                break;
            case "Customer":
                userCollection = await userService.GetUsersAsync(User.IsAdmin);
                break;
        }

        bool isValidUser = ValidateUser();
        if (!isValidUser)
        {
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(User), User }
        };

        if (User.IsAdmin)
        {
            await Shell.Current.GoToAsync($"//{nameof(Admin)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(Customer)}", navigationParameter);
        }

        //Reset fields
        User = new User();
    }

    private bool ValidateUser()
    {
        if (string.IsNullOrEmpty(User.Username) || string.IsNullOrEmpty(User.Password))
        {
            Shell.Current.DisplayAlert("Sign In", "Please enter username and password", "OK");
            return false;
        }

        return userCollection.FirstOrDefault(u => u.Username == User.Username && u.Password == User.Password) != null;
    }
}
