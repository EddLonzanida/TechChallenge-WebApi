using System.Linq;
using Eml.ConfigParser.Helpers;
using Microsoft.EntityFrameworkCore;
using Eml.DataRepository;
using TechChallengeAspNetCore.Business.Common.Entities;

namespace TechChallengeAspNetCore.Data
{
    public class TechChallengeAspNetCoreDb : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Race> Races { get; set; }

        public DbSet<Horse> Horses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ConfigBuilder.GetConfiguration();
            var mainDbConnectionString = new MainDbConnectionString(config);

            optionsBuilder.UseSqlServer(mainDbConnectionString.Value);
        }

        private bool allowIdentityInsertWhenSeeding { get; set; } = true;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (allowIdentityInsertWhenSeeding)
            {
                foreach (var pb in modelBuilder.Model
                    .GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.Name.Equals("Id"))
                    .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name)))
                {
                    pb.ValueGeneratedNever();
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
