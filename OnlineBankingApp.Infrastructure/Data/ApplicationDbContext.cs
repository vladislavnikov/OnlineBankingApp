using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Infrastructure.Data.Configuration;
using OnlineBankingApp.Infrastructure.Data.Models;
using OnlineBankingApp.Infrastructure.Data.Models.Role;

namespace OnlineBankingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TypeConfig());
            builder.ApplyConfiguration(new UserConfig());

            builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole { Id = "a1", Name = "Admin", NormalizedName = "ADMIN" });

            builder.Entity<ApplicationUser>()
                .HasMany(f => f.Cards)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Card>()
                .HasMany(f => f.Transactions)
                .WithOne(h => h.Card)
                .HasForeignKey(h => h.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionType> TransactionTypes { get; set; } = null!;
    }
}
