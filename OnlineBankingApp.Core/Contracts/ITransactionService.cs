using OnlineBankingApp.Core.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Contracts
{
    public interface ITransactionService
    {
        Task MakeTransactionAsync(TransactionViewModel model, int cardId);
    }
}
