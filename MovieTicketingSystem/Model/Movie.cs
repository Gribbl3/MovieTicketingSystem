

namespace MovieTicketingSystem.Model;

public class Movie : BaseModel
{
    #region Fields and Properties
    //generate field and properties
    private string _name;
    private string _description;
    private bool _isUpcoming;
    private bool _isNowShowing;
    private Byte[] _image;
    private DateTime _date;
    private DateTime _startTime;
    private DateTime _endTime;

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

    public DateTime Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    //image is byte array because there is no imagesource datatype in json
    public Byte[] Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged();
        }
    }

    public DateTime StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;
            OnPropertyChanged();
        }
    }

    public DateTime EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;
            OnPropertyChanged();
        }
    }
    #endregion
}
