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
   public class TotalBetAmountEngine : IRequestAsyncEngine<TotalBetAmountRequest, TotalBetAmountResponse>
    {
        private readonly IDataRepositoryInt<Bet> betsRepository;

        [ImportingConstructor]
        public TotalBetAmountEngine(IDataRepositoryInt<Bet> betsRepository)
        {
            this.betsRepository = betsRepository;
        }

        public async Task<TotalBetAmountResponse> GetAsync(TotalBetAmountRequest request)
        {
            var bets = await EntityFactory.GetBets( betsRepository);

            if (bets == null) return new TotalBetAmountResponse(new List<CustomerBetAmount>());

            var betAmounts = bets
                .GroupBy(r => new { customerId = r.CustomerId })
                .Select(g => new CustomerBetAmount
                {
                    Id = g.Key.customerId,
                    Totalstake = g.Sum(r => r.Stake)
                })
                .OrderBy(r => r.Totalstake)
                .ThenBy(r => r.Id);

            return new TotalBetAmountResponse(betAmounts);
        }

        public void Dispose()
        {
            betsRepository?.Dispose();
        }
    }
}

