using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLimit.Core.Model
{
    public class Limit
    {
        public int LimitId  { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal AggregateAmount { get; set; }
        public DateTimeOffset TransactionDate { get; set; }        
        public string CardId { get; set; }

        public Limit()
        {
             TransactionDate = DateTimeOffset.Now.Date;
        }
    }
}

