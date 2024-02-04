using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Infrastructure.Data.Configuration;
using OnlineBankingApp.Infrastructure.Data.Models;

namespace OnlineBankingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TypeConfig());

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> Users { get; set; } = null!;

        public DbSet<Card> Cards { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        public DbSet<TransactionType> TransactionTypes { get; set; } = null!;
    }
}
