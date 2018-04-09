using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Eml.DataRepository.Contracts;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.Common.Helpers;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Business.Common.Dto;

namespace TechChallengeAspNetCore.Business.RequestEngines
{
    public class RaceStatEngine : IRequestAsyncEngine<RaceStatRequest, RaceStatResponse>
    {
        private readonly IDataRepositoryInt<Race> racesRepository;

        private readonly IDataRepositoryInt<Bet> betsRepository;

        [ImportingConstructor]
        public RaceStatEngine(IDataRepositoryInt<Race> racesRepository, IDataRepositoryInt<Bet> betsRepository)
        {
            this.racesRepository = racesRepository;
            this.betsRepository = betsRepository;
        }

        public async Task<RaceStatResponse> GetAsync(RaceStatRequest request)
        {
            var races = await EntityFactory.GetRaces(racesRepository);
            var bets = await EntityFactory.GetBets(betsRepository);

            var response = races
                .Select(r => new RaceStat
                {

                    Id = r.Id,
                    Name = r.Name,
                    Status = r.Status,
                    Start = r.Start,
                    RaceTotalAmount = GetRaceAmount(r.Id, bets),
                    HorseStats = r.Horses.Select(h => new HorseStat
                    {
                        Id = h.Id,
                        Name = h.Name,
                        RaceId = r.Id,
                        Odds = h.Odds,
                        BetCount = GetBetCount(h.Id, r.Id, bets),
                        WinAmount = GetWinAmount(r.Id, h.Id, h.Odds, bets)
                    })
                    .OrderBy(h => h.WinAmount)
                    .ThenBy(h => h.Odds)
                    .ThenBy(h => h.BetCount)
                    .ThenBy(h => h.Name)
                    .ToList()
                })
                .OrderBy(r => r.Start)
                .ThenBy(r => r.Name);

            return new RaceStatResponse(response);
        }

        private static int GetBetCount(int horseId, int raceId, IEnumerable<Bet> bets)
        {
            return bets.Count(r => r.HorseId == horseId && r.RaceId == raceId);
        }

        private static double GetWinAmount(int raceId, int horseId, double odds, IEnumerable<Bet> bets)
        {
            return bets
                .Where(r => r.HorseId == horseId && r.RaceId == raceId)
                .Sum(r => r.Stake * odds);
        }

        private static double GetRaceAmount(int raceId, IEnumerable<Bet> bets)
        {
            var amount = bets
                .Where(r => r.RaceId == raceId)
                .Sum(r => r.Stake);

            return amount;
        }

        public void Dispose()
        {
            racesRepository?.Dispose();
            betsRepository?.Dispose();
        }
    }
}
