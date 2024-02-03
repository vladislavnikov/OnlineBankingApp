using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Infrastructure.Data.Models
{
    public  class Card
    {
        public Card()
        {
            Transactions = new List<Transaction>();
        }

        [Key]
        public int Id { get; set; }

        
        public string Number { get; set; }

        public double Balance { get; set; }


        [Required]
        public string UserId { get; set; } // One card belongs to one user
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public ICollection<Transaction> Transactions { get; set;}

    }
}
