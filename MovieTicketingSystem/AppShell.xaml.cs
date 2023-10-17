using MovieTicketingSystem.View;

namespace MovieTicketingSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Register), typeof(Register));
            Routing.RegisterRoute(nameof(Customer), typeof(Customer));
            Routing.RegisterRoute(nameof(Admin), typeof(Admin));
            Routing.RegisterRoute(nameof(AddMovie), typeof(AddMovie));
            Routing.RegisterRoute(nameof(AddCinema), typeof(AddCinema));

            InitializeComponent();
        }

    }
}