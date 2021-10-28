using System;
using CardLimit.Core;
using CardLimit.Core.Data;
using CardLimit.Core.Model;

using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CardLimit.Core.Services.Options;


namespace CardLimit.Core.Services
{
    public class CardService : ICardService
    {

        private CardDbContext _dbContext;
        //ivate ICardService _card;
        //private ILimitService _limit;

        public CardService(CardDbContext dbContext)
        {
            _dbContext = dbContext;
            
         }

        public async Task<Result<Card>> FindLimit2Async(string CardId)
        {
            if (string.IsNullOrWhiteSpace(CardId))
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            var q = await _dbContext.Set<Card>()
                .Where(c => c.CardId == CardId)
                .Include(l => l.Limits)
                .SingleOrDefaultAsync();

            if (q == null)
            {
                return new Result<Card>()
                {
                    Data = null,
                    Code = Constants.ResultCode.NotFound
                };

            }

            return new Result<Card>()
            {
                Data = q,
                Code = Constants.ResultCode.Success
            };
        }


        //Find available card balance
        public async Task<Result<Card>> FindAvailableBalance(string cardId)
        {
            if (string.IsNullOrWhiteSpace(cardId))
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            var q = await _dbContext.Set<Card>()
                        .Where(c => c.CardId == cardId)
                        .Include(l=>l.Limits)
                        .SingleOrDefaultAsync();

            if (q == null)
            {
                return new Result<Card>()
                {
                    Data = null,
                    Code = Constants.ResultCode.NotFound
                };

            }

            return new Result<Card>()
            {
                Data = q,
                Code = Constants.ResultCode.Success
            };

        }


        //public async Task<Result<Card>> AuthRequest(RequestOptions options)
        //{
        //    if (string.IsNullOrWhiteSpace(options.CardId))
        //    {
        //        return new Result<Card>()
        //        {
        //            ErrorMessage = "CardId is required",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    if (options.TransactionType == 0)
        //    {
        //        return new Result<Card>()
        //        {
        //            ErrorMessage = "CardId is required",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    if (options.TransactionAmount == 0)
        //    {
        //        return new Result<Card>()
        //        {
        //            ErrorMessage = "CardId is required",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    var avail = _card.FindLimit2Async(options.CardId).Result.Data;
        //    if (options.TransactionAmount != avail.AvailableBalance)
        //    {
        //        return new Result<Card>()
        //        {
        //            ErrorMessage = "Transaction cancelled",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    if (avail.Limits == null)
        //    {
        //        var opt1 = new CreateLimitOptions()
        //        {
        //            CardId = options.CardId,
        //            TransactionType = TransactionType.CardPresent,
        //            TransactionDate = DateTimeOffset.Now.Date,
        //            AggregateAmount = 0M

        //        };

        //        var lim1 = _limit.InitLimitAsync(opt1);
        //        var opt2 = new CreateLimitOptions()
        //        {
        //            CardId = options.CardId,
        //            TransactionType = TransactionType.CardPresent,
        //            TransactionDate = DateTimeOffset.Now.Date,
        //            AggregateAmount = 0M

        //        };

        //        var lim2 = _limit.InitLimitAsync(opt1);
        //    }


        //    if (options.TransactionType.Equals(TransactionType.CardPresent))
        //    {
        //        var lim3 = await _dbContext.Set<Limit>()
        //          .Where(c => c.CardId == options.CardId)
        //          .Where(c => c.TransactionType == options.TransactionType)
        //          .Where(c => c.TransactionDate == DateTimeOffset.Now.Date)
        //          .SingleOrDefaultAsync();
        //        if (lim3.AggregateAmount + options.TransactionAmount >= 1500M)
        //        {
        //            return new Result<Card>()
        //            {
        //                ErrorMessage = "Transaction cancelled",
        //                Code = Constants.ResultCode.BadRequest
        //            };

        //        }
        //        else lim3.AggregateAmount += options.TransactionAmount;
        //        _dbContext.SaveChanges();
        //    }

        //    if (options.TransactionType.Equals(TransactionType.Ecommerce))
        //    {
        //        var lim3 = await _dbContext.Set<Limit>()
        //            .Where(c => c.CardId == options.CardId)
        //            .Where(c => c.TransactionType == options.TransactionType)
        //            .Where(c => c.TransactionDate == DateTimeOffset.Now.Date)
        //            .SingleOrDefaultAsync();
        //        if (lim3.AggregateAmount + options.TransactionAmount >= 500M)
        //        {
        //            return new Result<Card>()
        //            {
        //                ErrorMessage = "Transaction cancelled",
        //                Code = Constants.ResultCode.BadRequest
        //            };

        //        }
        //        else lim3.AggregateAmount += options.TransactionAmount;
        //        _dbContext.SaveChanges();
        //    }

        //    var card = _card.FindLimit2Async(options.CardId).Result.Data;
        //    card.AvailableBalance -= options.TransactionAmount;
        //    _dbContext.SaveChanges();

        //    return new Result<Card>()
        //    {
        //        Code = Constants.ResultCode.Success,
        //        Data = card
        //    };
        //}
    }
}



