using TechChallengeAspNetCore.Business.Common.Entities;
using Eml.DataRepository;

namespace TechChallengeAspNetCore.Data.Migrations.Seeders
{
    public static class CustomerSeeder
    {
        public static void Seed(TechChallengeAspNetCoreDb context, string relativePath)
        {
            Seeder.Execute("Customers", () =>
            {
                var intialData = Seeder.GetJsonStubs<Customer>("customers", relativePath);

                context.Customers.AddRange(intialData);
                context.SaveChanges();
            });
        }
    }
}
