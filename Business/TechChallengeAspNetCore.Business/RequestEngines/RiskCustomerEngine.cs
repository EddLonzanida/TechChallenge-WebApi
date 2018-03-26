using System.Collections.Generic;
using System.Composition;
using System.Linq;
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
    public class RiskCustomerEngine : IRequestAsyncEngine<RiskCustomerRequest, RiskCustomerResponse>
    {
        private const double RISKY_AMOUNT = 200;

        private readonly IDataRepositoryInt<Customer> customersRepository;

        private readonly IDataRepositoryInt<Bet> betsRepository;

        [ImportingConstructor]
        public RiskCustomerEngine(IDataRepositoryInt<Customer> customersRepository, IDataRepositoryInt<Bet> betsRepository)
        {
            this.customersRepository = customersRepository;
            this.betsRepository = betsRepository;
        }

        public async Task<RiskCustomerResponse> GetAsync(RiskCustomerRequest request)
        {
            var customers = await EntityFactory.GetCustomers(customersRepository);
            var bets = await EntityFactory.GetBets(betsRepository);

            if (bets == null) return new RiskCustomerResponse(new List<RiskCustomer>());

            if (customers == null) return new RiskCustomerResponse(new List<RiskCustomer>());

            var riskCustomers = customers
                .Select(r => new RiskCustomer
                {
                    Id = r.Id,
                    Name = r.Name,
                    Bets = GetCustomerBets(r.Id, bets)
                })
                .Where(r => r.Bets.Any())
                .OrderBy(r => r.Name);

            return new RiskCustomerResponse(riskCustomers);
        }

        private static List<Bet> GetCustomerBets(int customerId, IEnumerable<Bet> bets)
        {
            return bets
                .Where(r => r.CustomerId == customerId && r.Stake > RISKY_AMOUNT) //over $200
                .OrderBy(r => r.Stake)
                .ThenBy(r => r.RaceId)
                .ToList();
        }

        public void Dispose()
        {
            customersRepository?.Dispose();
            betsRepository?.Dispose();
        }
    }
}

