using MovieTicketingSystem.View;

namespace MovieTicketingSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(CustomerLogin), typeof(CustomerLogin));

            InitializeComponent();
        }
    }
}