using System.Collections.Generic;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Dto;

namespace TechChallengeAspNetCore.Business.Responses
{
    public class RiskCustomerResponse : IResponse
    {
        public IEnumerable<RiskCustomer> RiskCustomers { get; }

        public RiskCustomerResponse(IEnumerable<RiskCustomer> riskCustomers)
        {
            RiskCustomers = riskCustomers;
        }
    }
}
