using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Infrastructure.Data.Models
{
    public class TransactionType
    {
        public TransactionType()
        {
            Transactions = new List<Transaction>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
