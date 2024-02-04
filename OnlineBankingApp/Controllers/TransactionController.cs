using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
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
            var model = new TransactionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(TransactionViewModel model)
        {
            var user = await userManager.GetUserAsync(User);

            var card = await context.Cards.FirstOrDefaultAsync(f => f.UserId == user.Id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await transactionService.MakeTransactionAsync(model, card.Id);
                return RedirectToAction("Card","Card");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
        }


    }
}
