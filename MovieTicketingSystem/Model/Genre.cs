namespace MovieTicketingSystem.Model;

public class Genre : BaseModel
{
    private string _name;
    private bool _isSelected;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }
}
