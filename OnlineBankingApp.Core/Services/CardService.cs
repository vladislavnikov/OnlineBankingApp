using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.ViewModels.Card;
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
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext context;

        public CardService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task CreateCardAsync(string userId)
        {
            int mii = 4;
            Random random = new Random();
            string restOfDigits = string.Join("", Enumerable.Range(0, 15).Select(_ => random.Next(0, 10)));

            string cardNumber = $"{mii}{restOfDigits}";

            var card = new Card()
            {
                Number = cardNumber,
                Balance = 0,
                UserId = userId
            };

            context.Cards.Add(card);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CardViewModel>> GetAllCardsAsync(string userId)
        {
            var cards = await context.Cards.Include(c => c.Transactions)
            .Where(card => card.UserId == userId)
            .ToListAsync();

            return cards.Select(c => new CardViewModel()
            {
                Id = c.Id,
                Balance = c.Balance,
                Number = c.Number,
                Transactions = c.Transactions.Select(t => new TransactionViewModel
                {
                    Type = t.Type,
                    Amount = t.Amount,
                })
            });
        }

        public async Task<Card> GetCardAsync(string userId)
        {
            var card = await context.Cards.FirstOrDefaultAsync(c => c.UserId == userId);

            return new Card()
            {
                Id = card.Id,
                Balance = card.Balance,
                Number = card.Number,
                Transactions = card.Transactions,
                UserId = userId

            };
        }

        public async Task<Card> GetCardAsync(int cardId)
        {
            var card = await context.Cards.FirstOrDefaultAsync(c => c.Id == cardId);

            return new Card()
            {
                Id = card.Id,
                Balance = card.Balance,
                Number = card.Number,
                Transactions = card.Transactions,
                UserId = card.UserId
            };
        }

        public async Task<Card> GetCardByNumberAsync(string number)
        {
            var card = await context.Cards.FirstOrDefaultAsync(c => c.Number == number);

            return new Card()
            {
                Id = card.Id,
                Balance = card.Balance,
                Number = card.Number,
                Transactions = card.Transactions,
                UserId = card.UserId
            };
        }

        public async Task<ICollection<Card>> GetUserCardsAsync(string userId)
        {
            var cards = await context.Cards.Where(c => c.UserId == userId).ToListAsync();

            return cards;
        }
    }
}