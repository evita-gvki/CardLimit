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
        private ICardService _card;
        private ILimitService _limit;

        public CardService(CardDbContext dbContext, ICardService card, ILimitService limit)
        {
            _dbContext = dbContext;
            _card = card;
            _limit= limit;
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
                .Where(c=> c.CardId == CardId)
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

       
        public async Task<Result<Card>> AuthRequest(RequestOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.CardId))
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if(options.TransactionType ==0)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            if(options.TransactionAmount==0)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "CardId is required",
                    Code = Constants.ResultCode.BadRequest
                };
            }

            var avail = _card.FindAvailableBalance(options.CardId).Result.Data;
            if (options.TransactionAmount != avail.AvailableBalance)
            {
                return new Result<Card>()
                {
                    ErrorMessage = "Transaction cancelled",
                    Code = Constants.ResultCode.BadRequest
                };
            }
                var opt = new FindLimitOptions()
                {
                    CardId = options.CardId,
                    TransactionDate = DateTimeOffset.Now.Date,
                    TransactionType = options.TransactionType
                };
                var lim = _limit.FindLimitAsync(opt);
            if (lim == null)
            {
                var opt2 = new CreateLimitOptions()
                {
                    CardId = options.CardId,
                    TransactionType = options.TransactionType,
                    TransactionDate = DateTimeOffset.Now.Date
                };

                lim = _limit.InitLimitAsync(opt2);
                var limit = lim.Result.Data;


            }
              //  if (lim.Result.Data.Transaction == )

                    return new Result<Card>()
                    {
                        Code = Constants.ResultCode.Success,
                      
                    };

            }



        }




    }


