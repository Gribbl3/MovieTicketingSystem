namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(FirstName), "name")]
public class CustomerViewModel : BaseViewModel
{
    private static object FirstName()
    {
        throw new NotImplementedException();
    }
}
