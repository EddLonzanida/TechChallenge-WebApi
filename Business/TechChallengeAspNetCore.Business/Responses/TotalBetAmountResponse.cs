using System.Collections.Generic;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Dto;

namespace TechChallengeAspNetCore.Business.Responses
{
   public class TotalBetAmountResponse : IResponse
    {
        public IEnumerable<CustomerBetAmount> CustomerBets { get; }

        public TotalBetAmountResponse(IEnumerable<CustomerBetAmount> customerBets)
        {
            CustomerBets = customerBets;
        }
    }
}
