using System;
using System.IO;
using Eml.DataRepository;
using Eml.Mediator.Contracts;
using TechChallengeAspNetCore.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace TechChallengeAspNetCore.Tests.Unit.BaseClasses
{
    public abstract class ControllerTestBase<T> : IDisposable
        where T : Controller
    {
        const string JSON_SOURCES = @"SampleData";

        protected readonly IMediator mediator;

        protected readonly TotalBetAmountResponse totalBetAmountResponseStub;

        protected T controller;

        protected ControllerTestBase()
        {
            totalBetAmountResponseStub = Seed.GetStub<TotalBetAmountResponse>("TotalBetAmountResponse", JSON_SOURCES);
            mediator = Substitute.For<IMediator>();
        }

        public void Dispose()
        {
            controller?.Dispose();
        }

        private static string GetJsonPath(string jsonFile)
        {
            var baseDirectory = Directory.GetCurrentDirectory();
            var jsonPath = $@"{baseDirectory}\{JSON_SOURCES}\{jsonFile}.json";

            return jsonPath;
        }
    }
}

