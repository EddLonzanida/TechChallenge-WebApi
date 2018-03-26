using System;
using System.Collections.Generic;
using Eml.Contracts.Repositories;
using Eml.DataRepository;
using Eml.Mediator.Contracts;
using NSubstitute;
using TechChallengeAspNetCore.Business.Common.Entities;

namespace TechChallengeAspNetCore.Tests.Unit.BaseClasses
{
    public abstract class EngineTestBase<T1, T2> : IDisposable
        where T1 : IRequestAsync<T1, T2> where T2 : IResponse
    {
        private const string JSON_SOURCES = @"SampleData";

        protected IRequestAsyncEngine<T1, T2> engine;

        protected readonly List<Race> racesStub;

        protected readonly List<Bet> betStub;

        protected readonly List<Customer> customerStub;

        protected readonly IDataRepositoryInt<Race> raceRepository;

        protected readonly IDataRepositoryInt<Bet> betRepository;

        protected readonly IDataRepositoryInt<Customer> customerRepository;

        protected EngineTestBase()
        {
            racesStub = Seed.GetStubs<Race>("races", JSON_SOURCES);
            betStub = Seed.GetStubs<Bet>("bets", JSON_SOURCES);
            customerStub = Seed.GetStubs<Customer>("customers", JSON_SOURCES);

            raceRepository = Substitute.For<IDataRepositoryInt<Race>>();
            betRepository = Substitute.For<IDataRepositoryInt<Bet>>();
            customerRepository = Substitute.For<IDataRepositoryInt<Customer>>();
        }

        public void Dispose()
        {
            engine?.Dispose();
            raceRepository?.Dispose();
            betRepository?.Dispose();
            customerRepository?.Dispose();
        }
    }
}

