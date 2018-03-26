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
    public class AmountsControllerTests : ControllerTestBase<AmountsController>
    {
        public AmountsControllerTests()
        {
            controller = new AmountsController(mediator);
        }

        [Fact]
        public async Task Controller_ShouldReturnGrandTotalBetAmount()
        {
            mediator.GetAsync(Arg.Any<TotalBetAmountRequest>()).Returns(totalBetAmountResponseStub);

            var response = await controller.GetTotal();

            await mediator.Received().GetAsync(Arg.Any<TotalBetAmountRequest>());
            response.ShouldBe(6060);
        }

        [Fact]
        public async Task Controller_ShouldGetAllAmountsPerCustomer()
        {
            mediator.GetAsync(Arg.Any<TotalBetAmountRequest>()).Returns(new TotalBetAmountResponse(new List<CustomerBetAmount>()));

            var response = await controller.Get();

            await mediator.Received().GetAsync(Arg.Any<TotalBetAmountRequest>());
        }
    }
}


