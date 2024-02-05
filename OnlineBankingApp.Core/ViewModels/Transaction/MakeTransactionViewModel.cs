using OnlineBankingApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.ViewModels.Transaction
{
    public class MakeTransactionViewModel
    {
        public MakeTransactionViewModel()
        {
            Type = new List<TransactionType>(); 
        }
        public double Amount { get; set; }

        //?
        public string? SendToNumber { get; set; }

        public int CardId{ get; set; }

		public int TypeId { get; set; }

		public IEnumerable<TransactionType> Type { get; set; }
    }
}
