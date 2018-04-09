using System;
using System.Composition;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.BaseClasses;
using Microsoft.EntityFrameworkCore;
using TechChallengeAspNetCore.Data.Migrations.Seeders;

namespace TechChallengeAspNetCore.Data.Migrations
{
    [DbMigratorExport(Environments.PRODUCTION)]
    public class MainDbMigrator : MigratorBase<TechChallengeAspNetCoreDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

		[ImportingConstructor]
        public MainDbMigrator(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString.Value)
        {
        }

        protected override void Seed(TechChallengeAspNetCoreDb context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            // Console.WriteLine($"Deleting {dbName}..");
            // context.Database.EnsureDeleted();

            Console.WriteLine($"Creating {dbName}..");
            context.Database.EnsureCreated();

            Console.WriteLine("Running Migration..");
            context.Database.Migrate();

            Console.WriteLine("Seeding Data..");
            CustomerSeeder.Seed(context, SAMPLE_DATA_SOURCES);
            RaceSeeder.Seed(context, SAMPLE_DATA_SOURCES);
            BetSeeder.Seed(context, SAMPLE_DATA_SOURCES);
        }
    }
}
