﻿using OnlineBankingApp.Core.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.ViewModels.Card
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}