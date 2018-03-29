using System;
using System.Collections.Generic;
using Eml.Contracts.Entities;
using Eml.Contracts.Repositories.Bases;
using Eml.Mediator.Contracts;

namespace TechChallengeAspNetCore.ApiHost.BaseClasses
{
    public abstract class CrudControllerBase<TKey, T> : EntityControllerBase<TKey, T, IDataRepositoryBase<TKey, T>>
        where T : class, IEntityBase<TKey>, ISearchableName
    {
        protected CrudControllerBase(IMediator mediator, IDataRepositoryBase<TKey, T> repository)
            : base(mediator, repository)
        {
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            disposables.Add(repository);
        }
    }
}

