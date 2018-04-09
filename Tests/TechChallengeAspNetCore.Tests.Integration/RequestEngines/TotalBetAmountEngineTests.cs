using System.Linq;
using System.Threading.Tasks;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.RequestEngines
{
    public class TotalBetAmountEngineTests : IntegrationTestDbBase
    {
        [Fact]
        public async Task Engine_ShouldHandleNullData()
        {
            var request = new TotalBetAmountRequest();

            var response = await mediator.GetAsync(request);

            response.CustomerBets.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnAmountBetPerCustomer()
        {
            var request = new TotalBetAmountRequest();

            var response = await mediator.GetAsync(request);

            response.CustomerBets.Count().ShouldBeGreaterThan(0);
        }
    }
}
