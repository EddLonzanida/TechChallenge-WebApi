using TechChallengeAspNetCore.Business.Common.Entities;
using  TechChallengeAspNetCore.Data.Migrations.Utils;

namespace TechChallengeAspNetCore.Data.Migrations.Data
{
    public static class CustomerData
    {
        public static void Seed(TechChallengeAspNetCoreDb context, string relativePath)
        {
            SeedData.Execute("Customers", () =>
            {
                var intialData = Eml.DataRepository.Seed.GetJsonStubs<Customer>("customers", relativePath);

                context.Customers.AddRange(intialData);
                context.SaveChanges();
            });
        }
    }
}

