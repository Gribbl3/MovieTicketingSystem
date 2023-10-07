using MovieTicketingSystem.View;

namespace MovieTicketingSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

            InitializeComponent();
        }
    }
}