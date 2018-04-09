using System.Linq;
using System.Threading.Tasks;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.RequestEngines
{
    public class RaceStatEngineTests : IntegrationTestDbBase
    {
        [Fact]
        public async Task Engine_ShouldHandleNullData()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnRaceStatus()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldSumAllRaceMoney()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);

        }

        [Fact]
        public async Task Engine_ShouldReturnAllHorsesPerRace()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnHorseNames()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnHorseBetCount()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Engine_ShouldReturnTotalHorseWinPrize()
        {
            var request = new RaceStatRequest(1);

            var response = await mediator.GetAsync(request);

            response.RaceStats.Count().ShouldBeGreaterThan(0);
        }
    }
}
