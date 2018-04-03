using System.Composition;
using System.Threading.Tasks;
using Eml.ControllerBase;
using Microsoft.AspNetCore.Mvc;
using Eml.DataRepository.Contracts;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace TechChallengeAspNetCore.ApiHost.Controllers
{
    [Export]
    public class CustomersController : CrudControllerBase<int, Customer>
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

        protected override async Task<SearchResponse<Customer>> GetAll(string search = "", int? page = 1, bool? desc = false, int? sortColumn = 0)
        {
             search = search.ToLower();
            Expression<Func<Customer, bool>> whereClause = r => search == "" || r.SearchableName.ToLower().Contains(search);

            Func<IQueryable<Customer>, IQueryable<Customer>> orderBy = r => r.OrderBy(x => x.SearchableName);
           
            if (desc.Value)orderBy = r => r.OrderByDescending(x => x.SearchableName);

            var items = await repository.GetPagedListAsync(page.Value, whereClause, orderBy);

            return new SearchResponse<Customer>(items, items.TotalItemCount, repository.PageSize);
        }
    }
}

