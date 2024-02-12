using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Areas.Admin.Models;
using OnlineBankingApp.Areas.Contracts;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;

namespace OnlineBankingApp.Areas.Service
{
	public class AdminService : IAdminService
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public AdminService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
		{
			context = _context;
			this.userManager = _userManager;
		}


		public async Task<IEnumerable<CardViewModel>> GetAllCardsAsync()
		{
			var cards = await context.Cards.ToListAsync();

			var model = new List<CardViewModel>();

			foreach (var card in cards)
			{
				var uvm = new CardViewModel()
				{
					Id = card.Id,
					Number = card.Number,
					Balance = card.Balance,
					UserId = card.UserId
				};

				model.Add(uvm);
			}

			return model;
		}

		public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
		{
			var users = await context.Users.ToListAsync();
			var model = new List<UserViewModel>();

			foreach (var user in users)
			{
				var isInRole = await userManager.IsInRoleAsync(user, "Admin");

				if (isInRole)
				{
					continue;
				}

				var uvm = new UserViewModel()
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					Cards = await NumberCardsOfUserAsync(user.Id)
				};
			model.Add(uvm);
			}

			return model;
		}

		public async Task<int> NumberCardsOfUserAsync(string userId)
		{
			var cards = await context.Cards.Where(c => c.UserId == userId).ToListAsync();

			return cards.Count();
		}


		public async Task<Card> GetCardAsync(int cardId)
		{
			var card = await context.Cards.FirstOrDefaultAsync(c => c.Id == cardId);

			return card;
		}

		public async Task<ApplicationUser> GetUserAsync(string userId)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			return user;
		}
		public async Task DeleteCard(int cardId)
		{
			var card = await GetCardAsync(cardId);

			context.Cards.Remove(card);
			await context.SaveChangesAsync();
		}

		public async Task DeleteUser(string userId)
		{
			var user = await GetUserAsync(userId);

			await DeleteCardsOfUser(userId);
			context.Users.Remove(user);
			await context.SaveChangesAsync();
		}

		public async Task DeleteCardsOfUser(string userId)
		{
			var cards = await context.Cards.Where(c => c.UserId == userId).ToListAsync();

			foreach (var card in cards)
			{
				context.Cards.Remove(card);
			}
			await context.SaveChangesAsync();
		}
	}
}
