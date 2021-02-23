using System;
using System.Collections.Generic;
using System.Text;
using CardLimit.Core.Services;
using CardLimit.Core.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardLimit.MyTest
{

    public class CardLimitFixture : IDisposable
    {
        public IServiceScope Scope { get; private set; }

        public CardLimitFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Initialize Dependency container
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAppServices(config);

            Scope = serviceCollection
                .BuildServiceProvider()
                .CreateScope();
        }

        public void Dispose()
        {
            Scope.Dispose();
        }

    }
}
