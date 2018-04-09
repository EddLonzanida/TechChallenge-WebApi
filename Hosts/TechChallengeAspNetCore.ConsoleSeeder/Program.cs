using Eml.ConfigParser.Helpers;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.Extensions;
using Eml.Mef;
using Eml.ClassFactory.Contracts;
using System;
using System.Composition.Hosting.Core;
using System.Composition.Hosting;

namespace TechChallengeAspNetCore.ConsoleSeeder
{
    public class Program
    {
        private const string APP_PREFIX = "TechChallengeAspNetCore";

        private const string DB_DIRECTORY = @"DataBase";

        private static IClassFactory classFactory;

        static void Main(string[] args)
        {
            var configuration = ConfigBuilder.GetConfiguration();

            ExportDescriptorProvider instanceRegistration(ContainerConfiguration r) => r.WithInstance(configuration);
            classFactory = Bootstrapper.Init(APP_PREFIX, instanceRegistration);

            var dbMigration = GetMainDbMigrator();

            try
            {
                Utils.Console.WriteLine("DestroyDb if any..");

                dbMigration.DestroyDb();
                dbMigration.CreateDb(DB_DIRECTORY);

				Utils.Console.WriteLine("Done. Press enter to exit...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

        private static IMigrator GetMainDbMigrator()
        {
            return classFactory.GetMigrator(Environments.PRODUCTION);
        }
    }
}
