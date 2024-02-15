using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Core.Contracts;
using OnlineBankingApp.Core.Services;
using OnlineBankingApp.Data;
using OnlineBankingApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Tests.Services
{
	[TestFixture]
	public class TransactionsTests
	{
		private IEnumerable<Transaction> transList;
		private ApplicationDbContext context;
		private ApplicationUser user;
		private Card card1;
		private Card card2;

		[SetUp]
		public void TestInitialize()
		{
			this.transList = new List<Transaction>()
		 {
		  new Transaction(){Id=1, Type = "Deposit", TransactionTypeId = 1, Amount = 50, CardId = 1 },
		  new Transaction(){Id=2,  Type = "Sent", TransactionTypeId = 3, Amount = 60, CardId = 1 },
		  new Transaction(){Id=3,  Type = "Withdraw", TransactionTypeId = 2, Amount = 70, CardId = 1 }
		 };


			this.user = new ApplicationUser()
			{
				Id = "u1",
				FirstName = "User",
				LastName = "Test",
				Email = "test@mail.com",
				Cards = new List<Card>()
			};

			this.card1 = new Card()
			{
				Id = 1,
				Number = "1111222233334444",
				Balance = 50,
				UserId = "u1",
				Transactions = new List<Transaction>()
			};

			this.card2 = new Card()
			{
				Id = 2,
				Number = "2111222233334444",
				Balance = 0,
				UserId = "u1",
				Transactions = new List<Transaction>()
			};

			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "BankDb")
				.Options;

			this.context = new ApplicationDbContext(options);
			this.context.AddRangeAsync(this.transList);
			this.context.AddRangeAsync(this.user);
			this.context.AddRangeAsync(this.card1);
			this.context.AddRangeAsync(this.card2);
			this.context.SaveChangesAsync();
		}

		[Test]
		public async Task WithdrawFromCard_ShouldGetMoneyFromCard()
		{
			// Arrange
			int cardId = 1;
			double amount = 50;

			ITransactionService service = new TransactionService(context);

			// Act
			await service.WithdrawAsync(amount, cardId);

			// Assert
			var dbCard = context.Cards.FirstOrDefault(c => c.Id == cardId);

			// Assert
			Assert.NotNull(dbCard, "Card not found");
			Assert.AreEqual(0, dbCard.Balance, "Card balance should be zero after withdrawal");
		}

		[Test]
		public async Task DepositToCard_ShouldAddMoreToCard()
		{
			// Arrange
			int cardId = 1;
			double amount = 50;

			ITransactionService service = new TransactionService(context);

			// Act
			await service.DepositAsync(amount, cardId);

			// Assert
			var dbCard = context.Cards.FirstOrDefault(c => c.Id == cardId);

			// Assert
			Assert.NotNull(dbCard, "Card not found");
			Assert.AreEqual(100, dbCard.Balance, "Card balance should be more after deposti");
		}

		[Test]
		public async Task GetAllTransactionsAsync_ShouldGiveAllTransacations()
		{
			// Arrange
			List<int> cardIds = new List<int>();
			cardIds.Add(1);
			cardIds.Add(2);
			cardIds.Add(3);

			ITransactionService service = new TransactionService(context);

			// Act
			var dbCard =await service.GetAllTransactionsAsync(cardIds);

			// Assert
			Assert.AreEqual(3, dbCard.Count());
		}

		[Test]
		public async Task SendAsync_ShouldSendMoneFromOneCardToAnother()
		{
			// Arrange
			int cardId1 = 1;
			int cardId2 = 2;

			ITransactionService service = new TransactionService(context);

			// Act
			await service.SendAsync(50,cardId1,cardId2);
			var card = await context.Cards.FirstOrDefaultAsync(c => c.Id == cardId2);

			// Assert
			Assert.AreEqual(50, card.Balance);
		}

	}
}
