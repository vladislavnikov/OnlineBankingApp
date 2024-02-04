using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Infrastructure.Data.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        [ForeignKey(nameof(TransactionTypeId))]
        public TransactionType TransactionType { get; set; }

        [Required]
        public int CardId { get; set; } 
        [ForeignKey(nameof(CardId))]
        public Card Card { get; set; }
    }
}
