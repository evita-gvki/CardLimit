using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLimit.Core.Model;


namespace CardLimit.Core.Services.Options
{
    public class FindLimitOptions
    {
        public TransactionType TransactionType { get; set; }
        //public DateTimeOffset TransactionDate { get; set; }
        public string CardId { get; set; }
    }
}
