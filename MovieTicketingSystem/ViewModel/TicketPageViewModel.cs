using MovieTicketingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(Movie), nameof(Movie))]
public class TicketPageViewModel : BaseViewModel
{
    private Movie _movie;
    public Movie Movie
    {
        get => _movie;
        set
        {
            _movie = value;
            OnPropertyChanged();
        }
    }
    public ICommand GoBackCommand => new Command(GoBack);

    private async void GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
