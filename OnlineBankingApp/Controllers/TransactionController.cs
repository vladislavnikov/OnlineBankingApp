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
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITransactionService transactionService;

        public TransactionController(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, ITransactionService _transactionService)
        {
            this.context = _context;
            this.userManager = _userManager;
            this.transactionService = _transactionService;

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
            var user = await userManager.GetUserAsync(User);

            var card = await context.Cards.FirstOrDefaultAsync(f => f.UserId == user.Id);

            

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

            var card = await context.Cards.FirstOrDefaultAsync(f => f.UserId == user.Id);

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

            var card = await context.Cards.FirstOrDefaultAsync(c => c.UserId == user.Id);
            var cardToSend = await context.Cards.FirstOrDefaultAsync(c => c.Number == sendToNumber);

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
    }
}
