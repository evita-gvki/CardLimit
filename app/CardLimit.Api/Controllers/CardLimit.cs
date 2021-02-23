using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardLimit.Core.Services;
using CardLimit.Core.Services.Options;

namespace CardLimit.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardLimit : Controller
    {
        
        private readonly ILogger<CardLimit> _logger;
        private readonly ICardService _cards;
        private readonly ILimitService _limits;

        public CardLimit(ILogger<CardLimit> logger, ICardService cards, ILimitService limits)
        {
            _logger = logger;
            _cards = cards;
            _limits = limits;
        }

        [HttpGet]
        public IActionResult FindLimits([FromBody] string CardId)
        {
            var limits = _cards.FindLimit2Async(CardId).Result.Data;
            return Json(limits);
        }

        [HttpPost]
        public IActionResult AuthRequest(
            [FromBody] RequestOptions options)
        {
            var card = _cards.AuthRequest(options).Result.Data;

            return Json(card);
        }

    }
}

