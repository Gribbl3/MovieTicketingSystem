

namespace MovieTicketingSystem.Model;

public class Movie : BaseModel
{
    #region Fields and Properties
    //generate field and properties
    private string _name;
    private string _description;
    private bool _isUpcoming;
    private bool _isNowShowing;
    private ImageSource _image;
    private DateTime _duration;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public bool IsUpcoming
    {
        get => _isUpcoming;
        set
        {
            _isUpcoming = value;
            OnPropertyChanged();
        }
    }

    public bool IsNowShowing
    {
        get => _isNowShowing;
        set
        {
            _isNowShowing = value;
            OnPropertyChanged();
        }
    }

    public DateTime Duration
    {
        get => _duration;
        set
        {
            _duration = value;
            OnPropertyChanged();
        }
    }

    public ImageSource Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged();
        }
    }
    #endregion
}
