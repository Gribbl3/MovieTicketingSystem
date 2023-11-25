using CommunityToolkit.Maui.Views;
using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class SeatReservationPopup : Popup
{
    SeatReservationPopupViewModel viewModel;
    public SeatReservationPopup()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void GoBack(object sender, EventArgs e) => Close();
    private async void Confirm(object sender, EventArgs e)
    {
        viewModel.DisplayPopup();
        await CloseAsync();
    }
}