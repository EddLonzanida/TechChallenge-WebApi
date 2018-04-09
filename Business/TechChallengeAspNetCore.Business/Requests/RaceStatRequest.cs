using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Responses;

namespace TechChallengeAspNetCore.Business.Requests
{
    public class RaceStatRequest : IRequestAsync<RaceStatRequest, RaceStatResponse>
    {
        public int PageNumber { get; }

	    /// <summary>
        /// This request will be processed by <see cref="RaceStatEngine"/>.
        /// </summary>
        public RaceStatRequest(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }
}
