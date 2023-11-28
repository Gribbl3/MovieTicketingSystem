using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public class CinemaService : BaseService<Cinema>
{
    public CinemaService() : base("Cinemas.json") { }

    public async Task<ObservableCollection<Cinema>> GetCinemasAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<ObservableCollection<Cinema>> GetActiveCinemasAsync()
    {
        var cinemaCollection = await GetItemsAsync();
        return new ObservableCollection<Cinema>(cinemaCollection.Where(c => !c.IsDeleted));
    }

    public async Task<ObservableCollection<Cinema>> GetDeletedCinemasAsync()
    {
        var cinemaCollection = await GetItemsAsync();
        return new ObservableCollection<Cinema>(cinemaCollection.Where(c => c.IsDeleted));
    }

    public async Task<Cinema> GetCinemaByIdAsync(int id)
    {
        var cinemaCollection = await GetItemsAsync();
        return cinemaCollection.FirstOrDefault(c => c.Id == id);
    }

    public async Task<(bool, ObservableCollection<Cinema>)> AddCinemaAsync(Cinema newCinema)
    {
        var cinemaCollection = await GetCinemasAsync();
        if (newCinema == null)
        {
            await Shell.Current.DisplayAlert("Error", "Please enter valid cinema", "OK");
            return (false, cinemaCollection);
        }

        int cinemaCount = cinemaCollection.Count(c => c.Mall.Id == newCinema.Mall.Id);
        newCinema.Id = cinemaCollection.Count + 1;
        newCinema.Name = $"CINEMA-{newCinema.Mall.Name}-{cinemaCount + 1}";
        cinemaCollection.Add(newCinema);
        bool isSaved = await SaveToJsonAsync(cinemaCollection);

        return (isSaved, cinemaCollection);
    }

    public async Task<ObservableCollection<Cinema>> DeleteCinemaAsync(int id)
    {
        var cinemaCollection = await GetCinemasAsync();
        var cinemaToBeDeleted = cinemaCollection.FirstOrDefault(c => c.Id == id);
        if (cinemaToBeDeleted == null)
        {
            await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");
            return cinemaCollection;
        }

        if (cinemaToBeDeleted.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Cinema already deleted", "OK");
            return cinemaCollection;
        }

        cinemaToBeDeleted.IsDeleted = true;
        bool isSaved = await SaveToJsonAsync(cinemaCollection);
        if (!isSaved)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to delete cinema", "OK");
            return cinemaCollection;
        }

        cinemaCollection.Remove(cinemaToBeDeleted);
        return cinemaCollection;
    }

    public async Task<(bool, ObservableCollection<Cinema>)> UpdateCinemaAsync(Cinema cinema)
    {
        var cinemaCollection = await GetCinemasAsync();
        if (cinema == null)
        {
            await Shell.Current.DisplayAlert("Error", "Please enter valid cinema", "OK");
            return (false, cinemaCollection);
        }

        var cinemaToBeUpdated = cinemaCollection.FirstOrDefault(c => c.Id == cinema.Id);
        if (cinemaToBeUpdated == null)
        {
            await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");
            return (false, cinemaCollection);
        }

        cinemaToBeUpdated.Name = cinema.Name;
        cinemaToBeUpdated.SeatCapacity = cinema.SeatCapacity;
        cinemaToBeUpdated.Mall = cinema.Mall;

        bool isSaved = await SaveToJsonAsync(cinemaCollection);
        if (!isSaved)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to update cinema", "OK");
            return (false, cinemaCollection);
        }

        return (true, cinemaCollection);
    }

    public async Task<ObservableCollection<Cinema>> RestoreDeletedCinemaAsync(int id)
    {
        var cinemaCollection = await GetCinemasAsync();
        var cinemaToBeRestored = cinemaCollection.FirstOrDefault(c => c.Id == id);
        if (cinemaToBeRestored == null)
        {
            await Shell.Current.DisplayAlert("Error", "Cinema not found", "OK");
            return cinemaCollection;
        }

        if (!cinemaToBeRestored.IsDeleted)
        {
            await Shell.Current.DisplayAlert("Error", "Cinema not deleted", "OK");
            return cinemaCollection;
        }

        cinemaToBeRestored.IsDeleted = false;
        bool isSaved = await SaveToJsonAsync(cinemaCollection);
        if (!isSaved)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to restore cinema", "OK");
            return cinemaCollection;
        }

        return cinemaCollection;
    }
}
