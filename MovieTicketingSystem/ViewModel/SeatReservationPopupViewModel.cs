using CommunityToolkit.Maui.Core;
using Mopups.Services;
using MovieTicketingSystem.Model;
using MovieTicketingSystem.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;


public class SeatReservationPopupViewModel : BaseViewModel
{
    private readonly IPopupService popupService;


    private ObservableCollection<Seat> _selectedSeats = new();
    public ObservableCollection<Seat> SelectedSeats
    {
        get => _selectedSeats;
        set
        {
            _selectedSeats = value;
            OnPropertyChanged();
        }
    }

    private Cinema _cinema;
    public Cinema Cinema
    {
        get => _cinema;
        set
        {
            _cinema = value;
            OnPropertyChanged();
        }
    }

    private User _user;
    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public Movie Movie { get; set; }

    public SeatReservationPopupViewModel(IPopupService popupService)
    {
        this.popupService = popupService;
    }

    public void PerformUpdates(Cinema Cinema, User User, Movie Movie)
    {
        this.Cinema = Cinema;
        this.User = User;
        this.Movie = Movie;
    }

    public ICommand UpdateSeatAvailabilityCommand => new Command<Seat>(UpdateSeatAvailability);

    private void UpdateSeatAvailability(Seat Seat)
    {
        if (Seat == null)
        {
            return;
        }

        if (Seat.IsAvailableSeat)
        {
            Seat.IsAvailableSeat = false;
            return;
        }

        Seat.IsAvailableSeat = true;
    }

    public async void GoToTicketSummary()
    {
        await MopupService.Instance.PushAsync(new TicketSummary(Cinema, User, Movie));
    }

}
