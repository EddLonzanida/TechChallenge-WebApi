using System.Collections.Generic;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Dto;

namespace TechChallengeAspNetCore.Business.Responses
{
    public class RaceStatResponse : IResponse
    {
        public IEnumerable<RaceStat> RaceStats { get; }

        public RaceStatResponse(IEnumerable<RaceStat> raceStats)
        {
            RaceStats = raceStats;
        }
    }
}

