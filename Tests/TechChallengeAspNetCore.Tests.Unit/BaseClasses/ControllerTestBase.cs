using System;
using Eml.DataRepository;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Business.Common.Entities;

namespace TechChallengeAspNetCore.Tests.Unit.BaseClasses
{
    public abstract class ControllerTestBase<T> : IDisposable
        where T : Controller
    {
        const string SAMPLE_DATA_SOURCES = @"SampleDataSources";

        protected readonly IMediator mediator;

        protected readonly TotalBetAmountResponse totalBetAmountResponseStub;

        protected readonly List<Customer> customerStub;

        protected T controller;

        protected ControllerTestBase()
        {
            totalBetAmountResponseStub = Seeder.GetJsonStub<TotalBetAmountResponse>("TotalBetAmountResponse", SAMPLE_DATA_SOURCES);
            customerStub = Seeder.GetJsonStubs<Customer>("customers", SAMPLE_DATA_SOURCES);
          
            mediator = Substitute.For<IMediator>();
        }

        public void Dispose()
        {
            controller?.Dispose();
        }
    }
}
