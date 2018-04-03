using System;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using Eml.ControllerBase;
using Microsoft.AspNetCore.Mvc;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;

namespace TechChallengeAspNetCore.ApiHost.Controllers
{
    [Export]
    public class DashboardController : ApiControllerBase
    {
        [ImportingConstructor]
        public DashboardController(IMediator mediator) : base(mediator)
        {
        }

        [Route("")]
        [HttpGet]
        public async Task<RaceStatResponse> Get(int? pageNumber = 1)
        {
            var request = new RaceStatRequest(pageNumber.Value);

            return await mediator.GetAsync(request);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
    }
}

