using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.BaseClasses;
using TechChallengeAspNetCore.Data;
using TechChallengeAspNetCore.Data.Migrations.Seeders;

namespace TechChallengeAspNetCore.Tests.Integration.Migrations
{
    [DbMigratorExport(Environments.INTEGRATIONTEST)]
    public class IntegrationTestDbMigrator : MigratorBase<TechChallengeAspNetCoreDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

		[ImportingConstructor]
        public IntegrationTestDbMigrator(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString.Value)
        {
        }

        protected override void Seed(TechChallengeAspNetCoreDb context)
        {
            var dbName = context.Database.GetDbConnection().Database;

            Console.WriteLine($"Deleting {dbName}..");
            context.Database.EnsureDeleted();

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
