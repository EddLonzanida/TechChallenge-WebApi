using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Responses;

namespace TechChallengeAspNetCore.Business.Requests
{
    public class TotalBetAmountRequest : IRequestAsync<TotalBetAmountRequest, TotalBetAmountResponse>
    {
        public int PageNumber { get; }

	    /// <summary>
        /// This request will be processed by <see cref="TotalBetAmountEngine"/>.
        /// </summary>
        public TotalBetAmountRequest()
        {
            PageNumber = 1;
        }
    }
}
