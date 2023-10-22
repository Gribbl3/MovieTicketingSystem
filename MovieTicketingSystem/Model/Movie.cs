namespace MovieTicketingSystem.Model;

public class Movie : BaseModel
{
    #region Fields and Properties
    //generate field and properties
    private string _name;
    private string _description;
    private decimal _price;
    public List<string> SelectedGenres { get; set; }
    public List<string> SelectedSubtitles { get; set; }
    private string _companySource;
    private bool _isUpcoming;
    private bool _isNowShowing;
    private Byte[] _image;
    //showtime start and end date
    private DateTime _duration;
    private DateTime _yearReleased;
    private DateTime _startDate;
    private DateTime _endDate;
    //movie duration

    public Movie()
    {
        _startDate = _endDate = DateTime.Now;
    }

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

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
        }
    }
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
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

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
        }
    }

    public string CompanySource
    {
        get => _companySource;
        set
        {
            _companySource = value;
            OnPropertyChanged();
        }
    }

    public DateTime YearReleased
    {
        get => _yearReleased;
        set
        {
            _yearReleased = value;
            OnPropertyChanged();
        }
    }

    #endregion
}
