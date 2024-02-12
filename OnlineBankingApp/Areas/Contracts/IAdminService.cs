using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Areas.Admin.Models;
using OnlineBankingApp.Infrastructure.Data.Models;

namespace OnlineBankingApp.Areas.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<IEnumerable<CardViewModel>> GetAllCardsAsync();
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<Card> GetCardAsync(int cardId);
        Task DeleteUser(string userId);
		Task DeleteCard(int cardId);
        Task DeleteCardsOfUser(string userId);

        


    }
}