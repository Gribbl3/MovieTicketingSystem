using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(MallCollection), nameof(MallCollection))]
public class AddMallViewModel : BaseViewModel
{
    private readonly string mallFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Malls.json");
    private bool isEditing = false;

    private ObservableCollection<Mall> _mallCollection = new();
    public ObservableCollection<Mall> MallCollection
    {
        get => _mallCollection;
        set
        {
            _mallCollection = value;
            OnPropertyChanged();
        }
    }

    private Mall _mall = new();
    public Mall Mall
    {
        get => _mall;
        set
        {
            _mall = value;
            OnPropertyChanged();
        }
    }

    private Mall _selectedMallForEdit;
    public Mall SelectedMallForEdit
    {
        get => _selectedMallForEdit;
        set
        {
            _selectedMallForEdit = value;
            OnPropertyChanged();
        }
    }

    public AddMallViewModel()
    {
        GetMallsFromJson();
    }

    public ICommand AddCommand => new Command(async () => await AddMall());
    public ICommand DeleteCommand => new Command<int>(DeleteMall);
    public ICommand EditCommand => new Command<Mall>(EditMall);
    public ICommand SaveCommand => new Command(SaveEditedMall);
    public ICommand ResetCommand => new Command<Mall>(ResetMall);

    /// <summary>
    /// Add mall to json file
    /// </summary>
    /// <returns>void</returns>
    private async Task AddMall()
    {
        bool isValidMall = await ValidateMall(Mall);
        if (!isValidMall)
            return;

        GenerateId();
        MallCollection.Add(Mall);
        string json = JsonSerializer.Serialize<ObservableCollection<Mall>>(MallCollection);
        await File.WriteAllTextAsync(mallFilePath, json);

        await Shell.Current.DisplayAlert("Success", "Mall added successfully", "OK");
        ResetMall(Mall);
    }

    /// <summary>
    /// Delete mall from json file, gets id from user input
    /// </summary>
    /// <param name="id"></param>
    private async void DeleteMall(int id)
    {
        foreach (Mall mall in MallCollection)
        {
            if (mall.Id == id)
            {
                MallCollection.Remove(mall);
                string mallJson = JsonSerializer.Serialize(MallCollection);
                await File.WriteAllTextAsync(mallFilePath, mallJson);
                await Shell.Current.DisplayAlert("Success", "Mall deleted successfully", "OK");
                return;
            }
        }

        //string result = await Shell.Current.DisplayPromptAsync("Delete Mall", "Enter mall id to delete");
        //if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int id))
        //{
        //    await Shell.Current.DisplayAlert("Error", "Please enter valid mall id", "OK");
        //    return;
        //}

        //foreach (Mall mall in MallCollection)
        //{
        //    if (mall.Id == id)
        //    {
        //        MallCollection.Remove(mall);
        //        string mallJson = JsonSerializer.Serialize(MallCollection);
        //        await File.WriteAllTextAsync(mallFilePath, mallJson);
        //        await Shell.Current.DisplayAlert("Success", "Mall deleted successfully", "OK");
        //        return;
        //    }
        //}

        //await Shell.Current.DisplayAlert("Error", "Mall not found", "OK");
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
            return;

        //loop through mall collection and replace selected mall with edited mall
        for (int index = 0; index < MallCollection.Count; index++)
        {
            if (MallCollection[index].Id == SelectedMallForEdit.Id)
            {
                MallCollection[index] = SelectedMallForEdit;
                string mallJson = JsonSerializer.Serialize(MallCollection);
                await File.WriteAllTextAsync(mallFilePath, mallJson);
                await Shell.Current.DisplayAlert("Success", "Mall edited successfully", "OK");

                //reset flag and selected mall
                isEditing = false;
                ResetMall(SelectedMallForEdit);
                return;
            }
        }

        await Shell.Current.DisplayAlert("Error", "Mall not found", "OK");
    }

    /// <summary>
    /// Generates mall id
    /// </summary>
    private void GenerateId()
    {
        //
        Mall.Id = MallCollection.Count + 1;
    }

    /// <summary>
    /// Reset mall name and address
    /// </summary>
    private void ResetMall(Mall mall)
    {
        //if user is editing, reset selected mall else reset mall
        if (isEditing)
        {
            SelectedMallForEdit = new Mall();
            OnPropertyChanged(nameof(SelectedMallForEdit));
            return;
        }

        Mall = new Mall();
        OnPropertyChanged(nameof(Mall));

    }

    /// <summary>
    /// Validate mall name and address
    /// </summary>
    /// <returns>boolean</returns>
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

    /// <summary>
    /// Get malls from json file
    /// </summary>
    public async void GetMallsFromJson()
    {
        if (!File.Exists(mallFilePath))
        {
            MallCollection = new ObservableCollection<Mall>();
            return;
        }

        string json = await File.ReadAllTextAsync(mallFilePath);
        MallCollection = JsonSerializer.Deserialize<ObservableCollection<Mall>>(json);
    }
}
