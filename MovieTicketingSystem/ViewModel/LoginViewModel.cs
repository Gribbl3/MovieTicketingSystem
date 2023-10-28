using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class LoginViewModel : BaseViewModel
{
    private readonly IUserService _userService;
    private ObservableCollection<User> userCollection;
    private string _firstName;
    public User User { get; set; } = new();
    public ICommand SignInCommand => new Command<string>(SignIn);
    public ICommand RegisterCommand => new Command(Register);

    public LoginViewModel(IUserService userService)
    {
        _userService = userService;
    }
    private void Register()
    {
        Shell.Current.GoToAsync("Register");
    }

    private void SignIn(string userType)
    {
        User.IsAdmin = userType == "Admin";
       

        switch (userType)
        {
            case "Admin":
                userCollection = _userService.GetUsers(User.IsAdmin).Result;
                break;
            case "Customer":
                userCollection = _userService.GetUsers(User.IsAdmin).Result;
                break;
        }

        bool isValidUser = ValidateUser();
        if (isValidUser)
        {
            if (User.IsAdmin)
            {
                Shell.Current.GoToAsync($"//{nameof(Admin)}?name={_firstName}");
            }
            else
            {
                Shell.Current.GoToAsync($"??//{nameof(Customer)}?name={_firstName}");
            }
        }
    }

    private bool ValidateUser()
    {
        if (User.Username == string.Empty || User.Password == string.Empty)
        {
            Shell.Current.DisplayAlert("Sign In", "Please enter username and password", "OK");
            return false;
        }

        foreach (var user in userCollection)
        {
            if (user.Username == User.Username && user.Password == User.Password)
            {
                _firstName = user.FirstName;
                return true;
            }
        }

        //if the user is not found, display an alert
        Shell.Current.DisplayAlert("Sign In", "Invalid username or password", "OK");
        return false;
    }
}
