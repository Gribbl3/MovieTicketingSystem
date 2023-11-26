using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class AddMallViewModel : BaseViewModel
{
    private readonly MallService mallService;
    private bool isEditing = false;
    private ObservableCollection<Mall> _mallCollection = new();
    private Mall _mall = new(), _selectedMallForEdit;



    public ObservableCollection<Mall> MallCollection
    {
        get => _mallCollection;
        set
        {
            _mallCollection = value;
            OnPropertyChanged();
        }
    }

    public Mall Mall
    {
        get => _mall;
        set
        {
            _mall = value;
            OnPropertyChanged();
        }
    }

    public Mall SelectedMallForEdit
    {
        get => _selectedMallForEdit;
        set
        {
            _selectedMallForEdit = value;
            OnPropertyChanged();
        }
    }

    public AddMallViewModel(MallService mallService)
    {
        this.mallService = mallService;
        ShowAllMalls();
    }

    public ICommand AddCommand => new Command(async () => await AddMall());
    public ICommand DeleteCommand => new Command<int>(DeleteMall);
    public ICommand EditCommand => new Command<Mall>(EditMall);
    public ICommand SaveCommand => new Command(SaveEditedMall);
    public ICommand ResetCommand => new Command<Mall>(ResetMall);
    public ICommand ShowDeletedMallsCommand => new Command(ShowDeletedMalls);
    public ICommand ShowAllMallsCommand => new Command(ShowAllMalls);
    public ICommand ShowActiveMallsCommand => new Command(ShowActiveMalls);

    private async Task AddMall()
    {
        bool isValidMall = await ValidateMall(Mall);
        if (!isValidMall)
            return;

        GenerateId();
        MallCollection.Add(Mall);
        bool isAdded = await mallService.AddUpdateMallAsync(MallCollection);

        if (isAdded)
        {
            await Shell.Current.DisplayAlert("Success", "Mall added successfully", "OK");
            ResetMall(Mall);
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Mall not added", "OK");
        }

    }

    private async void DeleteMall(int id)
    {
        MallCollection = await mallService.DeleteMallAsync(id, MallCollection);
    }

    private void EditMall(Mall mall)
    {
        //if we use this, it will modify the mall in the collection
        //to prevent it we clone the mall by instantiating a new mall
        ////SelectedMallForEdit = mall;

        //set selected mall for edit
        //does not modify the mall in the collection
        SelectedMallForEdit = new Mall
        {
            Id = mall.Id,
            Name = mall.Name,
            Address = mall.Address
        };

        //add flag if user is editing
        isEditing = true;
    }

    private async void SaveEditedMall()
    {
        //if user is not editing, display error message
        if (!isEditing)
        {
            await Shell.Current.DisplayAlert("Error", "Please select mall to edit", "OK");
            return;
        }

        //if user is editing, validate mall name and address
        bool isValidMall = await ValidateMall(SelectedMallForEdit);
        if (!isValidMall)
        {
            return;
        }

        MallCollection = await mallService.UpdateMallAsync(SelectedMallForEdit, MallCollection);
        ////loop through mall collection and replace selected mall with edited mall
        //for (int index = 0; index < MallCollection.Count; index++)
        //{
        //    if (MallCollection[index].Id == SelectedMallForEdit.Id)
        //    {
        //        MallCollection[index] = SelectedMallForEdit;
        //        await mallService.AddUpdateMallAsync(MallCollection);
        //        await Shell.Current.DisplayAlert("Success", "Mall edited successfully", "OK");

        //        //reset flag and selected mall
        //        isEditing = false;
        //        ResetMall(SelectedMallForEdit);
        //        return;
        //    }
        //}
        await Shell.Current.DisplayAlert("Error", "Mall not found", "OK");
    }

    private void GenerateId()
    {
        Mall.Id = MallCollection.Count + 1;
    }

    private void ResetMall(Mall mall)
    {
        //if user is editing, reset selected mall else reset mall
        if (isEditing)
        {
            SelectedMallForEdit = new Mall();
            return;
        }

        Mall = new Mall();

    }

    private async Task<bool> ValidateMall(Mall mall)
    {

        if (string.IsNullOrEmpty(mall.Name))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter mall name", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(mall.Address))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter mall address", "OK");
            return false;
        }

        return true;
    }

    private async void ShowAllMalls()
    {
        MallCollection = await mallService.GetMallsAsync();
    }
    private async void ShowDeletedMalls()
    {
        MallCollection = await mallService.GetDeletedMallsAsync();
    }
    private async void ShowActiveMalls()
    {
        MallCollection = await mallService.GetActiveMallsAsync();
    }
}
