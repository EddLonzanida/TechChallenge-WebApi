using System.Linq;
using System.Threading.Tasks;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.RequestEngines
{
    public class TotalBetCountEngineTests : IntegrationTestDbBase
    {
        [Fact]
        public async Task Engine_ShouldHandleNullData()
        {
            var request = new TotalBetCountRequest(customerId: 0, pageNumber: 1);

            var response = await mediator.GetAsync(request);

            response.BetCounts.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnCustomerBetCount()
        {
            var request = new TotalBetCountRequest(customerId: 0, pageNumber: 1);

            var response = await mediator.GetAsync(request);

            response.BetCounts.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnSingleCustomerBetCount()
        {
            var request = new TotalBetCountRequest(customerId: 1, pageNumber: 1);

            var response = await mediator.GetAsync(request);

            response.BetCounts.Count().ShouldBeGreaterThan(0);
        }
    }
}
