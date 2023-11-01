namespace MovieTicketingSystem.Model;

public class Mall : BaseModel //Mall - SM North Edsa, MallAddress - Quezon City
{
    public int Id { get; set; }

    private string _name;
    private string _address;

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
}

