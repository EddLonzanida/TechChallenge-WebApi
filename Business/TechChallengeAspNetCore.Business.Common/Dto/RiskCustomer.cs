using System.Collections.Generic;
using Eml.EntityBaseClasses;
using TechChallengeAspNetCore.Business.Common.Entities;

namespace TechChallengeAspNetCore.Business.Common.Dto
{
    public class RiskCustomer : EntityBaseInt
    {
        public string Name { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
