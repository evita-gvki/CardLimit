using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CardLimit.Core.Config.Extensions
{
    public static class ConfigurationExtensions
    {
        public static AppConfig ReadAppConfiguration(
            this IConfiguration @this)
        {
            var minLoggingLevel = @this.GetSection("MinLoggingLevel").Value;
            var connectionString = @this.GetConnectionString("CardDatabase");

            var clientId = @this.GetSection("ClientConfig")
                .GetSection("clientId").Value;

            var clientSecret = @this.GetSection("ClientConfig")
                .GetSection("clientSecret").Value;

            return new AppConfig()
            {
                CardConnectionString = connectionString,
                MinLoggingLevel = minLoggingLevel,
                ClientConfig = new ClientConfig()
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                }
            };
        }
    }
}
