using OnlineBankingApp.Core.Contracts;
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
    }
}
