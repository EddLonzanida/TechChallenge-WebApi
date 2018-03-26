using System;

namespace TechChallengeAspNetCore.Data.Migrations.Utils
{
    public static class SeedData
    {
        public static void Execute(string tableName, Action action)
        {
            Console.WriteLine($"Seeding {tableName}..");

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to seed {tableName}. {ex.Message}");
                Console.WriteLine(ex.InnerException);
                throw;
            }
        }
	}
}

