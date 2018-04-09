using System.Collections.Generic;
using System.Threading.Tasks;
using TechChallengeAspNetCore.ApiHost.Controllers;
using TechChallengeAspNetCore.Business.Common.Dto;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Tests.Unit.BaseClasses;
using NSubstitute;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Unit.Controllers
{
    public class DashboardControllerTests : ControllerTestBase<DashboardController>
    {
        public DashboardControllerTests()
        {
            controller = new DashboardController(mediator);
        }

        [Fact]
        public async Task Controller_ShouldGetAllCustomers()
        {
            mediator.GetAsync(Arg.Any<RaceStatRequest>()).Returns(new RaceStatResponse(new List<RaceStat>()));

            var response = await controller.Get(1);

            await mediator.Received().GetAsync(Arg.Any<RaceStatRequest>());
        }
    }
}
