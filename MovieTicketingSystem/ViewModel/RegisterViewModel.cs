using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class RegisterViewModel : BaseViewModel
{
    public User User { get; set; } = new();

    public ICommand RegisterCommand => new Command(RegisterUser);
    public ICommand ClearCommand => new Command(Clear);

    private readonly IUserService _userService;
    private bool _isMale;
    private bool _isFemale;
    private bool _isAdmin;
    private bool _isCustomer;
    private DateTime _birthdate;

    public DateTime BirthDate
    {
        get { return _birthdate; }
        set { _birthdate = value; OnPropertyChanged(); User.BirthDate = value.ToString("MM/dd/yyyy"); }
    }

    public bool IsMale
    {
        get { return _isMale; }
        set { _isMale = value; OnPropertyChanged(); User.Gender = "Male"; }
    }

    public bool IsFemale
    {
        get { return _isFemale; }
        set { _isFemale = value; OnPropertyChanged(); User.Gender = "Female"; }
    }

    public bool IsAdmin
    {
        get { return _isAdmin; }
        set { _isAdmin = value; OnPropertyChanged(); User.IsAdmin = true; }
    }

    public bool IsCustomer
    {
        get { return _isCustomer; }
        set { _isCustomer = value; OnPropertyChanged(); User.IsAdmin = false; }
    }


    public RegisterViewModel(IUserService userService)
    {
        _userService = userService;
    }

    private void RegisterUser()
    {
        bool isValidUser = ValidateUser();
        if (!isValidUser)
        {
            Shell.Current.DisplayAlert("Register", "Please enter all fields", "OK");
            return;
        }

        Shell.Current.DisplayAlert("Register", "User successfully registered", "OK");

        //adding user to local appdata
        _userService.AddUser(User);
        if (User.IsAdmin)
        {
            Shell.Current.GoToAsync($"//{nameof(Admin)}");
        }
        else
        {
            Shell.Current.GoToAsync($"//{nameof(Customer)}");
        }


    }
    private bool ValidateUser()
    {
        //check if all fields are not empty
        if (IsEmptyOrNull(User.FirstName, "First Name")) return false;
        if (IsEmptyOrNull(User.MiddleName, "Middle Name")) return false;
        if (IsEmptyOrNull(User.LastName, "Last Name")) return false;
        if (IsEmptyOrNull(User.EmailAddress, "Email Address")) return false;
        if (IsEmptyOrNull(User.BirthDate, "Birth Date")) return false;
        if (IsEmptyOrNull(User.HomeAddress, "Home Address")) return false;
        if (IsEmptyOrNull(User.Password, "Username")) return false;
        if (IsEmptyOrNull(User.Password, "Password")) return false;

        if (IsInvalidEmail(User.EmailAddress)) return false;
        if (IsInvalidBirthdate(BirthDate)) return false;

        return true;
    }

    private void Clear()
    {
        //set all fields to empty
        User.FirstName = User.MiddleName = User.LastName = User.EmailAddress = User.BirthDate = User.HomeAddress = User.Username = User.Password = string.Empty;
        BirthDate = DateTime.Now;
        IsMale = IsFemale = IsAdmin = IsCustomer = false;
    }
}
