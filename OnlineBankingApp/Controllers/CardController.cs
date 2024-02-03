using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.Services;
using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;

namespace OnlineBankingApp.Controllers
{
    public class CardController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICardService cardService;

        public CardController(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, ICardService _cardService)
        {
            this.context = _context;
            this.userManager = _userManager;
            this.cardService = _cardService;

        }

        [HttpGet]
        public async Task<IActionResult> Card()
        {
            var user = await userManager.GetUserAsync(User);
            var card = await context.Cards.FirstOrDefaultAsync(c => c.UserId == user.Id);

            var model = new CardViewModel();

            if (card == null)
            {
                await cardService.CreateCardAsync(user.Id);
                context.SaveChanges();

                card = await context.Cards.FirstOrDefaultAsync(c => c.UserId == user.Id);
            }

            model = new CardViewModel()
            {
                Balance = card.Balance,
                Number = card.Number
            };


            return View(model);
        }
    }
}
