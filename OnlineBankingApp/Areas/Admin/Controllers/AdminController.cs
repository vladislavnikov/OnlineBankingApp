using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Areas.Contracts;
using OnlineBankingApp.Data;

namespace OnlineBankingApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {

        private readonly IAdminService adminService;
        public AdminController(IAdminService _adminService)
        {
            this.adminService = _adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Admin()
        {
            var users = await adminService.GetAllUsersAsync();
            var cards = await adminService.GetAllCardsAsync();

            ViewBag.Users = users;
            ViewBag.Cards = cards;

            return View("Admin");
        }

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			await adminService.DeleteUser(userId);

			return RedirectToAction("Admin");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCard(int cardId)
		{
			await adminService.DeleteCard(cardId);

			return RedirectToAction("Admin");
		}
	}
}
