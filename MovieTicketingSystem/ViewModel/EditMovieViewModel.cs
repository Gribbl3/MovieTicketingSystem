using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(MovieCollection), nameof(MovieCollection))]
[QueryProperty(nameof(Movie), nameof(Movie))]
public class EditMovieViewModel : BaseViewModel
{
    private readonly string movieFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Movies.json");
    private readonly string defaultImage = "defaultImage.png";

    public ObservableCollection<Genre> AvailableGenre { get; set; }
    public ObservableCollection<Subtitle> AvailableSubtitle { get; set; }


    private string _image;
    public string Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged();
        }
    }

    private Movie _movie = new();
    public Movie Movie
    {
        get => _movie;
        set
        {
            _movie = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Movie> _movieCollection = new();
    public ObservableCollection<Movie> MovieCollection
    {
        get => _movieCollection;
        set
        {
            _movieCollection = value;
            OnPropertyChanged();
        }
    }

    public ICommand UploadImageCommand => new Command(async () => await UploadImage());
    public ICommand SaveCommand => new Command(async () => await Save());
    public ICommand ResetCommand => new Command(Reset);


    private void EditMovie(object obj)
    {
        throw new NotImplementedException();
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

        MovieCollection.Add(Movie);
        await SaveMovieToJson();

    }

    private async Task SaveMovieToJson()
    {
        GenerateId();
        string json = JsonSerializer.Serialize(MovieCollection);
        await File.WriteAllTextAsync(movieFilePath, json);

        await Shell.Current.DisplayAlert("Add Movie", "Movie added successfully", "OK");
        NavigateBack();
    }

    private async void NavigateBack()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(MovieCollection), MovieCollection }
        };

        await Shell.Current.GoToAsync("..", navigationParameter);
    }

    private void GenerateId()
    {
        Movie.Id = MovieCollection.Count + 1;
    }

    private void Reset()
    {
        //reset the movie object
        Movie = new Movie();
        Image = defaultImage;
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
        if (Image == defaultImage)
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
