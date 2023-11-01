using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace MovieTicketingSystem.ViewModel;

public class AddMallViewModel : BaseViewModel
{
    private readonly string mainDir = FileSystem.Current.AppDataDirectory;
    public Mall Mall { get; set; } = new();
    public ObservableCollection<Mall> MallCollection { get; set; } = new();

    public ICommand SaveCommand => new Command(async () => await AddMall());
    public ICommand ResetCommand => new Command(ResetMall);

    /// <summary>
    /// Add mall to json file
    /// </summary>
    /// <returns>void</returns>
    private async Task AddMall()
    {
        bool isValidMall = await ValidateMall();
        if (!isValidMall)
            return;

        GenerateId();
        MallCollection.Add(Mall);
        string json = JsonSerializer.Serialize(Mall);
        string mallFile = Path.Combine(mainDir, "Malls", $"{Guid.NewGuid()}.json");
        await File.WriteAllTextAsync(mallFile, json);

        await Shell.Current.DisplayAlert("Success", "Mall added successfully", "OK");
    }

    /// <summary>
    /// Generates mall id
    /// </summary>
    private void GenerateId()
    {
        Mall.Id = MallCollection.Count + 1;
    }

    /// <summary>
    /// Reset mall name and address
    /// </summary>
    private void ResetMall()
    {
        Mall.Name = Mall.Address = string.Empty;
    }

    /// <summary>
    /// Validate mall name and address
    /// </summary>
    /// <returns>boolean</returns>
    private async Task<bool> ValidateMall()
    {

        if (string.IsNullOrEmpty(Mall.Name))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter mall name", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(Mall.Address))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter mall address", "OK");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Create Malls folder if not exist
    /// </summary>
    /// <returns>void</returns>
    private async Task CreateMallsFolder()
    {
        try
        {
            string mallsFolder = Path.Combine(mainDir, "Malls");
            if (!Directory.Exists(mallsFolder))
            {
                Directory.CreateDirectory(mallsFolder);
                return;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    /// <summary>
    /// Get malls from json files
    /// </summary>
    /// <returns>void</returns>
    public async void GetMallsFromJson()
    {
        MallCollection.Clear();

        await CreateMallsFolder();
        //check from folder json files
        string mallsFolder = Path.Combine(mainDir, "Malls");
        string[] mallFiles = Directory.GetFiles(mallsFolder, "*.json");

        if (mallFiles.Length == 0)
            return;

        foreach (var mallFile in mallFiles)
        {
            string json = await File.ReadAllTextAsync(mallFile);
            Mall mall = JsonSerializer.Deserialize<Mall>(json);
            MallCollection.Add(mall);
        }
    }
}
