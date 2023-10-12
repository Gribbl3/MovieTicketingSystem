using MovieTicketingSystem.Model;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class AddMovieViewModel
{
    public Movie Movie { get; set; } = new Movie();

    public ICommand UploadImageCommand => new Command(UploadImage);

    private async void UploadImage()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the movie",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
        {
            return;
        }

        var stream = await result.OpenReadAsync();

        Movie.Image = ImageSource.FromStream(() => stream);
    }
}
