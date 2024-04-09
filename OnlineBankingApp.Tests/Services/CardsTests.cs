// CardsServiceTests.cs
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.Services;
using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Core.ViewModels.Transaction;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankingApp.Tests.Services
{
	[TestFixture]
	public class CardsServiceTests
	{
		private IEnumerable<Card> cardList;
		private ApplicationDbContext context;
		private ApplicationUser user;

		[SetUp]
		public void TestInitialize()
		{
			this.cardList = new List<Card>()
			{
				new Card() { Id = 1, Number = "1111111111111111", Balance = 100, UserId = "u1" },
				new Card() { Id = 2, Number = "2222222222222222", Balance = 200, UserId = "u1" },
				new Card() { Id = 3, Number = "3333333333333333", Balance = 300, UserId = "u1" }
			};

			this.user = new ApplicationUser()
			{
				Id = "u1",
				FirstName = "User",
				LastName = "Test",
				Email = "test@mail.com",
				Cards = new List<Card>()
			};

			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "BankDb")
				.Options;

			this.context = new ApplicationDbContext(options);
			this.context.AddRangeAsync(this.cardList);
			this.context.AddRangeAsync(this.user);
			this.context.SaveChangesAsync();
		}

		[Test]
		public async Task CreateCardAsync_ShouldCreateNewCardForUser()
		{
			// Arrange
			string userId = "u1";

			ICardRepository service = new CardRepository(context);

			// Act
			int newCardId = await service.CreateCardAsync(userId);
			await context.SaveChangesAsync();

			// Assert
			var userCards = context.Cards.Where(c => c.UserId == userId).ToList();
			Assert.AreEqual(4, userCards.Count);
			Assert.True(userCards.Any(c => c.Id == newCardId));
		}

		[Test]
		public async Task GetAllCardsAsync_ShouldReturnAllCardsForUser()
		{
			// Arrange
			string userId = "u1";

			ICardRepository service = new CardRepository(context);

			// Act
			var result = await service.GetAllCardsAsync(userId);
			await context.SaveChangesAsync();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count()); 
		}

		[Test]
		public async Task GetCardAsync_ShouldReturnCardForUser()
		{
			// Arrange
			string userId = "u1";

			ICardRepository service = new CardRepository(context);

			// Act
			var result = await service.GetCardAsync(userId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("1111111111111111", result.Number);
		}

		[Test]
		public async Task GetCardByNumberAsync_ShouldReturnCardByNumber()
		{
			// Arrange
			string cardNumber = "1111111111111111";

			ICardRepository service = new CardRepository(context);

			// Act
			var result = await service.GetCardByNumberAsync(cardNumber);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(cardNumber, result.Number);
		}
	}
}
