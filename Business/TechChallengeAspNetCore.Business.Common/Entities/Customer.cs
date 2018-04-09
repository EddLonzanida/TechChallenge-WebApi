using System.ComponentModel.DataAnnotations.Schema;
using Eml.EntityBaseClasses;
using Eml.Contracts.Entities;

namespace TechChallengeAspNetCore.Business.Common.Entities
{
    public class Customer : EntityBaseSoftDeleteInt, ISearchableName
    {
        public string Name { get; set; }

        [NotMapped]
        public string SearchableName => Name;
    }
}
