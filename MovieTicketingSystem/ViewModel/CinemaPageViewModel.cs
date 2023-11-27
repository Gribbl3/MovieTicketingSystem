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
        ShowAllCinemas();
    }

    public ICommand AddCinemaCommand => new Command(GoToAddCinemaPage);
    public ICommand DeleteCinemaCommand => new Command(DeleteCinema);
    public ICommand EditCinemaCommand => new Command(GoToEditCinemaPage);
    public ICommand ShowActiveCinemasCommand => new Command(ShowActiveCinemas);
    public ICommand ShowDeletedCinemasCommand => new Command(ShowDeletedCinemas);
    public ICommand ShowAllCinemasCommand => new Command(ShowAllCinemas);
    public ICommand RestoreDeletedCinemaCommand => new Command(RestoreDeletedCinema);

    private async void ShowAllCinemas()
    {
        CinemaCollection = await cinemaService.GetCinemasAsync();
    }

    private async void ShowActiveCinemas()
    {
        CinemaCollection = await cinemaService.GetActiveCinemasAsync();
    }

    private async void ShowDeletedCinemas()
    {
        CinemaCollection = await cinemaService.GetDeletedCinemasAsync();
    }

    private async void RestoreDeletedCinema()
    {
        string result = await Shell.Current.DisplayPromptAsync("Restore Cinema", "Enter cinema id to restore");
        if (!int.TryParse(result, out int id))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter valid cinema id", "OK");
            return;
        }
        await cinemaService.RestoreDeletedCinemaAsync(id);
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

        CinemaCollection = await cinemaService.DeleteCinemaAsync(id);
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
