using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLimit.Core.Model
{
    public class Card
    {
        public string CardId { get; set; }
        public string CardHolderName { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal AvailableBalance  { get; set; }
        

        public List<Limit> Limits { get; set; }

        public Card()
        {
            Limits = new List<Limit>();
            AvailableBalance = InitialBalance;
         }

        public void UpdateAvBal(decimal trnamount)
        {
            AvailableBalance = AvailableBalance - trnamount;
        }
    }
}
