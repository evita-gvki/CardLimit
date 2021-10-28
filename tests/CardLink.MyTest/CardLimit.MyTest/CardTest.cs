using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;
using CardLimit.Core;
using CardLimit.Core.Model;
using CardLimit.MyTest;
using CardLimit.Core.Services;
using CardLimit.Core.Data;
using Microsoft.EntityFrameworkCore;


namespace CardLimit.MyTest
{
    
        public class CardTest : IClassFixture<CardLimitFixture>
        {
            private ICardService _cards;
            private CardDbContext _dbContext;
            private ILimitService _limits;


            public CardTest(CardLimitFixture fixture)
            {
                _cards = fixture.Scope.ServiceProvider
                    .GetRequiredService<ICardService>();
                _dbContext = fixture.Scope.ServiceProvider
                    .GetRequiredService<CardDbContext>();
                _limits = fixture.Scope.ServiceProvider
                        .GetRequiredService<ILimitService>();

            }

            [Fact]
            public void Add_Card_Success()
            {
                var card = new Card()
                {
                    CardId = "5215521552155215",
                    CardHolderName = "Mary Mariou",
                    AvailableBalance =3000M,
                    InitialBalance = 3000M
                };


                _dbContext.Add(card);
                _dbContext.SaveChanges();

                Assert.NotNull(card);

            }

        [Fact]
            public void GetCard_Success()
        {
            var cardid="5215521552155215";
            var limits = _cards.FindLimit2Async(cardid).Result.Data;

            Assert.NotNull(limits);

        }
        }
    
}
