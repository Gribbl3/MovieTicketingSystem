using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(CinemaCollection), nameof(CinemaCollection))]
public class CinemaPageViewModel : BaseViewModel
{
    private readonly CinemaService cinemaService;
    private ObservableCollection<Cinema> _cinemaCollection = new();
    public ObservableCollection<Cinema> CinemaCollection
    {
        get => _cinemaCollection;
        set
        {
            _cinemaCollection = value;
            OnPropertyChanged();
        }
    }

    public CinemaPageViewModel(CinemaService cinemaService)
    {
        this.cinemaService = cinemaService;
        PopulateCinemas();
    }

    public ICommand AddCinemaCommand => new Command(GoToAddCinemaPage);
    public ICommand DeleteCinemaCommand => new Command(DeleteCinema);
    public ICommand EditCinemaCommand => new Command(GoToEditCinemaPage);

    private async void PopulateCinemas()
    {
        CinemaCollection = await cinemaService.GetCinemasAsync();
    }

    private async void GoToAddCinemaPage()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(CinemaCollection), CinemaCollection }
        };
        await Shell.Current.GoToAsync($"{nameof(AddCinema)}", navigationParameter);
    }

    private async void DeleteCinema()
    {
        string result = await Shell.Current.DisplayPromptAsync("Delete Cinema", "Enter cinema id to delete");
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
            return;

        foreach (Cinema cinema in CinemaCollection)
        {
            if (cinema.Id == id)
            {
                cinema.IsDeleted = !cinema.IsDeleted;
                await cinemaService.AddUpdateCinemaAsync(CinemaCollection);
                CinemaCollection.Remove(cinema);
                await Shell.Current.DisplayAlert("Success", "Cinema deleted successfully", "OK");
                return;
            }
        }

        await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");
    }

    private async void GoToEditCinemaPage()
    {
        string result = await Shell.Current.DisplayPromptAsync("Edit Cinema", "Enter cinema id to edit");
        if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
            return;

        foreach (Cinema cinema in CinemaCollection)
        {
            if (cinema.Id == id)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { nameof(Cinema), cinema },
                    { nameof(CinemaCollection), CinemaCollection}
                };
                await Shell.Current.GoToAsync($"{nameof(EditCinema)}", navigationParameter);
                return;
            }
        }
    }
}
