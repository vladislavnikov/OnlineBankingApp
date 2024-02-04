using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.ViewModels.Transaction;
using OnlineBankingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext context;

        public TransactionService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task MakeTransactionAsync(TransactionViewModel model, int cardId)
        {
            var type = model.Type;

            var card = await context.Cards.FindAsync(cardId);

            if (type == "Depost")
            {
                card.Balance += model.Amount;
            }
            else if (type == "Withdraw")
            {
                card.Balance -= model.Amount;
            }

            await context.SaveChangesAsync();
        }
    }
}
