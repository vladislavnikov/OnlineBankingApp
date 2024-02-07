﻿using OnlineBankingApp.Core.ViewModels.Card;
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
        Task<IEnumerable<CardViewModel>> GetAllCardsAsync(string userId);
        Task<Card> GetCardAsync(string userId);
        Task<Card> GetCardAsync(int cardId);
        Task<Card> GetCardByNumberAsync(string number);
        Task<ICollection<Card>> GetUserCardsAsync(string userId);
    }
}