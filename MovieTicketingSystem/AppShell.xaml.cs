using MovieTicketingSystem.View;

namespace MovieTicketingSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Register), typeof(Register));
            Routing.RegisterRoute(nameof(Customer), typeof(Customer));
            Routing.RegisterRoute(nameof(Admin), typeof(Admin));
            InitializeComponent();
        }
    }
}