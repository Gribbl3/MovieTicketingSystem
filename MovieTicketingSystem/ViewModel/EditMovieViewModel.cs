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
    private readonly string defaultImage = "add_photo.png";


    public ObservableCollection<Genre> AvailableGenre { get; set; } = new();
    public ObservableCollection<Subtitle> AvailableSubtitle { get; set; } = new();


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
            CloneSelectedGenre();
            CloneSelectedSubtitle();
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


    public EditMovieViewModel()
    {
        PopulateGenre();
        PopulateSubtitle();
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

        //check Movie in the Movie Collection then save to movie json file
        for (int index = 0; index < MovieCollection.Count; index++)
        {
            if (MovieCollection[index].Id == Movie.Id)
            {
                //inserts the updated movie in the collection
                MovieCollection[index] = Movie;
                await SaveMovieToJson();
            }
        }

    }

    private async Task SaveMovieToJson()
    {
        string json = JsonSerializer.Serialize(MovieCollection);
        await File.WriteAllTextAsync(movieFilePath, json);

        await Shell.Current.DisplayAlert("Edit Movie", "Movie edited successfully", "OK");
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

    private void Reset()
    {
        //get the current id first
        int id = Movie.Id;
        //reset the movie
        Movie = new Movie();
        Image = defaultImage;
        PopulateSubtitle();
        PopulateGenre();
        //set the id back to the movie
        Movie.Id = id;
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

    /// <summary>
    /// Generates genre
    /// </summary>
    private void PopulateGenre()
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

    /// <summary>
    /// Clones selected genre from movie to available genre
    /// </summary>
    private void CloneSelectedGenre()
    {
        foreach (var genre in AvailableGenre)
        {
            if (Movie.SelectedGenre.Contains(genre.Name))
            {
                genre.IsSelected = true;
            }
        }
    }

    /// <summary>
    /// Generates subtitle
    /// </summary>
    private void PopulateSubtitle()
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

    /// <summary>
    /// Clones selected subtitle from movie to available subtitle
    /// </summary>
    private void CloneSelectedSubtitle()
    {
        foreach (var subtitle in AvailableSubtitle)
        {
            if (Movie.SelectedSubtitle.Contains(subtitle.Language))
            {
                subtitle.IsSelected = true;
            }
        }
    }
}
