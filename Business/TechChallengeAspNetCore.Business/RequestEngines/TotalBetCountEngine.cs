using System.Collections.Generic;
using System.Linq;
using System.Composition;
using System.Threading.Tasks;
using Eml.Contracts.Repositories;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.Common.Helpers;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Business.Common.Dto;

namespace TechChallengeAspNetCore.Business.RequestEngines
{
    public class TotalBetCountEngine : IRequestAsyncEngine<TotalBetCountRequest, TotalBetCountResponse>
    {
        private readonly IDataRepositoryInt<Bet> betsRepository;

        [ImportingConstructor]
        public TotalBetCountEngine(IDataRepositoryInt<Bet> betsRepository)
        {
            this.betsRepository = betsRepository;
        }

        public async Task<TotalBetCountResponse> GetAsync(TotalBetCountRequest request)
        {
            var bets = await EntityFactory.GetBets(betsRepository);

            if (bets == null) return new TotalBetCountResponse(new List<CustomerBetCount>());

            var betCounts = bets
                .GroupBy(r => new { customerId = r.CustomerId })
                .Select(g => new CustomerBetCount
                {
                    Id = g.Key.customerId,
                    TotalBets = g.Count()
                })
                .OrderBy(r => r.TotalBets)
                .ThenBy(r => r.Id);

            if (request.CustomerId > 0)
            {
                var filteredCustomers = betCounts.ToList()
                    .Where(r => r.Id == request.CustomerId);
                return new TotalBetCountResponse(filteredCustomers);
            }

            return new TotalBetCountResponse(betCounts);
        }

        public void Dispose()
        {
            betsRepository?.Dispose();
        }
    }
}

