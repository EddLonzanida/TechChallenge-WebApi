using TechChallengeAspNetCore.Business.Common.Entities;
using Eml.DataRepository;
	
namespace TechChallengeAspNetCore.Data.Migrations.Seeders
{
    public static class RaceSeeder
    {
        public static void Seed(TechChallengeAspNetCoreDb context, string relativePath)
        {
            Seeder.Execute("Races", () =>
            {
                var intialData = Seeder.GetJsonStubs<Race>("races", relativePath);

                context.Races.AddRange(intialData);
                context.SaveChanges();
            });
        }
    }
}
