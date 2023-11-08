namespace MovieTicketingSystem.View;

public partial class Logout : ContentPage
{
    public Logout()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DisplayLogoutConfirmation();
    }

    private async void DisplayLogoutConfirmation()
    {
        bool isLogout = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (isLogout)
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}