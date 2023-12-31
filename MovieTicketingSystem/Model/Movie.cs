﻿using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Model;

public class Movie : BaseModel
{
    public int Id { get; set; }

    #region Fields and Properties
    //generate field and properties
    private string _name, _description, _companySource;
    private bool _isUpcoming, _isNowShowing, _isDeleted;
    private decimal _price;
    private Byte[] _image;
    //showtime start and end date
    private DateTime _yearReleased, _startDate, _endDate;
    private TimeSpan _startTime, _endTime;
    public ObservableCollection<string> SelectedGenre { get; set; } = new();
    public ObservableCollection<string> SelectedSubtitle { get; set; } = new();
    private ObservableCollection<Cinema> _cinemas = new();

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

    public TimeSpan StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;
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

    public TimeSpan Duration
    {
        get => EndTime - StartTime;
    }

    public ObservableCollection<Cinema> Cinemas
    {
        get => _cinemas;
        set
        {
            _cinemas = value;
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
    #endregion
}
