using System.Collections.Generic;
using System.Threading.Tasks;
using Eml.ConfigParser.Helpers;
using TechChallengeAspNetCore.ApiHost.Controllers;
using TechChallengeAspNetCore.Business.Common.Dto;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Tests.Unit.BaseClasses;
using TechChallengeAspNetCore.Data.Repositories;
using NSubstitute;
using Xunit;


namespace TechChallengeAspNetCore.Tests.Unit.Controllers
{
    public class CustomerControllerTests : ControllerTestBase<CustomersController>
    {
        public CustomerControllerTests()
        {
            var configuration = ConfigBuilder.GetConfiguration();

            controller = new CustomersController(mediator, new DataRepositorySoftDeleteInt<Customer>(configuration));
        }

        [Fact]
        public async Task Controller_ShouldGetAllBetCount()
        {
            mediator.GetAsync(Arg.Any<TotalBetCountRequest>()).Returns(new TotalBetCountResponse(new List<CustomerBetCount>()));

            var response = await controller.GetBets(1, 1);

            await mediator.Received().GetAsync(Arg.Any<TotalBetCountRequest>());
        }

        [Fact]
        public async Task Controller_ShouldGetRiskCustomers()
        {
            mediator.GetAsync(Arg.Any<RiskCustomerRequest>()).Returns(new RiskCustomerResponse(new List<RiskCustomer>()));

            var response = await controller.GetRisks(1);

            await mediator.Received().GetAsync(Arg.Any<RiskCustomerRequest>());
        }
    }
}

