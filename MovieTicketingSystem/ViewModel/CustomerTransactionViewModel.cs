﻿
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

    public CustomerTransactionViewModel(TransactionService transactionService, SharedDataService sharedDataService)
    {
        this.transactionService = transactionService;
        this.sharedDataService = sharedDataService;
        userId = sharedDataService.UserId;
        GetUserTransactionAsync();
    }

    private async void GetUserTransactionAsync()
    {
        Transactions = await transactionService.GetTransactionsByUserIdAsync(userId);
    }



}
