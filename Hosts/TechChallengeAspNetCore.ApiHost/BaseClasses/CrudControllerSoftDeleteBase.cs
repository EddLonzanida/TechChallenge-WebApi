using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eml.Contracts.Entities;
using Eml.Contracts.Repositories.Bases;
using Eml.Mediator.Contracts;

namespace TechChallengeAspNetCore.ApiHost.BaseClasses
{
    public abstract class CrudControllerSoftDeleteBase<TKey, T> : EntityControllerBase<TKey, T, IDataRepositorySoftDeleteBase<TKey, T>>
        where T : class, IEntityBase<TKey>, ISearchableName, IEntitySoftdeletableBase
    {
        protected CrudControllerSoftDeleteBase(IMediator mediator, IDataRepositorySoftDeleteBase<TKey, T> repository)
            : base(mediator, repository)
        {
        }

        protected override async Task<T> DeleteItem(TKey id, string reason = "")
        {
            return await repository.DeleteAsync(id, reason);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            disposables.Add(repository);
        }
    }
}