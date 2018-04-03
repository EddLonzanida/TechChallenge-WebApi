using System;
using System.Composition;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.BaseClasses;
using Microsoft.EntityFrameworkCore;
using TechChallengeAspNetCore.Data.Migrations.Data;

namespace TechChallengeAspNetCore.Data.Migrations
{
    [DbMigratorExport(Environments.PRODUCTION)]
    public class MainDbMigration : MigratorBase<TechChallengeAspNetCoreDb>
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

        private const bool ALLOW_IDENTITYINSERT_WHEN_SEEDING = true;
        
		[ImportingConstructor]
        public MainDbMigration(MainDbConnectionString mainDbConnectionString) 
            :base(mainDbConnectionString.Value, ALLOW_IDENTITYINSERT_WHEN_SEEDING)
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
            CustomerData.Seed(context, SAMPLE_DATA_SOURCES);
            RaceData.Seed(context, SAMPLE_DATA_SOURCES);
            BetData.Seed(context, SAMPLE_DATA_SOURCES);
        }
    }
}
	
