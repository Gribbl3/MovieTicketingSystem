using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class AdminLogin : ContentPage
{
	public AdminLogin()
	{
		InitializeComponent();
		BindingContext = new AdminLoginViewModel();
	}
}