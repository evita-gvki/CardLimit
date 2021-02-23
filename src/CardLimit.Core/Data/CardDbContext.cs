using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CardLimit.Core.Model;

namespace CardLimit.Core.Data
{
    public class CardDbContext : DbContext
    {
    

        public CardDbContext(
          DbContextOptions options) : base(options)
        { }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Card>()
                .ToTable("Card");

            modelBuilder.Entity<Card>()
                 .HasIndex(c => c.CardId)
                 .IsUnique();

            modelBuilder.Entity<Limit>()
                .ToTable("Limit");

           
        }
    }
}

