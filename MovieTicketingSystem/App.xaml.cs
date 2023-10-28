using Microsoft.Maui;

namespace MovieTicketingSystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 1366;
            const int newHeight = 768;

            window.X = 200;
            window.Y = 100;

            window.Width = newWidth;
            window.Height = newHeight;

            window.MinimumHeight = newHeight;
            window.MinimumWidth = newWidth;

            window.MaximumHeight = newHeight;
            window.MaximumWidth = newWidth;

            return window;

        }

    }


}