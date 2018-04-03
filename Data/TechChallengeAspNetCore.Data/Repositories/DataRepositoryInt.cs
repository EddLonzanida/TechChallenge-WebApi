using System.Composition;
using Eml.Contracts.Entities;
using Eml.DataRepository;
using Eml.DataRepository.Contracts;
using Microsoft.Extensions.Configuration;

namespace TechChallengeAspNetCore.Data.Repositories
{
    [Export(typeof(IDataRepositoryInt<>))] //If using GUID, change to [Export(typeof(IDataRepositoryGuid<>))]
    public class DataRepositoryInt<T> : DataRepositoryInt<T, TechChallengeAspNetCoreDb>
        where T : class, IEntityBase<int>
    {
        [ImportingConstructor]
        public DataRepositoryInt(IConfiguration configuration) : base(configuration)
        {
        }
    }
}

