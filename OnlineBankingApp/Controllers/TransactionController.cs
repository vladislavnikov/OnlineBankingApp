using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.Services;
using OnlineBankingApp.Core.ViewModels.Transaction;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;

namespace OnlineBankingApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITransactionService transactionService;
        private readonly ICardService cardService;

        public TransactionController(UserManager<ApplicationUser> _userManager,
            ITransactionService _transactionService,
            ICardService _cardService)
        {
            
            this.userManager = _userManager;
            this.transactionService = _transactionService;
            this.cardService = _cardService;
        }


        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var model = new MakeTransactionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(MakeTransactionViewModel model)
        { 
            var card = await cardService.GetCardAsync(model.CardId);
            //GetCard

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
               await transactionService.DepositAsync(model.Amount, card.Id);
				return RedirectToAction("Card", "Card");
			}
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
				return View(model);
			}

		}

        [HttpGet]
        public async Task<IActionResult> Withdraw()
        {
            var model = new MakeTransactionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(MakeTransactionViewModel model)
        {
            var user = await userManager.GetUserAsync(User);

            var card = await cardService.GetCardAsync(user.Id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await transactionService.WithdrawAsync(model.Amount, card.Id);
				return RedirectToAction("Card", "Card");
			}
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }

			
		}

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            var model = new MakeTransactionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Send(MakeTransactionViewModel model, string sendToNumber)
        {
            var user = await userManager.GetUserAsync(User);

            var card = await cardService.GetCardAsync(user.Id);
            var cardToSend = await cardService.GetCardByNumberAsync(sendToNumber);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (cardToSend == null)
            {
                ModelState.AddModelError("", "Invalid number!");
                return View(model);
            }

            try
            {
                await transactionService.SendAsync(model.Amount, card.Id, cardToSend.Id);
                return RedirectToAction("Card", "Card");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
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
    }

}
