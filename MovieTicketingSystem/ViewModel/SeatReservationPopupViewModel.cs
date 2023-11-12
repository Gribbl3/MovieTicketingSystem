using MovieTicketingSystem.Model;

namespace MovieTicketingSystem.ViewModel;

public class SeatReservationPopupViewModel : BaseViewModel
{
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

    public void PerformUpdates(Cinema Cinema)
    {
        this.Cinema = Cinema;
    }
}
