using TechChallengeAspNetCore.Business.Common.Entities;
using  TechChallengeAspNetCore.Data.Migrations.Utils;
	
namespace TechChallengeAspNetCore.Data.Migrations.Data
{
    public static class RaceData
    {
        public static void Seed(TechChallengeAspNetCoreDb context, string relativePath)
        {
            SeedData.Execute("Races", () =>
            {
                var intialData = Eml.DataRepository.Seed.GetStubs<Race>("races", relativePath);

                context.Races.AddRange(intialData);
                context.SaveChanges();
            });
        }
    }
}

