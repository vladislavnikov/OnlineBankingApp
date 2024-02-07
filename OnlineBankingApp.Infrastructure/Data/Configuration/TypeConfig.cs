using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBankingApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Infrastructure.Data.Configuration
{
    public class TypeConfig : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasData(CreateStatusOptions());
        }
        private List<TransactionType> CreateStatusOptions()
        {
            List<TransactionType> statuses = new List<TransactionType>()
            {
                new TransactionType()
                {
                    Id = 1,
                    Name = "Deposit"
                },

                new TransactionType()
                {
                    Id = 2,
                    Name = "Withdraw"
                },

                new TransactionType()
                {
                    Id = 3,
                    Name = "Send"
                },
                 new TransactionType()
                {
                    Id = 4,
                    Name = "SendToMe"
                }
             };

            return statuses;
        }


    }
}
