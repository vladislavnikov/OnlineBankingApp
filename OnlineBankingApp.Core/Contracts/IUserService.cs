using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Core.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Contracts
{
    public interface IUserService
    {
		Task<UserViewModel> GetUserAsync(string userId);
	}
}
