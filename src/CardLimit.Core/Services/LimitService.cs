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
        private ILimitService _limit;

        public LimitService(CardDbContext dbContext, ICardService card, ILimitService limit)
        {
            _dbContext = dbContext;
            _card = card;
            _limit = limit;
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

            if (options.TransactionDate != DateTimeOffset.Now.Date)
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "Date is not current",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if (options.TransactionType == 0)
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "TransactionType is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            var q = await _dbContext.Set<Limit>()
                .Where(l => l.CardId == options.CardId)
                .Where(l => l.TransactionDate == options.TransactionDate)
                .Where(l => l.TransactionType == options.TransactionType)
                .SingleOrDefaultAsync();

            if (q == null)
            {
                return new Result<Limit>()
                {
                    Data = null,
                    Code = Constants.ResultCode.NotFound
                };

            }

            return new Result<Limit>()
            {
                Data = q,
                Code = Constants.ResultCode.Success
            };

        }



        //Init  limit
        public async Task<Result<Limit>> InitLimitAsync(CreateLimitOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.CardId))
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if (options.TransactionDate != DateTimeOffset.Now.Date)
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "Date is not current",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if (options.TransactionType == 0)
            {
                return new Result<Limit>()
                {
                    ErrorMessage = "TransactionType is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            var q = new Limit()
            {
                TransactionDate = options.TransactionDate,
                TransactionType = options.TransactionType,
                CardId = options.CardId,
                AggregateAmount = 0M,
            };

            _dbContext.Add(q);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new Result<Limit>()
                {
                    Code = Constants.ResultCode.InternalServerError,
                    ErrorMessage = "limit could not be saved"
                };
            }

            return new Result<Limit>()
            {
                Code = Constants.ResultCode.Success,
                Data = q
            };

        }
    }
}

    

    



