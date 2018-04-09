using TechChallengeAspNetCore.Business.Common.Entities;
using Eml.DataRepository;

namespace TechChallengeAspNetCore.Data.Migrations.Seeders
{
    public static class BetSeeder
    {
        public static void Seed(TechChallengeAspNetCoreDb context, string relativePath)
        {
            Seeder.Execute("Bets", () =>
            {
                var intialData = Seeder.GetJsonStubs<Bet>("bets", relativePath);

                context.Bets.AddRange(intialData);
                context.SaveChanges();
            });
        }
    }
}
