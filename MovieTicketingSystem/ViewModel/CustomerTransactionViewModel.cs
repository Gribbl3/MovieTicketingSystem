
using MovieTicketingSystem.Model;
using MovieTicketingSystem.Service;
using System.Collections.ObjectModel;

namespace MovieTicketingSystem.ViewModel;

[QueryProperty(nameof(User), nameof(User))]
public class CustomerTransactionViewModel : BaseViewModel
{
    private readonly TransactionService transactionService;
    private readonly SharedDataService sharedDataService;

    private User _user;
    private ObservableCollection<Transaction> _transactions;
    private int userId;
    private bool _isVisible;
    private bool _isTextVisible;


    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Transaction> Transactions
    {
        get => _transactions;
        set
        {
            _transactions = value;
            OnPropertyChanged();
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged();
        }
    }

    public bool IsTextVisible
    {
        get => _isTextVisible;
        set
        {
            _isTextVisible = value;
            OnPropertyChanged();
        }
    }

    public CustomerTransactionViewModel(TransactionService transactionService, SharedDataService sharedDataService)
    {
        this.transactionService = transactionService;
        this.sharedDataService = sharedDataService;
        userId = sharedDataService.UserId;
    }

    public async void GetUserTransactionAsync()
    {
        Transactions = await transactionService.GetTransactionsByUserIdAsync(userId);
    }

}
