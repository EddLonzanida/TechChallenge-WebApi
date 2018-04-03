using TechChallengeAspNetCore.Business.Common.Entities;
using  TechChallengeAspNetCore.Data.Migrations.Utils;

namespace TechChallengeAspNetCore.Data.Migrations.Data
{
    public static class BetData
    {
        public static void Seed(TechChallengeAspNetCoreDb context, string relativePath)
        {
            SeedData.Execute("Bets", () =>
            {
                var intialData = Eml.DataRepository.Seed.GetJsonStubs<Bet>("bets", relativePath);

                context.Bets.AddRange(intialData);
                context.SaveChanges();
            });
        }
    }
}

