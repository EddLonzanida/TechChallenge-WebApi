using System;
using System.Data;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using Xunit;
using Eml.ClassFactory.Contracts;
using Eml.Mef;
using Eml.ConfigParser.Helpers;
using Eml.DataRepository.Extensions;
using Eml.DataRepository.Attributes;

namespace TechChallengeAspNetCore.Tests.Integration.BaseClasses
{
    public class IntegrationTestDbFixture : IDisposable
    {
        public const string COLLECTION_DEFINITION = "IntegrationTestDbFixture CollectionDefinition";

        public const string APP_PREFIX = "TechChallengeAspNetCore";

        private readonly IMigrator dbMigration;

        public static IClassFactory ClassFactory { get; private set; }
       
		public IntegrationTestDbFixture()
        {
            var configuration = ConfigBuilder.GetConfiguration();

            ExportDescriptorProvider instanceRegistration(ContainerConfiguration r) => r.WithInstance(configuration);
            ClassFactory = Bootstrapper.Init(APP_PREFIX, instanceRegistration);

            dbMigration = GetTestDbMigration();

            if (dbMigration == null)
            {
                throw new NoNullAllowedException("dbMigration not found..");
            }

            Console.WriteLine("DestroyDb if any..");
            dbMigration.DestroyDb();

            Console.WriteLine("CreateDb..");
            dbMigration.CreateDb();
        }

        public void Dispose()
        {
            Console.WriteLine("Detach and DestroyDb..");

            dbMigration.DestroyDb();

            var container = ClassFactory.Container;
            ClassFactory = null;
            container.Dispose();
        }

        private static IMigrator GetTestDbMigration()
        {
            return ClassFactory.GetMigrator(Environments.INTEGRATIONTEST);
        }
    }

    [CollectionDefinition(IntegrationTestDbFixture.COLLECTION_DEFINITION)]
    public class IntegrationTestDbFixtureCollectionDefinition : ICollectionFixture<IntegrationTestDbFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
