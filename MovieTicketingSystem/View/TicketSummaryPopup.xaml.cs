using CommunityToolkit.Maui.Views;
using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class TicketSummaryPopup : Popup
{
    public TicketSummaryPopup(TicketSummaryPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}