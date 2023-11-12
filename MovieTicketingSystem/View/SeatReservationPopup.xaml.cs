using CommunityToolkit.Maui.Views;
using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class SeatReservationPopup : Popup
{
    public SeatReservationPopup(SeatReservationPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}