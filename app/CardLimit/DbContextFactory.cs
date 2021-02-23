using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CardLimit.Core.Config.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLimit.Core.Data;
using CardLimit.Core;
using CardLimit.Core.Model;

namespace CardLimit
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CardDbContext>
    {
        public CardDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<CardDbContext>();

            optionsBuilder.UseSqlServer(
                config.CardConnectionString,
                options =>
                {
                    options.MigrationsAssembly("CardLimit");
                });

            return new CardDbContext(optionsBuilder.Options);
        }
    }
}
