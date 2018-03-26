using TechChallengeAspNetCore.ApiHost.Controllers;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.Controllers
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void AmountsController_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<AmountsController>();

            exported.ShouldNotBeNull();
        }

        [Fact]
        public void CustomersController_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<CustomersController>();

            exported.ShouldNotBeNull();
        }

        [Fact]
        public void DashboardController_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<DashboardController>();

            exported.ShouldNotBeNull();
        }
    }
}

