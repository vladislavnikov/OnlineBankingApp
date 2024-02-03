using OnlineBankingApp.Core.ViewModels.Card;
using OnlineBankingApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.Contracts
{
    public interface ICardService
    {
        Task CreateCardAsync(string userId);
    }
}
