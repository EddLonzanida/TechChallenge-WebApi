using System;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eml.Contracts.Repositories;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.ApiHost.BaseClasses;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;

namespace TechChallengeAspNetCore.ApiHost.Controllers
{
    [Export]
    public class CustomersController : CrudControllerSoftDeleteBase<int, Customer>
    {
        [ImportingConstructor]
        public CustomersController(IMediator mediator, IDataRepositorySoftDeleteInt<Customer> repository)
            : base(mediator, repository)
        {
        }

        [Route("{Id}/Bets")]
        [HttpGet]
        public async Task<TotalBetCountResponse> GetBets(int Id, int? pageNumber = 1)
        {
            var request = new TotalBetCountRequest(Id, pageNumber.Value);

            return await mediator.GetAsync(request);
        }


        [Route("Risks")]
        [HttpGet]
        public async Task<RiskCustomerResponse> GetRisks(int? pageNumber = 1)
        {
            var request = new RiskCustomerRequest(pageNumber.Value);

            return await mediator.GetAsync(request);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}

