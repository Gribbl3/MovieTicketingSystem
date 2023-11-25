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

    private void GoBack(object sender, EventArgs e) => Close();
    private void Confirm(object sender, EventArgs e)
    {
        Shell.Current.DisplayAlert("Success", "Your reservation has been confirmed", "OK");
        Close();
    }
}