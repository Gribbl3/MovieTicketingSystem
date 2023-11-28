using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public class TransactionService : BaseService<Model.Transaction>
{
    private int userId;
    public TransactionService(int userId) : base($"Customer{userId}.json")
    {
        this.userId = userId;
    }

    public async Task<ObservableCollection<Model.Transaction>> GetTransactionsByUserIdAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<bool> AddTransactionAsync(Ticket ticket)
    {
        var transactionCollection = await GetItemsAsync();
        var transaction = new Model.Transaction
        {
            Id = transactionCollection.Count + 1,
            TicketId = ticket.Id,
            DatePaid = default,
            IsPaid = false,
        };

        transactionCollection.Add(transaction);
        await SaveToJsonAsync(transactionCollection);
        return true;
    }
}
