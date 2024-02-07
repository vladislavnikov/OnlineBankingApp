using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public int TypeId { get; set; }
    }
}
