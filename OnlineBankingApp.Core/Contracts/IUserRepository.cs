using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Core.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Contracts
{
    public interface IUserRepository
    {
		Task<UserViewModel> GetUserAsync(string userId);
		Task EditUserAsync(string userId, EditUserViewModel model);

	}
}
