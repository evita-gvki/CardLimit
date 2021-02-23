using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLimit.Core.Config
{
    public class AppConfig
    {
        public string CardConnectionString { get; set; }
        public string MinLoggingLevel { get; set; }
        public ClientConfig ClientConfig { get; set; }
    }
}
