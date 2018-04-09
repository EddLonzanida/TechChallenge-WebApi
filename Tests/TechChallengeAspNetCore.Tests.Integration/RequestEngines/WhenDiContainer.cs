using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.RequestEngines
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void RaceStatEngine_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<RaceStatRequest, RaceStatResponse>>();

            exported.ShouldNotBeNull();
        }

        [Fact]
        public void RiskCustomerEngine_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<RiskCustomerRequest, RiskCustomerResponse>>();

            exported.ShouldNotBeNull();
        }

        [Fact]
        public void TotalBetAmountEngine_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<TotalBetAmountRequest, TotalBetAmountResponse>>();

            exported.ShouldNotBeNull();
        }

        [Fact]
        public void TotalBetCountEngine_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IRequestAsyncEngine<TotalBetCountRequest, TotalBetCountResponse>>();

            exported.ShouldNotBeNull();
        }
    }
}
