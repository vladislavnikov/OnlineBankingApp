using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.Services;
using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Core.ViewModels.Transaction;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;
using System.Collections.Generic;

namespace OnlineBankingApp.Controllers
{
    public class CardController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICardService cardService;
        private readonly ITransactionService transactionService;

        public CardController(UserManager<ApplicationUser> _userManager,
            ICardService _cardService,
            ITransactionService _transactionService)
        {
            this.userManager = _userManager;
            this.cardService = _cardService;
            this.transactionService = _transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Card()
        {
            var user = await userManager.GetUserAsync(User);
            var card = await cardService.GetCardAsync(user.Id);

            if (card == null)
            {
                await cardService.CreateCardAsync(user.Id);
                card = await cardService.GetCardAsync(user.Id);
            }
            var model = await cardService.GetAllCardsAsync(user.Id);

            await Sidebar();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCard()
        {
            var user = await userManager.GetUserAsync(User);

            await cardService.CreateCardAsync(user.Id);
            //?
            return RedirectToAction("Card");
        }

        [HttpGet]
        public async Task<IActionResult> AllCardTransactions()
        {
            var user = await userManager.GetUserAsync(User);
            var cards = await cardService.GetAllCardsAsync(user.Id);

            var transactions = new List<IEnumerable<TransactionViewModel>>();

            foreach (var card in cards)
            {
                var cardTransactions = await transactionService.GetAllTransactionsAsync(new List<int> { card.Id });
                transactions.Add(cardTransactions);
            }

            return PartialView("_AllCardTransactions", transactions);
        }

        [HttpGet]
        public async Task<IActionResult> Sidebar()
        {
            var user = await userManager.GetUserAsync(User);
            var cards = await cardService.GetAllCardsAsync(user.Id);

            cards = cards.Select(c => new CardViewModel
            {
                Id = c.Id,
                Balance = c.Balance,
                Number = c.Number,
                Transactions = c.Transactions
            });

            ViewBag.Cards = cards;
            return PartialView("_Sidebar");
        }

    }
}