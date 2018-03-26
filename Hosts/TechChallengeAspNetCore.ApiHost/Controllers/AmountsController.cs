using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.ApiHost.BaseClasses;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;

namespace TechChallengeAspNetCore.ApiHost.Controllers
{
    [Export]
    public class AmountsController : ApiControllerBase
    {
        [ImportingConstructor]
        public AmountsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<TotalBetAmountResponse> Get()
        {
            var request = new TotalBetAmountRequest();
            var response = await mediator.GetAsync(request);

            return response;
        }

        [HttpGet]
        [Route("Total")]
        public async Task<double> GetTotal()
        {
            var request = new TotalBetAmountRequest();
            var response = await mediator.GetAsync(request);
            var grandTotal = response.CustomerBets.Sum(r => r.Totalstake);

            return grandTotal;
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}

