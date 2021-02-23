using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CardLimit.Core.Data;
using CardLimit.Core.Config;
using CardLimit.Core.Config.Extensions;

namespace CardLimit.Core.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(
            this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddSingleton<AppConfig>(
                configuration.ReadAppConfiguration());

            @this.AddDbContext<CardDbContext>(
                 (serviceProvider, optionsBuilder) => {
                     var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                     optionsBuilder.UseSqlServer(appConfig.CardConnectionString);
                 });

            @this.AddScoped<ICardService, CardService>();
            @this.AddScoped<ILimitService, LimitService>();
           
        }
    }
}
