using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class TicketPage : ContentPage
{
	public TicketPage(TicketPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}