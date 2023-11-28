using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public class TicketService : BaseService<Ticket>
{
    public TicketService() : base("Tickets.json")
    {

    }

    public async Task<ObservableCollection<Ticket>> GetAllTicketsAsync()
    {
        var tickets = await GetItemsAsync();
        if (tickets != null)
        {
            foreach (var ticket in tickets)
            {
                ticket.Movie = await new MovieService().GetMovieByIdAsync(ticket.MovieId);
                ticket.User = await new UserService().GetUserByIdAsync(ticket.UserId, false);
            }
        }

        return tickets;
    }

    private async Task<int> GetNextIdAsync()
    {
        var tickets = await GetAllTicketsAsync();
        return tickets.Count + 1;
    }

    public async Task<bool> AddTicketAsync(User user, Movie movie)
    {
        if (user == null || movie == null)
        {
            return false;
        }

        var ticketCollection = await GetAllTicketsAsync();
        var ticket = new Ticket
        {
            Id = ticketCollection.Count + 1,
            UserId = user.Id,
            MovieId = movie.Id,
            DateBooked = DateTime.Now,
        };
        ticketCollection.Add(ticket);
        await SaveToJsonAsync(ticketCollection);
        return true;
    }
}
