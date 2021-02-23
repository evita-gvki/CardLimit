using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLimit.Core.Model;

namespace CardLimit.Core.Services.Options
{
    public class RequestOptions
    {
        public TransactionType TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public string CardId { get; set; }
    }
}
