using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.ViewModels.User;
using OnlineBankingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Services
{
	public class UserService : IUserService
	{

		private readonly ApplicationDbContext context;

		public UserService(ApplicationDbContext _context)
		{
			context = _context;
		}

        public async void EditUserAsync(string userId, EditUserViewModel model)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Email = model.Email;

			await context.SaveChangesAsync();
        }

        public async Task<UserViewModel> GetUserAsync(string userId)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			var model = new UserViewModel()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email
			};

			return model;
		}
	}
}
