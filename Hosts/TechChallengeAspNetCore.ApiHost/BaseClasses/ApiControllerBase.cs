using System;
using System.Collections.Generic;
using Eml.ExplicitDispose.Api;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeAspNetCore.ApiHost.BaseClasses
{
    [ExplicitDispose]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public abstract class ApiControllerBase : Controller, IDisposeAware
    {
        protected readonly IMediator mediator;

        protected ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Ex: Disposables.Add([Concrete classes that implements IDisposable]);
        /// </summary>
        /// <param name="disposables"></param>
        protected abstract void RegisterIDisposable(List<IDisposable> disposables);

        public List<IDisposable> Disposables { get; private set; } = new List<IDisposable>();

        [ApiExplorerSettings(IgnoreApi = true)]
        public void RegisterDisposables(List<IDisposable> disposables)
        {
            RegisterIDisposable(disposables);
        }
    }
}


