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
    private async void Register()
    {
        await Shell.Current.GoToAsync("Register");
    }

    private async void SignIn(string userType)
    {
        User.IsAdmin = userType == "Admin";


        switch (userType)
        {
            case "Admin":
                userCollection = await _userService.GetUsers(User.IsAdmin);
                break;
            case "Customer":
                userCollection = await _userService.GetUsers(User.IsAdmin);
                break;
        }

        bool isValidUser = ValidateUser();
        if (isValidUser)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(User), User }
            };

            if (User.IsAdmin)
            {
                await Shell.Current.GoToAsync($"//{nameof(Admin)}?name={_firstName}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(Customer)}", navigationParameter);
            }
            User = new User();
            OnPropertyChanged(nameof(User));
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
