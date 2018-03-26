using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Responses;

namespace TechChallengeAspNetCore.Business.Requests
{
    public class RiskCustomerRequest : IRequestAsync<RiskCustomerRequest, RiskCustomerResponse>
    {
        public int PageNumber { get; }

	    /// <summary>
        /// This request will be processed by <see cref="RiskCustomerEngine"/>.
        /// </summary>
        public RiskCustomerRequest(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }
}

