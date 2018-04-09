using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TechChallengeAspNetCore.ApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Application Started...");

                BuildWebHost(args).Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of unhandled exception.");
                throw;
            }
            finally
            {
                Startup.ClassFactory.Dispose();
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
