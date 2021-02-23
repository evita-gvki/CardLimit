using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLimit.Core.Model;
using CardLimit.Core.Services.Options;



namespace CardLimit.Core.Services
{
    public interface ICardService 
    {
        public Task<Result<Card>> FindAvailableBalance(string cardId);
        public Task<Result<Card>> AuthRequest(RequestOptions options);
        public Task<Result<Card>> FindLimit2Async(string CardId);
    }
}
