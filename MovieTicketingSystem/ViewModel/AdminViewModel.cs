namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(FirstName), "name")]
public class AdminViewModel : BaseViewModel
{
    private static object FirstName()
    {
        throw new NotImplementedException();
    }
}
