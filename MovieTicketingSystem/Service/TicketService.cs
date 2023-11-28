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
                ticket.Seat = ticket.Movie.Cinemas.SelectMany(c => c.Seats).FirstOrDefault(s => s.Id == ticket.SeatId);
            }
        }
        return tickets;
    }

    public async Task<bool> AddTicketAsync(User user, Movie movie, Seat seat)
    {
        if (user == null || movie == null)
        {
            return false;
        }

        var ticketCollection = await GetItemsAsync();
        var ticket = new Ticket
        {
            Id = ticketCollection.Count + 1,
            UserId = user.Id,
            MovieId = movie.Id,
            SeatId = seat.Id,
            DateBooked = DateTime.Now,
            IsCancelled = false,
            User = null,
            Seat = null,
            Movie = null
        };

        await new TransactionService(ticket.UserId).AddTransactionAsync(ticket);
        ticketCollection.Add(ticket);
        await SaveToJsonAsync(ticketCollection);
        return true;
    }
}
