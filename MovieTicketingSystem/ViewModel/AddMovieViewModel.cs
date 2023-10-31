using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class AddMovieViewModel : BaseViewModel
{
    public Movie Movie { get; set; } = new Movie();
    public ObservableCollection<Genre> AvailableGenre { get; set; }
    public ObservableCollection<Subtitle> AvailableSubtitle { get; set; }

    private MovieService _movieService;
    private ObservableCollection<Movie> _movieCollection;
    private const string _defaultImage = "add_photo.png";
    private string _image;
    public AddMovieViewModel(MovieService movieService)
    {
        _movieService = movieService;
        Image = _defaultImage;
        PopulateGenres();
        PopulateSubtitles();
    }

    public ICommand UploadImageCommand => new Command(async () => await UploadImage());
    public ICommand SaveCommand => new Command(async () => await Save());
    public ICommand ResetCommand => new Command(async () => await Reset());

    private async Task Reset()
    {
        //reset the movie object
        Movie = new Movie();
        //reset the image
        Image = "add_photo.png";
    }

    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }

    public string Image
    {
        get => _image;
        set
        {
            _image = value;
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

        //call the method to convert the fileResult to byte array
        var imageByteArray = await ConvertFileResultToByteArray(fileResult);

        //set the image property of the movie to the imageByteArray
        Image = fileResult.FullPath.ToString();
        Movie.Image = imageByteArray;
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

        if (!GetSelectedGenre())
        {
            return;
        }

        if (!GetSelectedSubtitle())
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
        if (!ValidateEntries("Movie Title", Movie.Name) || !ValidateEntries(nameof(Movie.Description), Movie.Description))
        {
            return false;
        }

        //check if price is greater than 0
        if (Movie.Price <= 0)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Price must be greater than 0", "OK");
            return false;
        }

        //check if start time is greater than end time
        if (Movie.StartDate > Movie.EndDate)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Start time cannot be greater than end time", "OK");
            return false;
        }

        //check if radio buttons are checked
        if (!Movie.IsNowShowing && !Movie.IsUpcoming)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Please select a movie type", "OK");
            return false;
        }

        //check if image is uploaded
        if (Image == _defaultImage)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Please upload an image", "OK");
            return false;
        }

        //check if start time is greater than end time
        if (Movie.StartTime > Movie.EndTime)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Start time cannot be greater than end time", "OK");
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

    private void PopulateGenres()
    {
        AvailableGenre = new()
        {
            new Genre{Name = "Action"},
            new Genre{Name = "Adventure"},
            new Genre{Name = "Comedy"},
            new Genre{Name = "Drama"},
            new Genre{Name = "Fantasy"},
            new Genre{Name = "Horror"},
            new Genre{Name = "Mystery"},
            new Genre{Name = "Thriller"},
            new Genre{Name = "Western"}
        };
    }

    private void PopulateSubtitles()
    {
        AvailableSubtitle = new()
        {
            new Subtitle{Language = "English"},
            new Subtitle{Language = "Filipino"},
            new Subtitle{Language = "Japanese"},
            new Subtitle{Language = "Korean"},
            new Subtitle{Language = "Mandarin"},
            new Subtitle{Language = "Spanish"},
            new Subtitle{Language = "Thai"},
            new Subtitle{Language = "Vietnamese"}
        };
    }

    private bool GetSelectedGenre()
    {
        //Clear previous selectedGenre
        Movie.SelectedGenre.Clear();

        //loop through AvailableGenre and checked what item has a true value of IsSelected
        //add the item to SelectedGenre
        bool hasSelectedGenre = false;
        for (int index = 0; index < AvailableGenre.Count; index++)
        {
            if (AvailableGenre[index].IsSelected)
            {
                Movie.SelectedGenre.Add(AvailableGenre[index].Name);
                hasSelectedGenre = true;
            }
        }

        if (!hasSelectedGenre)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Please select a genre", "OK");
        }

        return hasSelectedGenre;
    }

    private bool GetSelectedSubtitle()
    {
        //Clear previous selectedSubtitle
        Movie.SelectedSubtitle.Clear();

        bool hasSelectedGenre = false;
        for (int index = 0; index < AvailableSubtitle.Count; index++)
        {
            if (AvailableSubtitle[index].IsSelected)
            {
                Movie.SelectedSubtitle.Add(AvailableSubtitle[index].Language);
                hasSelectedGenre = true;
            }
        }

        if (!hasSelectedGenre)
        {
            Shell.Current.DisplayAlert("Add Movie Error", "Please select a subtitle", "OK");
        }
        return hasSelectedGenre;
    }
}
