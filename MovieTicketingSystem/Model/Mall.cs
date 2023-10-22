namespace MovieTicketingSystem.Model;

public class Mall : BaseModel //Mall - SM North Edsa, MallAddress - Quezon City
{
    private string _mallName;
    private string _mallAddress;

    public string MallName
    {
        get => _mallName;
        set
        {
            _mallName = value;
            OnPropertyChanged();
        }
    }
    public string MallAddress
    {
        get => _mallAddress;
        set
        {
            _mallAddress = value;
            OnPropertyChanged();
        }
    }
}

