using System;
using CardLimit.Core;
using CardLimit.Core.Data;
using CardLimit.Core.Model;
using CardLimit.Core.Services.Options;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CardLimit.Core.Services
{
    public class LimitService : ILimitService
    {
        private CardDbContext _dbContext;
        private ICardService _card;
        //ivate ILimitService _limit;

        public LimitService(CardDbContext dbContext, ICardService card)
        {
            _dbContext = dbContext;
            _card = card;
            //imit = limit;
        }

        public async Task<Result<Limit>> FindLimitAsync(FindLimitOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.CardId))
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            //if (options.TransactionDate != DateTimeOffset.Now.Date)
            //{
            //    return new Result<Limit>()
            //    {
            //        ErrorMessage = "Date is not current",
            //        Code = Constants.ResultCode.BadRequest
            //    };
            //}

            if (options.TransactionType == 0)
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "TransactionType is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            var q = _dbContext.Set<Limit>()
                .Where(l => l.CardId == options.CardId)
                 //.Where(l => l.TransactionDate == DateTimeOffset.Now.Date)
                 .Where(l => l.TransactionType == options.TransactionType)
                .SingleOrDefault();
            //.ToListAsync();

            if (q == null)
            {
                return new Result<Limit>()
                {
                    Data = null,
                    Code = Constants.ResultCode.NotFound
                };

            }

            else return new Result<Limit>()
            {
                Data = q,
                Code = Constants.ResultCode.Success
            };

        }



        ////Init  limit
        //public async Task<Result<Limit>> InitLimitAsync(CreateLimitOptions options)
        //{
        //    if (string.IsNullOrWhiteSpace(options.CardId))
        //    {
        //        return new Result<Limit>()
        //        {
        //            ErrorMessage = "CardId is required",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    if (options.TransactionDate != DateTimeOffset.Now.Date)
        //    {
        //        return new Result<Limit>()
        //        {
        //            ErrorMessage = "Date is not current",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    if (options.TransactionType == 0)
        //    {
        //        return new Result<Limit>()
        //        {
        //            ErrorMessage = "TransactionType is required",
        //            Code = Constants.ResultCode.BadRequest
        //        };
        //    }

        //    var q = new Limit()
        //    {
        //        TransactionDate = options.TransactionDate,
        //        TransactionType = options.TransactionType,
        //        CardId = options.CardId,
        //        AggregateAmount = 0M,
        //    };

        //    _dbContext.Add(q);
        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        return new Result<Limit>()
        //        {
        //            Code = Constants.ResultCode.InternalServerError,
        //            ErrorMessage = "limit could not be saved"
        //        };
        //    }

        //    return new Result<Limit>()
        //    {
        //        Code = Constants.ResultCode.Success,
        //        Data = q
        //    };

        //}

        public async Task<Result<Card>> AuthRequest2(RequestOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.CardId))
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if (options.TransactionType == 0)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if (options.TransactionAmount == 0)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }


            //Check if card exists
            var card1 = _card.FindAvailableBalance(options.CardId).Result.Data;
            if (card1 == null)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is not found",
                    Code = Constants.ResultCode.BadRequest
                };

            }

            if (card1.Limits.Count == 0)


            ////check if card has limits, else add new limits
            //var findopt = new FindLimitOptions()
            //{
            //    CardId = options.CardId,
            //    TransactionType = options.TransactionType
            //    //TransactionDate = DateTimeOffset.Now.Date
            //};

            //var findlimit = FindLimitAsync(findopt).Result.Data;
            //if (findlimit == null)
            {
                //add limit for CardPresent
                var q = new Limit()
                {
                    TransactionDate = DateTimeOffset.Now.Date,
                    TransactionType = TransactionType.CardPresent,
                    CardId = options.CardId,
                    AggregateAmount = 0M,
                };

                card1.Limits.Add(q);
                _dbContext.Add(q);
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return new Result<Card>()
                    {
                        Code = Constants.ResultCode.InternalServerError,
                        ErrorMessage = "limit could not be saved"
                    };
                }

                //add limit for ecommerce
                var p = new Limit()
                {
                    TransactionDate = DateTimeOffset.Now.Date,
                    TransactionType = TransactionType.Ecommerce,
                    CardId = options.CardId,
                    AggregateAmount = 0M,
                };

                card1.Limits.Add(p);
                _dbContext.Add(p);
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return new Result<Card>()
                    {
                        Code = Constants.ResultCode.InternalServerError,
                        ErrorMessage = "limit could not be saved"
                    };
                }

            }


            //check available balance
            if (options.TransactionAmount > card1.AvailableBalance)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "Transaction cancelled",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            //check limits
            var lim3 = new Limit();

            lim3 = card1.Limits
                .Where(c => c.TransactionType == options.TransactionType)
                .Where(c => c.TransactionDate == DateTimeOffset.Now.Date)
                 .SingleOrDefault();

            if ((options.TransactionType.Equals(TransactionType.CardPresent)) & (lim3.AggregateAmount + options.TransactionAmount > 1500M))
            //{
            //    lim3 =  _dbContext.Set<Limit>()
            //     .Where(c => c.CardId == options.CardId)
            //     .Where(c => c.TransactionType == options.TransactionType)
            //     .Where(c => c.TransactionDate == DateTimeOffset.Now.Date)
            //     .SingleOrDefault();
            //    if (lim3.AggregateAmount + options.TransactionAmount >= 1500M)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "Transaction cancelled",
                    Code = Constants.ResultCode.BadRequest
                };

            }
            else if (options.TransactionType.Equals(TransactionType.CardPresent))
                    { 
        lim3.AggregateAmount += options.TransactionAmount;
            _dbContext.SaveChanges();

        }

        if ((options.TransactionType.Equals(TransactionType.Ecommerce)) & (lim3.AggregateAmount + options.TransactionAmount > 500M))
        

        //if (options.TransactionType.Equals(TransactionType.Ecommerce))
        //{
        //     lim3 = await _dbContext.Set<Limit>()
        //        .Where(c => c.CardId == options.CardId)
        //        .Where(c => c.TransactionType == options.TransactionType)
        //        .Where(c => c.TransactionDate == DateTimeOffset.Now.Date)
        //        .SingleOrDefaultAsync();
        //    if (lim3.AggregateAmount + options.TransactionAmount >= 500M)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "Transaction cancelled",
                    Code = Constants.ResultCode.BadRequest
    };

}
            else if (options.TransactionType.Equals(TransactionType.Ecommerce))
             {
                lim3.AggregateAmount += options.TransactionAmount;
                _dbContext.SaveChanges();
             }

        card1.UpdateAvBal(options.TransactionAmount);
            _dbContext.SaveChanges();


          
            return new Result<Card>()
            {
                Code = Constants.ResultCode.Success,
                Data = card1
            };
        }
    }
}

    

    



