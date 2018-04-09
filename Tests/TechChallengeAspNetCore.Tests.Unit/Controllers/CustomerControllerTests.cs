using System.Collections.Generic;
using System.Threading.Tasks;
using TechChallengeAspNetCore.ApiHost.Controllers;
using TechChallengeAspNetCore.Business.Common.Dto;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Tests.Unit.BaseClasses;
using NSubstitute;
using Xunit;
using X.PagedList;
using System.Linq.Expressions;
using System.Linq;
using Shouldly;
using System;
using Microsoft.AspNetCore.Mvc;
using Eml.DataRepository.Contracts;

namespace TechChallengeAspNetCore.Tests.Unit.Controllers
{
    public class CustomerControllerTests : ControllerTestBase<CustomersController>
    {
        protected readonly IDataRepositorySoftDeleteInt<Customer> repository;

        public CustomerControllerTests()
        {
            repository = Substitute.For<IDataRepositorySoftDeleteInt<Customer>>();

            controller = new CustomersController(mediator, repository);
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

        [Fact]
        public async Task Controller_ShouldGetCustomers()
        {
            var pagedList = new PagedList<Customer>(customerStub, 1, 10);
            repository.GetPagedListAsync(Arg.Any<int>(),
                                         Arg.Any<Expression<Func<Customer, bool>>>(),
                                         Arg.Any<Func<IQueryable<Customer>, IQueryable<Customer>>>())
                       .Returns(pagedList);

            var response = await controller.Get() as OkObjectResult; ;

            await repository.Received()
                .GetPagedListAsync(Arg.Any<int>(),
                                   Arg.Any<Expression<Func<Customer, bool>>>(),
                                   Arg.Any<Func<IQueryable<Customer>, IQueryable<Customer>>>());
            var result = response?.Value as IEnumerable<Customer>;
            result?.ToList().Count.ShouldBe(10);
        }
    }
}
