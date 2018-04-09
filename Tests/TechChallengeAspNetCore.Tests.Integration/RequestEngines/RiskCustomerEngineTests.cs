using System.Linq;
using System.Threading.Tasks;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.RequestEngines
{
    public class RiskCustomerEngineTests : IntegrationTestDbBase
    {
        [Fact]
        public async Task Engine_ShouldHandleNullData()
        {
            var request = new RiskCustomerRequest(pageNumber: 1);

            var response = await mediator.GetAsync(request);

            response.RiskCustomers.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnRiskCustomers()
        {
            var request = new RiskCustomerRequest(pageNumber: 1);

            var response = await mediator.GetAsync(request);

            response.RiskCustomers.Count().ShouldBeGreaterThan(0);
        }
    }
}
