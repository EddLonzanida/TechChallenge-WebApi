using Eml.Contracts.Repositories;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Integration.DataRepositories
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void RaceRepository_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IDataRepositoryInt<Race>>();

            exported.ShouldNotBeNull();
            exported.PageSize.ShouldBe(10);
        }

        [Fact]
        public void BetRepository_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IDataRepositoryInt<Bet>>();

            exported.ShouldNotBeNull();
            exported.PageSize.ShouldBe(10);
        }

        [Fact]
        public void CustomerRepository_ShouldBeDiscoverable()
        {
            var exported = classFactory.GetExport<IDataRepositoryInt<Customer>>();

            exported.ShouldNotBeNull();
            exported.PageSize.ShouldBe(10);
        }
    }
}

