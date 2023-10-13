using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class AddMovieViewModel : BaseViewModel
{
    public Movie Movie { get; set; } = new Movie();
    private MovieService _movieService;
    private ObservableCollection<Movie> _movieCollection;
    private ImageSource _movieImageSource;
    private DateTime _date;
    private DateTime _startTime;
    private DateTime _endTime;

    public AddMovieViewModel(MovieService movieService)
    {
        _movieService = movieService;
        Date = DateTime.Today;
    }

    public ICommand UploadImageCommand => new Command(async () => await UploadImage());
    public ICommand SaveCommand => new Command(async () => await Save());

    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }

    public ImageSource MovieImageSource
    {
        get => _movieImageSource;
        set
        {
            _movieImageSource = value;
            OnPropertyChanged();
        }
    }

    public DateTime Date
    {
        get => _date;
        set
        {
            _date = value;
            Movie.Date = value;
            OnPropertyChanged();
        }
    }

    public DateTime StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;
            Movie.StartTime = value;
            OnPropertyChanged();
        }
    }

    public DateTime EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;
            Movie.EndTime = value;
            OnPropertyChanged();
        }
    }

    private async Task UploadImage()
    {
        var fileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the movie",
            FileTypes = FilePickerFileType.Images
        });

        if (fileResult == null)
        {
            return;
        }

        var stream = await fileResult.OpenReadAsync();
        //call the method to convert the fileResult to byte array
        var imageByteArray = await ConvertFileResultToByteArray(fileResult);

        Movie.Image = imageByteArray;
        MovieImageSource = ImageSource.FromStream(() => stream);
    }


    private async Task<byte[]> ConvertFileResultToByteArray(FileResult fileResult)
    {
        using var stream = await fileResult.OpenReadAsync();
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    private async Task Save()
    {
        if (!ValidateMovie())
        {
            return;
        }

        var result = await _movieService.AddMovieAsync(Movie);
        if (result)
        {
            await Shell.Current.DisplayAlert("Add Movie", "Movie added successfully", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Add Movie Error", "Something went wrong", "OK");
    }

    //validators
    private bool ValidateMovie()
    {
        //check if entries are empty or null
        if (!ValidateEntries(nameof(Movie.Name), Movie.Name) || !ValidateEntries(nameof(Movie.Description), Movie.Description))
        {
            return false;
        }

        //check if start time is greater than end time
        if (Movie.StartTime > Movie.EndTime)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Start time cannot be greater than end time", "OK");
            return false;
        }

        //check if date is greater than today
        if (Movie.Date < DateTime.Today)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Date cannot be less than today", "OK");
            return false;
        }

        //check if radio buttons are checked
        if (!Movie.IsNowShowing && !Movie.IsUpcoming)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Please select a movie type", "OK");
            return false;
        }

        return true;
    }

    private bool ValidateEntries(string fieldName, string fieldValue)
    {
        if (string.IsNullOrWhiteSpace(fieldValue))
        {
            Shell.Current.DisplayAlert("Add Movie Error", $"{fieldName} is required", "OK");
            return false;
        }

        return true;
    }

}
