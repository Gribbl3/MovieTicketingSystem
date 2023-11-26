namespace MovieTicketingSystem.Model;

public class Mall : BaseModel //Mall - SM North Edsa, MallAddress - Quezon City
{
    public int Id { get; set; }

    private string _name;
    private string _address;
    private bool _isDeleted; //IsDeleted - hide or show

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }
    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged();
        }
    }
    public bool IsDeleted
    {
        get => _isDeleted;
        set
        {
            _isDeleted = value;
            OnPropertyChanged();
        }
    }
}

