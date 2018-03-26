using System.Collections.Generic;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Dto;

namespace TechChallengeAspNetCore.Business.Responses
{
    public class TotalBetCountResponse : IResponse
    {
        public IEnumerable<CustomerBetCount> BetCounts { get; }

        public TotalBetCountResponse(IEnumerable<CustomerBetCount> betCounts)
        {
            BetCounts = betCounts;
        }
    }
}

