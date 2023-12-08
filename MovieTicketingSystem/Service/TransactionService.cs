using MovieTicketingSystem.Model;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.Service;

public class TransactionService : BaseService<Model.Transaction>
{
    public TransactionService() : base($"Customer.json")
    {
    }

    public async Task<ObservableCollection<Model.Transaction>> GetTransactionsByUserIdAsync(int userId)
    {
        UpdateFilePath($"Customer{userId}.json");
        return await GetItemsAsync();
    }

    public async Task<bool> AddTransactionAsync(Ticket ticket, int userId)
    {
        UpdateFilePath($"Customer{userId}.json");

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
