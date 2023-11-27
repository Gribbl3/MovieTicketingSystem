using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class RegisterViewModel : BaseViewModel
{
    private readonly UserService userService;

    public User User { get; set; } = new();
    private bool _isMale;
    private bool _isFemale;
    private bool _isAdmin;
    private bool _isCustomer;
    private DateTime _birthdate;

    public DateTime BirthDate
    {
        get { return _birthdate; }
        set { _birthdate = value; OnPropertyChanged(); }
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


    public RegisterViewModel(UserService userService)
    {
        this.userService = userService;
    }

    public ICommand RegisterCommand => new Command(RegisterUser);
    public ICommand ClearCommand => new Command(Clear);


    private async void RegisterUser()
    {
        bool isValidUser = ValidateUser();
        if (!isValidUser)
        {
            await Shell.Current.DisplayAlert("Register", "Please enter all fields", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Register", "User successfully registered", "OK");

        await userService.AddUserAsync(User);
        if (User.IsAdmin)
        {
            await Shell.Current.GoToAsync($"//{nameof(Admin)}");
            return;
        }
        await Shell.Current.GoToAsync($"//{nameof(Customer)}");


    }
    private bool ValidateUser()
    {
        //check if all fields are not empty
        if (IsEmptyOrNull(User.FirstName, "First Name")) return false;
        if (IsEmptyOrNull(User.MiddleName, "Middle Name")) return false;
        if (IsEmptyOrNull(User.LastName, "Last Name")) return false;
        if (IsEmptyOrNull(User.EmailAddress, "Email Address")) return false;
        if (BirthDate == DateTime.Today) return false;
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
        User.FirstName = User.MiddleName = User.LastName = User.EmailAddress = User.HomeAddress = User.Username = User.Password = string.Empty;
        BirthDate = DateTime.Now;
        IsMale = IsFemale = IsAdmin = IsCustomer = false;
    }
}
