namespace MovieTicketingSystem.Model;


public class User : BaseModel
{
    //create private fields for id, firstname, middlename, lastname,email address, birthdate,home address,gender, username, password
    //create public properties for id, firstname, middlename, lastname,email address, birthdate,home address, username, password


    //Parent class for Customer and Admin
    private int _id;
    private string _firstName;
    private string _middleName;
    private string _lastName;
    private string _emailAddress;
    private string _homeAddress;
    private string _gender;
    private bool _isAdmin, _isDeleted;

    private string _username;
    private string _password;

    private DateTime _birthDate;

    public int Id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(); }
    }
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; OnPropertyChanged(); }
    }
    public string MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; OnPropertyChanged(); }
    }
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; OnPropertyChanged(); }
    }
    public string EmailAddress
    {
        get { return _emailAddress; }
        set { _emailAddress = value; OnPropertyChanged(); }
    }
    public DateTime BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; OnPropertyChanged(); }
    }
    public string HomeAddress
    {
        get { return _homeAddress; }
        set { _homeAddress = value; OnPropertyChanged(); }
    }
    public string Gender
    {
        get { return _gender; }
        set { _gender = value; OnPropertyChanged(); }
    }
    public string Username
    {
        get { return _username; }
        set { _username = value; OnPropertyChanged(); }
    }
    public string Password
    {
        get { return _password; }
        set { _password = value; OnPropertyChanged(); }
    }

    public bool IsAdmin
    {
        get { return _isAdmin; }
        set { _isAdmin = value; OnPropertyChanged(); }
    }

    public bool IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; OnPropertyChanged(); }
    }

}
