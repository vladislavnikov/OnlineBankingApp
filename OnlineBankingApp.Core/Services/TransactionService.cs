using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.ViewModels.Transaction;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;
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


        public async Task DepositAsync(double amount, int cardId)
		{
			var card = await context.Cards.FindAsync(cardId);

			var transaction = new Transaction()
			{
				Amount = amount,
                TransactionTypeId = 1,
				CardId = cardId,
                Type = "Deposit"
			};

			await context.Transactions.AddAsync(transaction);

			card.Balance += amount;

			await context.SaveChangesAsync();
		}
		public async Task<IEnumerable<TransactionType>> GetTypeAsync()
        {
            return await context.TransactionTypes.ToListAsync();
        }

        public async Task SendAsync(double amount, int cardId, int cardToSendId)
        {
            var card = await context.Cards.FindAsync(cardId);
            var cardToSend = await context.Cards.FindAsync(cardToSendId);

            
                var transaction = new Transaction()
                {
                    Amount = amount,
                    TransactionTypeId = 3,
                    CardId = cardId,
                    Type = "Send"
                };

                await context.Transactions.AddAsync(transaction);

                card.Balance -= amount;
                cardToSend.Balance += amount;

                await context.SaveChangesAsync();
            
        }

        public async Task WithdrawAsync(double аmount, int cardId)
        {
			var card = await context.Cards.FindAsync(cardId);

			var transaction = new Transaction()
			{
				Amount = аmount,
				TransactionTypeId = 2,
				CardId = cardId,
				Type = "Withdraw"
			};

			await context.Transactions.AddAsync(transaction);

			card.Balance -= аmount;

			await context.SaveChangesAsync();
		}
    }
}
