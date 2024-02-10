using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Core.ViewModels.Transaction;
using OnlineBankingApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Contracts
{
    public interface ITransactionService
    {
        Task DepositAsync(double amount, int cardId);
        Task WithdrawAsync(double amount, int cardId);
        Task SendAsync(double amount,int cardId, int cardToSendId);

        Task<IEnumerable<TransactionType>> GetTypeAsync();
        Task<IEnumerable<TransactionViewModel>> GetAllTransactionsAsync(IEnumerable<int> cardIds);

    }
}
