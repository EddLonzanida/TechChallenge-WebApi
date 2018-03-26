using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.RequestEngines;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Tests.Unit.BaseClasses;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Unit.RequestEngines
{
    public class TotalBetCountEngineTests : EngineTestBase<TotalBetCountRequest, TotalBetCountResponse>
    {
        public TotalBetCountEngineTests()
        {
            engine = new TotalBetCountEngine(betRepository);
        }

        [Fact]
        public async Task Engine_ShouldHandleNullData()
        {
            List<Bet> nullBetData = null;
            betRepository.GetAllAsync(Arg.Any<int>()).Returns(nullBetData);
            var request = new TotalBetCountRequest(0, 1);

            var response = await engine.GetAsync(request);

            await betRepository.Received(1).GetAllAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task Engine_ShouldReturnCustomerBetCount()
        {
            betRepository.GetAllAsync(Arg.Any<int>()).Returns(betStub);
            var request = new TotalBetCountRequest(0, 1);

            var response = await engine.GetAsync(request);

            response.BetCounts.Count().ShouldBe(7);
            var c1 = response.BetCounts.First(r => r.Id == 1);
            var c2 = response.BetCounts.First(r => r.Id == 2);
            var c3 = response.BetCounts.First(r => r.Id == 3);
            var c4 = response.BetCounts.First(r => r.Id == 4);
            var c5 = response.BetCounts.First(r => r.Id == 5);
            var c6 = response.BetCounts.First(r => r.Id == 6);
            var c7 = response.BetCounts.First(r => r.Id == 7);
            c1.TotalBets.ShouldBe(4);
            c2.TotalBets.ShouldBe(4);
            c3.TotalBets.ShouldBe(3);
            c4.TotalBets.ShouldBe(3);
            c5.TotalBets.ShouldBe(5);
            c6.TotalBets.ShouldBe(5);
            c7.TotalBets.ShouldBe(5);
        }

        [Fact]
        public async Task Engine_ShouldReturnSingleCustomerBetCount()
        {
            betRepository.GetAllAsync(Arg.Any<int>()).Returns(betStub);
            var request = new TotalBetCountRequest(1, 1);

            var response = await engine.GetAsync(request);

            response.BetCounts.Count().ShouldBe(1);
        }
    }
}

