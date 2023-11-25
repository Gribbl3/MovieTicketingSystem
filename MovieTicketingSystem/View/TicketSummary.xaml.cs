using MovieTicketingSystem.Model;
using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class TicketSummary
{
    TicketSummaryViewModel viewModel;
    public TicketSummary(Cinema Cinema, User User, Movie Movie)
    {
        InitializeComponent();
        viewModel = new TicketSummaryViewModel(Cinema, User, Movie);
        BindingContext = viewModel;
    }
}