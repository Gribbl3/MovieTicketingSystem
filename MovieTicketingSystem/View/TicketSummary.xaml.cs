using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class TicketSummary : ContentPage
{
    public TicketSummary(TicketSummaryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}