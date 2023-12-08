using MovieTicketingSystem.ViewModel;

namespace MovieTicketingSystem.View;

public partial class AddMall : ContentPage
{
    public AddMall(AddMallViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}