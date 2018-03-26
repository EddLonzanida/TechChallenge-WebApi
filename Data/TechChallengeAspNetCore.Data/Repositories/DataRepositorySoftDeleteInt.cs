using System.Composition;
using Eml.Contracts.Entities;
using Eml.Contracts.Repositories;
using Eml.DataRepository;
using Microsoft.Extensions.Configuration;

namespace TechChallengeAspNetCore.Data.Repositories
{
    [Export(typeof(IDataRepositorySoftDeleteInt<>))] //If using GUID, change to [Export(typeof(IDataRepositorySoftDeleteGuid<>))]
    public class DataRepositorySoftDeleteInt<T> : DataRepositorySoftDeleteInt<T, TechChallengeAspNetCoreDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
        [ImportingConstructor]
        public DataRepositorySoftDeleteInt(IConfiguration configuration) : base(configuration)
        {
        }
    }
}

